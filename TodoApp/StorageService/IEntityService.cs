namespace TodoApp.StorageService
{
    public interface IEntityService<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
