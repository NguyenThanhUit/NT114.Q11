using AuctionService;
using AuctionService.Data;

using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polly;


var builder = WebApplication.CreateBuilder(args);
//Tao database truoc
await EnsureDatabaseExists();

builder.Services.AddControllers();
//Thêm mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AuctionDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AuctionFinishedConsumer>();
    //Message outbox có tác dụng lưu trữ message khi service bus down
    // x.AddEntityFrameworkOutbox<AuctionDbContext> (o =>{
    //     o.QueryDelay = TimeSpan.FromSeconds(10);
    //     o.UsePostgres();
    //     o.UseBusOutbox();
    // });

    x.UsingRabbitMq((context, cfg) =>
    {

        //Thêm để dùng khi chạy Dockerfile
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

//Add authenticate(Identity Service)
// Cấu hình xác thực bằng JWT Bearer Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Thêm JWT Bearer handler để xử lý token
    .AddJwtBearer(option =>
    {

        option.Authority = builder.Configuration["IdentityServiceUrl"];


        option.RequireHttpsMetadata = false;


        option.TokenValidationParameters.ValidateAudience = false;

        option.TokenValidationParameters.NameClaimType = "username";
    });


builder.Services.AddGrpc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<GrpcAuctionService>();

builder.WebHost.UseUrls("http://*:80");


app.MapControllers();

//Retry dung trong k8s
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

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
//         DBInitializer.SeedData(context);
//     });
// }


// Ket noi den DB
// try
// {
//     DBInitializer.InitDb(app);
// }
// catch (Exception e)
// {
//     Console.WriteLine(e.Message);
// }
await RunMigrationsAndSeed();
app.Run();
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
    var context = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
    
    try
    {
        Console.WriteLine("🔄 Running migrations...");
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Migrations completed");
        
        if (!context.Auctions.Any())
        {
            Console.WriteLine("🌱 Seeding data...");
            DBInitializer.SeedData(context);
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