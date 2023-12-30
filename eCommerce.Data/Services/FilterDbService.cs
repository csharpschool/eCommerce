namespace eCommerce.Data.Services;

public class FilterDbService<T>(T db, IMapper mapper) : DbService<T>(db, mapper) where T : DbContext
{
    public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        IncludeNavigationsFor<Filter>();
        var result = await base.GetAsync<TEntity, TDto>();
        return result;
    }
}