using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Services;

public class FilterDbService<T>(T db, IMapper mapper) : DbService<T>(db, mapper) where T : DbContext
{
    public override Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        IncludeNavigationsFor<Filter>();
        return base.GetAsync<TEntity, TDto>();
    }
}