using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Contexts;

public class ECommerceContext(DbContextOptions<ECommerceContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Filter> Filters => Set<Filter>();
    public DbSet<Size> Sizes => Set<Size>();
    public DbSet<Color> Colors => Set<Color>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CategoryFilter> CategoryFilters => Set<CategoryFilter>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<ProductSize> ProductSizes => Set<ProductSize>();
    public DbSet<ProductColor> ProductColors => Set<ProductColor>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Composite Keys
        builder.Entity<ProductColor>()
            .HasKey(pc => new { pc.ProductId, pc.ColorId });
        builder.Entity<ProductSize>()
            .HasKey(ps => new { ps.ProductId, ps.SizeId });
        builder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });
        builder.Entity<CategoryFilter>()
            .HasKey(cf => new { cf.CategoryId, cf.FilterId });
        #endregion

        #region CategoryFilter Many-to-Many Relationship

        builder.Entity<Category>()
            .HasMany(c => c.Filters)
            .WithMany(f => f.Categories)
            .UsingEntity<CategoryFilter>();
        #endregion

        /*#region OptionFilter One-to-Many Relationship
        builder.Entity<Filter>()
            .HasMany(c => c.Options)
            .WithOne(f => f.Filter);
        #endregion

        #region FilterType One-to-Many Relationship
        builder.Entity<FilterType>()
            .HasMany(ft => ft.Filters)
            .WithOne(f => f.FilterType);
        #endregion*/

        #region ProductCategory Many-to-Many Relationship
        builder.Entity<ProductCategory>()
               .HasKey(cf => new { cf.CategoryId, cf.ProductId }); // Composite key

        builder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<ProductCategory>();
        #endregion
    }
}

