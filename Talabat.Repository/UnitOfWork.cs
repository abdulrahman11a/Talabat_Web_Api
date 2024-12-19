using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.core.Entites.Common;
using Talabat.core.IRepository;
using Talabat.core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Infrastructure
{
    public class UnitOfWork (StoreDbcontext _dbContext): IUnitOfWork
    {
        private readonly Hashtable _repositories = new();


        public IGenaricRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repositoryInstance = new GenaricRepository<TEntity>(_dbContext);
                _repositories[key] = repositoryInstance;
            }

            return (IGenaricRepository<TEntity>)_repositories[key];
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
    //    private  Dictionary<string, GenaricRepository<BaseEntity>> repos;

    //   public async Task<int> CompleteAsync()

    //     => await  _dbContext.SaveChangesAsync();


    //    public async void Dispose()

    //      => _dbContext.Dispose();

    //    public async ValueTask DisposeAsync()
    //   =>await _dbContext.DisposeAsync();   

    //    public IGenaricRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
    //    {
    //        var type= typeof(TEntity).Name;
    //        if (!repos.ContainsKey(type))
    //        {

    //            var repositoryInstance = new GenaricRepository<TEntity>(_dbContext) as GenaricRepository<BaseEntity>;

    //            repos.Add(type,repositoryInstance);
    //        }

    //        return repos[type] as IGenaricRepository<TEntity>;




    //    }
    //}

}
