using eCommerce.API.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace eCommerce.Data.Services;

public class FilterDbService(ECommerceContext db, IMapper mapper) : DbService(db, mapper)
{
    public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        IncludeNavigationsFor<Filter>();
        var result = await base.GetAsync<TEntity, TDto>();
        return result;
    }

    public async Task<List<ProductGetDTO>> FilterProducts(List<FilterRequestDTO> filters)
    {
        if (filters == null || !filters.Any())
            throw new ArgumentNullException(nameof(filters), "Filters cannot be null or empty.");

        var firstFilter = filters.FirstOrDefault() ?? throw new ArgumentNullException("Missing filters.");

        foreach (var filter in filters)
            db.Set<Product>().Include(filter.TypeName).Load();

        // Start with a query that gets all products that match the first filter's category.
        IQueryable<Product> query = db.Products
            .Where(p => p.Categories.Any(pc => pc.Id == firstFilter.CategoryId));

        // Build the query for each filter
        foreach (var filter in filters.Where(f => f.OptionType == OptionType.Checkbox && f.Options != null))
        {
            // Get the IDs for the current filter options
            var optionIds = filter.Options.Select(o => o.Id).ToArray();

            // Create a parameter expression representing a Product entity
            var productParam = Expression.Parameter(typeof(Product), "product");

            // Get the navigation property info using the filter TypeName
            var navigationPropertyInfo = typeof(Product).GetProperty(filter.TypeName);

            if (navigationPropertyInfo != null)
            {
                // Create an expression to access the collection property of Product
                var collectionPropertyExp = Expression.Property(productParam, navigationPropertyInfo.Name);

                // Get the 'Contains' method for an integer
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));

                // Build an expression tree to represent the predicate inside the Any method
                var innerParam = Expression.Parameter(navigationPropertyInfo.PropertyType.GenericTypeArguments[0], "i");
                var innerProperty = Expression.Property(innerParam, "Id");
                var optionsExpression = Expression.Constant(optionIds, typeof(IEnumerable<int>));
                    // Create the inner lambda expression for the Any method
                    var innerLambda = Expression.Lambda(
                        Expression.Call(typeof(Enumerable), "Contains", new Type[] { typeof(int) }, optionsExpression, innerProperty),
                        innerParam);

                    // Build the Any lambda expression
                    var anyLambda = Expression.Call(typeof(Enumerable), "Any", new Type[] { navigationPropertyInfo.PropertyType.GenericTypeArguments[0] }, collectionPropertyExp, innerLambda);

                    // Apply the lambda to the query
                    var finalLambda = Expression.Lambda<Func<Product, bool>>(anyLambda, productParam);
                    query = query.Where(finalLambda);
            }
        }

        // Execute the query and map the results to DTOs
        var products = await query.ToListAsync();
        var productDtos = _mapper.Map<List<ProductGetDTO>>(products);

        return productDtos;
    }
}