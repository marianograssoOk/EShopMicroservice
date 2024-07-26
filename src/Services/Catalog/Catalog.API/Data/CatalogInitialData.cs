using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync()) return;

        session.Store(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => [ new Product() 
    { 
        Id = new Guid(),
        Name = "Iphone X",
        Description = "Test",
        ImageFile = "product1.jpg",
        Price = 950.00M,
        Category = ["Smart Phone"]
    } ];
}
