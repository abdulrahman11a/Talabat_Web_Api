using Talabat.core.Entites.Common;
using Talabat.core.Specifications;

namespace Talabat.core.IRepository
{
    public interface IGenaricRepository<T>where T : BaseEntity
    {
         Task< IReadOnlyList<T>> GetAllAsync(bool withNoTracking = true); 
         Task <T?> GetByIdAsync(int Id);

         Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec, bool withNoTracking = true);
         Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<int> GetCountAsync(ISpecification<T> spec);
    }
}
