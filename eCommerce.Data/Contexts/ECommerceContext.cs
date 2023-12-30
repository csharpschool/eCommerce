namespace eCommerce.Data.Contexts;

public class ECommerceContext(DbContextOptions<ECommerceContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Filter> Filters => Set<Filter>();
    public DbSet<Option> Options => Set<Option>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CategoryFilter> CategoryFilters => Set<CategoryFilter>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region CategoryFilter Many-to-Many Relationship
        builder.Entity<CategoryFilter>()
            .HasKey(cf => new { cf.CategoryId, cf.FilterId }); // Composite key

        builder.Entity<Category>()
            .HasMany(c => c.Filters)
            .WithMany(f => f.Categories)
            .UsingEntity<CategoryFilter>();
        #endregion

        #region OptionFilter One-to-Many Relationship
        builder.Entity<Filter>()
            .HasMany(c => c.Options)
            .WithOne(f => f.Filter);
        #endregion

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

