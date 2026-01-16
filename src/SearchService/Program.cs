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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

//Thêm mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// //Thêm HTTP client
builder.Services.AddHttpClient<OrderSvcHttpClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
})
.AddPolicyHandler(GetPolicy());

builder.Services.AddHttpClient<AuctionSvcHTTPClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
})
.AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
    //Them consumer
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

// app.Lifetime.ApplicationStarted.Register(async () =>
// {
//     //Dung cho k8s
//     await Policy.Handle<TimeoutException>()
//         .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(10))
//         .ExecuteAndCaptureAsync(async () => await DbInitializers.InitDb(app));


//     //Dung cho Docker
//     // try
//     // {
//     //     await DbInitializers.InitDb(app);
//     // }
//     // catch (Exception e)
//     // {
//     //     Console.WriteLine(e.Message);
//     // }
// });
app.Lifetime.ApplicationStarted.Register(async () =>
{
    
    await Policy.Handle<TimeoutException>()
        .Or<HttpRequestException>()
        .WaitAndRetryAsync(
            retryCount: 5,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(5),
            onRetry: (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"[DbInitializer] Retry #{retryCount} after {timeSpan.TotalSeconds}s due to: {exception.Message}");
            }
        )
        .ExecuteAndCaptureAsync(async () => await DbInitializers.InitDb(app));
});
app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryAsync(
            retryCount: 3,  // Chỉ retry 3 lần
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(2),
            onRetry: (outcome, timespan, retryCount, context) =>
            {
                var message = outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString();
                Console.WriteLine($"[HttpClient] Request failed: {message}. Waiting {timespan.TotalSeconds}s before retry #{retryCount}");
            }
        );

// static IAsyncPolicy<HttpResponseMessage> GetPolicy()
//     => HttpPolicyExtensions
//         .HandleTransientHttpError()

//         .OrResult(static msg => msg.StatusCode == HttpStatusCode.NotFound)

//         .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));

