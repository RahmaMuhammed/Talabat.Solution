using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Generic_Repository.Data;
using Talabat.Repository.GenericRepository;

namespace Talabat.Repository.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)  //Ask CLR for creating object from DbContext Implicitly
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
             // if (typeof(T) == typeof(Product))
            //  return (IReadOnlyList<T>)await _dbContext.Set<Product>().skip(20).take(10).Include(p => p.Brand).Include(P => P.Category).ToListAsync();
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).AsNoTracking().ToListAsync(); ;
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
                return await _dbContext.Set<Product>().Where(P => P.Id == id).Include(p => p.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> GetCountAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).CountAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        private  IQueryable<T> ApplySpecification(ISpecifications<T> Spec)
        {
            return  SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }
    }
}
