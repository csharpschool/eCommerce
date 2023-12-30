using eCommerce.API.DTO;

namespace eCommerce.Data.Services;

public class ProductDbService(ECommerceContext db, IMapper mapper) : DbService(db, mapper)
{
    public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        IncludeNavigationsFor<Category>();
        var result = await base.GetAsync<TEntity, TDto>();
        return result;
    }
    public async Task<List<ProductGetDTO>> GetAsync(int categoryId)
    {
        var productIds = GetAsync<ProductCategory>(pc => pc.CategoryId.Equals(categoryId))
            .Select(pc => pc.ProductId);
        var products = await GetAsync<Product>(p => productIds.Contains(p.Id)).ToListAsync();
        return MapList<Product, ProductGetDTO>(products);
    }

}