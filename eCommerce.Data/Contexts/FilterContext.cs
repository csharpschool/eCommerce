namespace eCommerce.Data.Contexts;

public class FilterContext(DbContextOptions<FilterContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Filter> Filters => Set<Filter>();
    public DbSet<Option> Options => Set<Option>();
    public DbSet<CategoryFilter> CategoryFilters => Set<CategoryFilter>();

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

        builder.Entity<Filter>()
            .HasMany(c => c.Options)
            .WithOne(f => f.Filter);
        #endregion
    }
}
