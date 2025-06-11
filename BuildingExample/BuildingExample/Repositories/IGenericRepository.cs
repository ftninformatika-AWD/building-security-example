namespace BuildingExample.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetOne(int id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        void Dispose();
    }
}
