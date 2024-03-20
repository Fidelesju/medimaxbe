namespace MediMax.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entidade);
        Task CreateAsync(List<T> entidadeLista);

        Task UpdateAsync(T entidade);
        // Task RemoveAsync(T entidade);
        // Task<List<T>> GetAllAsync();
    }
}