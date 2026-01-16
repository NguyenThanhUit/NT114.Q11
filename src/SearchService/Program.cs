using OrderService;
using Polly.Extensions.Http;
using Polly;
using MassTransit;
using System.Net;
using SearchService;
using SearchService.Consumers;
using Contracts;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Fix Envoy reuse connection nhưng app đóng socket quá sớm
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);     // ⬅ FIX RST 5s
    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
});


// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Order Service HTTP Client
builder.Services.AddHttpClient<OrderSvcHttpClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2), // ⬅ match Kestrel
    KeepAlivePingDelay = TimeSpan.FromSeconds(30),
    KeepAlivePingTimeout = TimeSpan.FromSeconds(10),
    KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always
})
.AddPolicyHandler(GetPolicy());

// Auction Service HTTP Client
builder.Services.AddHttpClient<AuctionSvcHTTPClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
    KeepAlivePingDelay = TimeSpan.FromSeconds(30),
    KeepAlivePingTimeout = TimeSpan.FromSeconds(10),
    KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always
})
.AddPolicyHandler(GetPolicy());



builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<OrderCreatedConsumer>();
    x.AddConsumersFromNamespaceContaining<OrderUpdatedConsumer>();
    x.AddActivitiesFromNamespaceContaining<BuyingPlacedConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});



var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

#region 🚀 APPLICATION STARTUP LOGIC

app.Lifetime.ApplicationStarted.Register(async () =>
{
    Console.WriteLine("=== Application Started ===");
    Console.WriteLine($"UTC Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}");

    Console.WriteLine("Waiting for Istio sidecar...");
    await WaitForIstioSidecar();
    Console.WriteLine("✓ Istio sidecar ready");

    await Task.Delay(TimeSpan.FromSeconds(15));
    Console.WriteLine("✓ Waited 15s for dependencies");

    Console.WriteLine("Starting DB initialization...");

    var result = await Policy
        .Handle<TimeoutException>()
        .Or<HttpRequestException>()
        .Or<TaskCanceledException>()
        .WaitAndRetryAsync(
            retryCount: 5,
            sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
            onRetry: (ex, ts, retry, _) =>
            {
                Console.WriteLine($"❌ Retry #{retry} after {ts.TotalSeconds}s");
                Console.WriteLine($"   {ex.GetType().Name}: {ex.Message}");
            })
        .ExecuteAndCaptureAsync(async () =>
        {
            Console.WriteLine("→ DbInitializers.InitDb()");
            await DbInitializers.InitDb(app);
        });

    if (result.Outcome == OutcomeType.Failure)
    {
        Console.WriteLine("❌ DB INIT FAILED");
        Console.WriteLine(result.FinalException?.Message);
    }
    else
    {
        Console.WriteLine("✓ DB INIT SUCCESS");
    }

    Console.WriteLine("=== Startup completed ===");
});



app.Run();



static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retry => TimeSpan.FromSeconds(Math.Pow(2, retry)),
            onRetry: (outcome, ts, retry, _) =>
            {
                var reason = outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString();
                var url = outcome.Result?.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                Console.WriteLine($"⚠ HTTP Retry #{retry} → {url}");
                Console.WriteLine($"   Reason: {reason}");
                Console.WriteLine($"   Wait: {ts.TotalSeconds}s");
            });


static async Task WaitForIstioSidecar()
{
    const int maxRetries = 30;
    var delay = TimeSpan.FromSeconds(1);

    for (int i = 1; i <= maxRetries; i++)
    {
        try
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
            var res = await client.GetAsync("http://localhost:15021/healthz/ready");

            if (res.IsSuccessStatusCode)
            {
                Console.WriteLine($"✓ Sidecar ready after {i}s");
                return;
            }
        }
        catch (Exception ex)
        {
            if (i == 1 || i % 5 == 0)
                Console.WriteLine($"Attempt {i}: {ex.Message}");
        }

        await Task.Delay(delay);
    }

    Console.WriteLine("⚠ Istio sidecar NOT ready, continuing anyway");
}


