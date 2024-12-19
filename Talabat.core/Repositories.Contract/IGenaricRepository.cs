using Talabat.core.Entites.Common;
using Talabat.core.Specifications;

namespace Talabat.core.IRepository
{
    public interface IGenaricRepository<T>where T : BaseEntity
    {
         Task< IReadOnlyList<T>> GetAllAsync(bool withNoTracking = true); 
         Task <T?> GetByIdAsync(int Id);

         Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec, bool withNoTracking = true);
         Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountAsync(ISpecification<T> spec);
        Task AddAsync(T entity);

        Task Delete(T entity);


    }
}
