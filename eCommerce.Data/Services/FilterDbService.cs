namespace eCommerce.Data.Services;

public class FilterDbService(ECommerceContext db, IMapper mapper) : DbService(db, mapper)
{
    public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        IncludeNavigationsFor<Filter>();
        var result = await base.GetAsync<TEntity, TDto>();
        return result;
    }
}