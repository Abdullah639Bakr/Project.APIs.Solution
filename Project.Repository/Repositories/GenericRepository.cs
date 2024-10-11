﻿using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using Project.Core.Repositories.Contract;
using Project.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext _context;

        public GenericRepository( AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product)) 
            {
                return (IEnumerable<TEntity>) await _context.Products.Include(p=>p.Brand)
                                                                     .Include(p=>p.Type).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(p => p.Brand)
                                              .Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

       

        
    }
}