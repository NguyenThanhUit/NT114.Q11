using OrderService;
using Polly.Extensions.Http;
using Polly;
using MassTransit;
using System.Net;
using SearchService;
using SearchService.Consumers;
using Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Thêm mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Thêm HTTP client với timeout và logging
builder.Services.AddHttpClient<OrderSvcHttpClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
})
.AddPolicyHandler(GetPolicy())
.AddLogger(); // Thêm logging

builder.Services.AddHttpClient<AuctionSvcHTTPClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
})
.AddPolicyHandler(GetPolicy())
.AddLogger(); // Thêm logging

builder.Services.AddMassTransit(x =>
{
    // Them consumer
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

app.Lifetime.ApplicationStarted.Register(async () =>
{
    Console.WriteLine("=== Application Started Event Triggered ===");
    Console.WriteLine($"Current Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC");
    
    // Đợi Istio sidecar ready
    Console.WriteLine("Checking if Istio sidecar is ready...");
    await WaitForIstioSidecar();
    Console.WriteLine("✓ Istio sidecar is ready!");
    
    // Retry với số lần giới hạn
    Console.WriteLine("Starting database initialization with retry policy...");
    var result = await Policy.Handle<TimeoutException>()
        .Or<HttpRequestException>()
        .WaitAndRetryAsync(
            retryCount: 5,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(5),
            onRetry: (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"❌ [DbInitializer] Retry #{retryCount} after {timeSpan.TotalSeconds}s");
                Console.WriteLine($"   Exception: {exception.GetType().Name} - {exception.Message}");
            }
        )
        .ExecuteAndCaptureAsync(async () => 
        {
            Console.WriteLine("→ Calling DbInitializers.InitDb...");
            await DbInitializers.InitDb(app);
            Console.WriteLine("✓ DbInitializers.InitDb completed successfully!");
        });
    
    if (result.Outcome == OutcomeType.Failure)
    {
        Console.WriteLine($"❌ Database initialization FAILED after all retries!");
        Console.WriteLine($"   Final Exception: {result.FinalException?.GetType().Name} - {result.FinalException?.Message}");
    }
    else
    {
        Console.WriteLine("✓ Database initialization completed successfully!");
    }
    
    Console.WriteLine("=== Application Startup Completed ===");
});

app.Run();

// Retry policy với logging
static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential backoff: 2s, 4s, 8s
            onRetry: (outcome, timespan, retryCount, context) =>
            {
                var message = outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString();
                var url = outcome.Result?.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                Console.WriteLine($"⚠ [HttpClient Retry] #{retryCount} for {url}");
                Console.WriteLine($"   Reason: {message}");
                Console.WriteLine($"   Waiting {timespan.TotalSeconds}s before retry...");
            }
        );

// Hàm đợi Istio sidecar ready với logging
static async Task WaitForIstioSidecar()
{
    var maxRetries = 30;
    var delay = TimeSpan.FromSeconds(1);
    
    Console.WriteLine($"Waiting for Istio sidecar (max {maxRetries} attempts)...");
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(2);
            
            // Check Istio sidecar health endpoint
            var response = await httpClient.GetAsync("http://localhost:15021/healthz/ready");
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"✓ Istio sidecar ready after {i + 1} attempt(s) ({(i + 1) * delay.TotalSeconds}s)");
                return;
            }
            
            Console.WriteLine($"  Attempt {i + 1}/{maxRetries}: Sidecar not ready yet (Status: {response.StatusCode})");
        }
        catch (Exception ex)
        {
            if (i == 0 || (i + 1) % 5 == 0) // Log mỗi 5 lần
            {
                Console.WriteLine($"  Attempt {i + 1}/{maxRetries}: {ex.GetType().Name} - {ex.Message}");
            }
        }
        
        await Task.Delay(delay);
    }
    
    Console.WriteLine($"⚠ WARNING: Istio sidecar health check timed out after {maxRetries} attempts!");
    Console.WriteLine("  Proceeding anyway, but connectivity issues may occur...");
}