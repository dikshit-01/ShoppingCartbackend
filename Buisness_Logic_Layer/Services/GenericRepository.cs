using Data_Access_Layer.DataContext;
using Data_Access_Layer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Logic_Layer.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        readonly ContextClass _context;
        readonly DbSet<T> _dbSet;   
        public GenericRepository(ContextClass context) { 
         _context = context;
         _dbSet = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T FindById(object id)
        {
            return  _dbSet.Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
