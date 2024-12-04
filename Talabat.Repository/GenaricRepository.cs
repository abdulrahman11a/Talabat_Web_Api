using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites.Common;
using Talabat.core.IRepository;
using Talabat.core.Specifications;
using Talabat.Infrastructure;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenaricRepository<T>(StoreDbcontext dbcontext) : IGenaricRepository<T> where T : BaseEntity
    {
        public async Task<IReadOnlyList<T>> GetAllAsync(bool withNoTracking = true)
        {
            if (withNoTracking) return await dbcontext.Set<T>().AsNoTracking().ToListAsync();

            return dbcontext.Set<T>().ToList();
        }
        public async Task<T?> GetByIdAsync(int Id)
          =>  await dbcontext.Set<T>().FindAsync(Id);




        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec, bool withNoTracking = true)
        {
            if (withNoTracking)
            {
               return await ApplySpecification(spec).AsNoTracking().ToListAsync();
            }
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int>  GetCountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();

        }


        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpacificationEvaluator<T>.GetQuery(dbcontext.Set<T>().AsQueryable(), spec);
        }

     
    }

}
