using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites.Common;
using Talabat.core.IRepository;

namespace Talabat.core.Repositories.Contract
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IGenaricRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;

       Task<int> CompleteAsync();
    }
}
