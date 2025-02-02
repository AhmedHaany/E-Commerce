
namespace Persistence.Data.Configurations
{
	internal class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(Product => Product.ProductBrand)
				.WithMany()
				.HasForeignKey(Product => Product.BrandId);

			builder.HasOne(Product => Product.ProductType)
				.WithMany()
				.HasForeignKey(Product => Product.TypeId);

			builder.Property(Product => Product.Price)
				.HasColumnType("decimal(18,3)");
		}
	}
}
