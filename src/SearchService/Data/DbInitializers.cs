using MongoDB.Driver;
using MongoDB.Entities;
using OrderService;
using SearchService;

public class DbInitializers
{
    public static async Task InitDb(WebApplication app)
    {
        // Khởi tạo MongoDB
        await DB.InitAsync("SearchDB", MongoClientSettings.FromConnectionString(
            app.Configuration.GetConnectionString("MongoDbConnection")));

        // Xóa dữ liệu cũ để chạy lại clean
        Console.WriteLine("Deleting existing Products and Items...");
        await DB.DeleteAsync<Product>(_ => true);
        await DB.DeleteAsync<Item>(_ => true);
        Console.WriteLine("Old data deleted.");

        // Tạo index cho tìm kiếm
        await DB.Index<Product>()
            .Key(x => x.Name, KeyType.Text)
            .CreateAsync();

        await DB.Index<Item>()
            .Key(x => x.Name, KeyType.Text)
            .Key(x => x.Category, KeyType.Text)
            .Key(x => x.Year, KeyType.Text)
            .CreateAsync();

        using var scope = app.Services.CreateScope();

        // ===== OrderService =====
        var orderHttpClient = scope.ServiceProvider.GetService<OrderSvcHttpClient>();
        var products = await orderHttpClient.GetProductForSearch();
        if (products?.Count > 0)
        {
            await DB.SaveAsync(products);
            Console.WriteLine($"{products.Count} products inserted.");
        }

        // ===== AuctionService =====
        var auctionHttpClient = scope.ServiceProvider.GetService<AuctionSvcHTTPClient>();
        var items = await auctionHttpClient.GetItemForSearchDb();
        Console.WriteLine($"{items.Count} items returned from auction service");
        if (items?.Count > 0)
        {
            await DB.SaveAsync(items);
            Console.WriteLine($"{items.Count} items inserted.");
        }
    }
}
