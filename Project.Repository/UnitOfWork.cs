using Project.Core;
using Project.Core.Entities;
using Project.Core.Repositories.Contract;
using Project.Repository.Data.Contexts;
using Project.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type)) 
            {
                var repository = new GenericRepository<TEntity, Tkey>(_context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity, Tkey>;
        }
    }
}
