using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using VNPAY.NET;
using WalletService;
using WalletService.Consumers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IVnpay>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var vnpayConfig = config.GetSection("Vnpay");

    Console.WriteLine("[VNPAY INIT] Starting initialization...");

    var tmnCode = vnpayConfig["TmnCode"];
    var hashSecret = vnpayConfig["HashSecret"];
    var baseUrl = vnpayConfig["BaseUrl"];
    var callbackUrl = vnpayConfig["CallbackUrl"];

    Console.WriteLine($"[VNPAY INIT] TmnCode: {tmnCode}");
    Console.WriteLine($"[VNPAY INIT] HashSecret: {(string.IsNullOrEmpty(hashSecret) ? "NULL" : "SET")}");
    Console.WriteLine($"[VNPAY INIT] BaseUrl: {baseUrl}");
    Console.WriteLine($"[VNPAY INIT] CallbackUrl: {callbackUrl}");

    try
    {
        var vnpay = new Vnpay();
        vnpay.Initialize(tmnCode!, hashSecret!, baseUrl!, callbackUrl!, "2.1.0", "other");
        Console.WriteLine("[VNPAY INIT] Initialization successful!");
        return vnpay;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[VNPAY INIT] Initialization failed: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
        throw; // vẫn ném để app biết có lỗi
    }
});



builder.Services.AddMassTransit(x =>
{

    x.AddConsumersFromNamespaceContaining<BuyingItemConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionFinishedConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionKeyConfirmedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("deposits", false));

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


// Đăng ký các dịch vụ
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.Authority = builder.Configuration["IdentityServiceUrl"];
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters.ValidateAudience = false;
        option.TokenValidationParameters.NameClaimType = "username";
    });

// Sau khi cấu hình xong services, mới được gọi Build()
var app = builder.Build();

await Policy.Handle<TimeoutException>()
    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(10))
    .ExecuteAndCaptureAsync(async () =>
    {
        // Khởi tạo MongoDB.Entities
        await DB.InitAsync("DepositDb", MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("DepositDbConnection")));

    });



// // Khởi tạo MongoDB.Entities
// await DB.InitAsync("DepositDb", MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("DepositDbConnection")));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
