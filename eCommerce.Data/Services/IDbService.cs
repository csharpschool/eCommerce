namespace eCommerce.Data.Services
{
    public interface IDbService
    {
        Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
            where TEntity : class
            where TDto : class;
        bool Delete<TEntity, TDto>(TDto dto)
            where TEntity : class
            where TDto : class;
        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;
        Task<List<TDto>> GetAsync<TEntity, TDto>()
            where TEntity : class
            where TDto : class;
        void IncludeNavigationsFor<TEntity>() where TEntity : class;
        Task<bool> SaveChangesAsync();
        Task<TDto> SingleAsync<TEntity, TDto>(int id)
            where TEntity : class, IEntity
            where TDto : class;
        void Update<TEntity, TDto>(TDto dto)
            where TEntity : class, IEntity
            where TDto : class;
    }
}