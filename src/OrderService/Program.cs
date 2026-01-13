using OrderService.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MassTransit;
using OrderService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Polly;
using Npgsql;



var builder = WebApplication.CreateBuilder(args);
//Tao database truoc
await EnsureDatabaseExists();
// Đăng ký AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Đăng ký Controllers
builder.Services.AddControllers();

// Đăng ký DbContext
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký MassTransit **trước khi gọi builder.Build()**
builder.Services.AddMassTransit(x =>
{
    // Kích hoạt Message Outbox để đảm bảo độ tin cậy
    // x.AddEntityFrameworkOutbox<OrderDbContext>(o =>
    // {
    //     o.QueryDelay = TimeSpan.FromSeconds(10);
    //     o.UsePostgres();
    //     o.UseBusOutbox();
    // });
    x.AddConsumersFromNamespaceContaining<BuyingItemConsumer>();

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Thêm JWT Bearer handler để xử lý token
    .AddJwtBearer(option =>
    {

        option.Authority = builder.Configuration["IdentityServiceUrl"];


        option.RequireHttpsMetadata = false;

        // Bỏ qua kiểm tra audience (aud) - giúp token có thể dùng cho nhiều dịch vụ
        option.TokenValidationParameters.ValidateAudience = false;

        // Xác định tên người dùng dựa trên claim "username" trong token
        option.TokenValidationParameters.NameClaimType = "username";
    });

// Sau khi đăng ký dịch vụ xong, mới gọi `builder.Build()`
var app = builder.Build();

// Middleware
app.UseAuthentication();  // Đặt trước Authorization
app.UseAuthorization();
app.MapControllers();



// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

//     var retryPolicy = Policy
//         .Handle<Exception>()
//         .WaitAndRetry(
//             retryCount: 10,
//             sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(5),
//             onRetry: (exception, timeSpan, retry, ctx) =>
//             {
//                 Console.WriteLine($"DB not ready yet, retry {retry}: {exception.Message}");
//             }
//         );

//     retryPolicy.Execute(() =>
//     {
//         context.Database.Migrate();
        
//         DbInitializer.SeedData(context);
//     });
// }




// Khởi tạo database
// try
// {
//     DbInitializer.InitDb(app);
// }
// catch (Exception e)
// {
//     Console.WriteLine($"Database Initialization Error: {e}");
// }

//Chay migrations và seed data
await RunMigrationsAndSeed();
app.Run();
// Tạo database nếu chưa tồn tại
async Task EnsureDatabaseExists()
{
    await Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(
            retryCount: 20,
            sleepDurationProvider: attempt => TimeSpan.FromSeconds(3),
            onRetry: (ex, _, retry, _) =>
            {
                Console.WriteLine($"⏳ [{retry}/20] Waiting for PostgreSQL: {ex.Message}");
            }
        )
        .ExecuteAsync(async () =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.Database;
            
            // Kết nối postgres database
            connectionStringBuilder.Database = "postgres";
            var masterConnectionString = connectionStringBuilder.ToString();
            
            await using var masterConnection = new NpgsqlConnection(masterConnectionString);
            await masterConnection.OpenAsync();
            Console.WriteLine("✅ Connected to PostgreSQL server");
            
            // Kiểm tra database
            await using var checkCmd = new NpgsqlCommand(
                $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'", 
                masterConnection
            );
            
            var exists = await checkCmd.ExecuteScalarAsync();
            
            if (exists == null)
            {
                Console.WriteLine($"📦 Creating database '{databaseName}'...");
                await using var createCmd = new NpgsqlCommand(
                    $"CREATE DATABASE \"{databaseName}\"", 
                    masterConnection
                );
                await createCmd.ExecuteNonQueryAsync();
                Console.WriteLine($"✅ Database '{databaseName}' created!");
            }
            else
            {
                Console.WriteLine($"✅ Database '{databaseName}' exists");
            }
        });
}

// Chạy migrations và seed data
async Task RunMigrationsAndSeed()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    
    try
    {
        Console.WriteLine("🔄 Running migrations...");
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Migrations completed");
        
        if (!context.Orders.Any())
        {
            Console.WriteLine("🌱 Seeding data...");
            DbInitializer.SeedData(context);
            Console.WriteLine("✅ Data seeded");
        }
        else
        {
            Console.WriteLine("ℹ️  Database already contains data");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Migration/Seed error: {ex.Message}");
        throw;
    }
}