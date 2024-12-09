using POS_System.Infrastructure.Contexts;
using POS_System.Infrastructure.Repositories;
using POS_System.Interfaces;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Infrastructure
{
    public class Unitofwork : IUntiofWork
    {
        private readonly POS_SystemDBContext _dBContext;

        private Dictionary<object,object> Repositories { get; set; }

        public Unitofwork(POS_SystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async ValueTask DisposeAsync()
        {
          await  _dBContext.DisposeAsync();
        }

        public IGenaricRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var key = typeof(T);
            if(!Repositories.ContainsKey(key))
            {
                var repo = new GenaricRepository<T>(_dBContext);
                Repositories.Add(key, repo);
            }
            return Repositories[key] as IGenaricRepository<T>;
        }

        public async Task SaveChangesAsync()
        {
          await  _dBContext.SaveChangesAsync();
        }
    }
}
