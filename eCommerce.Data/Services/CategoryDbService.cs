using eCommerce.API.DTO;
using eCommerce.Data.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eCommerce.Data.Services;

public class CategoryDbService(ECommerceContext db, IMapper mapper) : DbService(db, mapper)
{
    public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        await GetCategoriesWithAllRelatedDataAsync();
        IncludeNavigationsFor<Filter>();
        IncludeNavigationsFor<Product>();
        var result = await base.GetAsync<TEntity, TDto>();
        return result;
    }

    public async Task<List<CategoryGetDTO>> GetCategoriesWithAllRelatedDataAsync()
    {
        IncludeNavigationsFor<Filter>();
        IncludeNavigationsFor<Product>();
        var categories = await base.GetAsync<Category, CategoryGetDTO>();

        foreach (var category in categories)
        {
            if (category is null || category.Filters is null) continue;

            foreach (var filter in category.Filters)
            {
                filter.Options = [];

                var dbSetProperty = db.GetType().GetProperties()
                    .FirstOrDefault(p => p.Name.Equals(filter.Name, StringComparison.OrdinalIgnoreCase) &&
                        p.PropertyType.IsGenericType &&
                        p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

                if (dbSetProperty is null) continue;

                // Retrieve the DbSet and cast it to IQueryable<object>
                var dbSet = (IQueryable<object>?)dbSetProperty.GetValue(db);

                if (dbSet is null) continue;

                var data = await dbSet.ToListAsync();

                filter.Options = _mapper.Map<List<OptionDTO>>(data);
            }
        }

        return categories;
    }
}