using Azure;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Contexts;

public class FilterContext(DbContextOptions<FilterContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Filter> Statuses => Set<Filter>();
    public DbSet<CategoryFilter> CategoryFilters => Set<CategoryFilter>();
    public DbSet<FilterOption> FilterOptions => Set<FilterOption>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region CategoryFilter Many-to-Many Relationship
        builder.Entity<Category>()
            .HasMany(c => c.Filters)
            .WithMany(f => f.Categorys)
            .UsingEntity<CategoryFilter>(
                f => f.HasOne(cf => cf.Filter).WithMany().HasForeignKey(cf => cf.CategoryId),
                c => c.HasOne(cf => cf.Category).WithMany().HasForeignKey(cf => cf.FilterId)
            );

        builder.Entity<Filter>()
            .HasMany(f => f.Categorys)
            .WithMany(c => c.Filters)
            .UsingEntity<CategoryFilter>(
                c => c.HasOne(cf => cf.Category).WithMany().HasForeignKey(cf => cf.FilterId),
                f => f.HasOne(cf => cf.Filter).WithMany().HasForeignKey(cf => cf.CategoryId)
            );
        #endregion

        #region FilterOption Many-to-Many Relationship
        builder.Entity<Filter>()
            .HasMany(f => f.Options)
            .WithMany(o => o.Filters)
            .UsingEntity<FilterOption>(
                o => o.HasOne(of => of.Option).WithMany().HasForeignKey(of => of.FilterId),
                f => f.HasOne(of => of.Filter).WithMany().HasForeignKey(of => of.OptionId)
            );

        builder.Entity<Option>()
            .HasMany(o => o.Filters)
            .WithMany(f => f.Options)
            .UsingEntity<FilterOption>(
                f => f.HasOne(of => of.Filter).WithMany().HasForeignKey(of => of.OptionId),
                o => o.HasOne(of => of.Option).WithMany().HasForeignKey(of => of.FilterId)
            );
        #endregion
    }
}
