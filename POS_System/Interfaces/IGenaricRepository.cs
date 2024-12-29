using POS_System.Infrastructure.SpecificatoinDP;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Interfaces
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);
        T GetWithSpec(Specification<T> spec);
        Task<T> GetAsyncWithSpec(Specification<T> spec);
        List<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsyncWithSpec(Specification<T> spec);
        IEnumerable<T> GetAllWithSpec(Specification<T> spec);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        void Update(T entity);
    }
}
