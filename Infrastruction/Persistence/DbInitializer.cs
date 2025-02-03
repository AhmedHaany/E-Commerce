using Persistence.Data;
using System.Text.Json;

namespace Persistence
{
	public class DbInitializer : IDbInitializer
	{
		private readonly StoreContext _storeContext;

		public DbInitializer(StoreContext storeContext)
		{
			this._storeContext = storeContext;
		}
		public async Task InitializeAsync()
		{
			try
			{
				if (_storeContext.Database.GetPendingMigrations().Any())
					await _storeContext.Database.MigrateAsync();

				if (!_storeContext.ProductTypes.Any())
				{
					// Read Types from file as string
					var typesData = await File.ReadAllTextAsync(@"..\Infrastruction\Persistence\Data\Seeding\types.json");

					// Transform into C# object
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

					if (types is not null && types.Any())
					{
						await _storeContext.ProductTypes.AddRangeAsync(types);
						await _storeContext.SaveChangesAsync();
					}
				}
				if (!_storeContext.ProductBrands.Any())
				{
					// Read Brands from file as string
					var brandsData = await File.ReadAllTextAsync(@"..\Infrastruction\Persistence\Data\Seeding\brands.json");

					// Transform into C# object
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

					if (brands is not null && brands.Any())
					{
						await _storeContext.ProductBrands.AddRangeAsync(brands);
						await _storeContext.SaveChangesAsync();
					}
				}
				if (!_storeContext.Products.Any())
				{
					// Read Products from file as string
					var productsData = await File.ReadAllTextAsync(@"..\Infrastruction\Persistence\Data\Seeding\products.json");

					// Transform into C# object
					var products = JsonSerializer.Deserialize<List<Product>>(productsData);

					if (products is not null && products.Any())
					{
						await _storeContext.Products.AddRangeAsync(products);
						await _storeContext.SaveChangesAsync();
					}
				}
			}
			catch (Exception)
			{
				throw;	
			}
			
		}
	}
}
//..\Infrastruction\Persistence\Data\Seeding\types.json