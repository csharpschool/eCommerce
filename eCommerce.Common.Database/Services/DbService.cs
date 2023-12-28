using AutoMapper;
using eCommerce.Common.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Common.Database.Services;

public class DbService<T> where T : DbContext
{
    protected readonly T _db;
    private protected readonly IMapper _mapper;

    public DbService(T db, IMapper mapper) => (_db, _mapper) = (db, mapper);

    public async Task<TDto> SingleAsync<TEntity, TDto>(int id) where TEntity : class, IEntity where TDto : class
    {
        IncludeNavigations<TEntity>();
        var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        return _mapper.Map<TDto>(entity);
    }
    public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class where TDto : class
    {
        IncludeNavigations<TEntity>();
        var entities = await _db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }
    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
    }
    public void Update<TEntity, TDto>(TDto dto) where TEntity : class, IEntity where TDto : class
    {
        // Note that this method isn't asynchronous because Update modifies
        // an already exisiting object in memory, which is very fast.
        var entity = _mapper.Map<TEntity>(dto);
        _db.Set<TEntity>().Update(entity);
    }
    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        try
        {
            var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch { return false; }

        return true;
    }
    public bool Delete<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
    {
        try
        {
            var entity = _mapper.Map<TEntity>(dto);
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch { return false; }

        return true;
    }

    public async Task<bool> SaveChangesAsync() => await _db.SaveChangesAsync() >= 0;
    public void IncludeNavigations<TEntity>() where TEntity : class
    {
        // Skip Navigation Properties are used for many-to-many
        // relationsips (ICollection) and Navigation Properties
        // are used for one-to-many relationsips.
        var entityType = _db.Model.FindEntityType(typeof(TEntity));
        if (entityType == null) return;

        var skipNavigationProperties = entityType.GetDeclaredSkipNavigations().Select(s => s.Name);
        var navigationProperties = entityType.GetNavigations().Select(s => s.Name);
        foreach (var name in navigationProperties.Union(skipNavigationProperties))
            _db.Set<TEntity>().Include(name).Load();
    }

}