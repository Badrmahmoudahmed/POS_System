using Microsoft.EntityFrameworkCore;
using POS_System.Infrastructure.Contexts;
using POS_System.Infrastructure.SpecificatoinDP;
using POS_System.Interfaces;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Infrastructure.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly POS_SystemDBContext _dBContext;

        public GenaricRepository(POS_SystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task AddAsync(T entity)
        {
           await _dBContext.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
           await Task.Run(() => _dBContext.Remove(entity));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dBContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithSpec(Specification<T> spec)
        {
          return await SpecificationEvaluator<T>.GetQuery(_dBContext.Set<T>(), spec).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
          return await _dBContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetAsyncWithSpec(Specification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dBContext.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
           _dBContext.Update(entity);
        }
    }
}
