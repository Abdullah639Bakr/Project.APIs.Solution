using Project.Core.Entities;
using Project.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public interface IUnitOfWork
    {
        Task <int> CompleteAsync();
        IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
