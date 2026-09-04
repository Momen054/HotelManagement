using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.Repositories.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelManagement.Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelManagementContext _context;

        private readonly DbSet<T> _dbSet;

        public GenericRepository(HotelManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task PostAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public async Task<IEnumerable<T>> GetAllAsunc(Expression<Func<T,bool>>? pred=null)
        {
            IQueryable<T> query = _dbSet;

            if(pred != null)
                query = query.Where(pred);
            return await query.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>>? pred = null)
        {
            IQueryable<T> query = _dbSet;

            if (pred != null)
                query = query.Where(pred);
            return await query.FirstOrDefaultAsync();
        }
        public void Put(T entity)
           => _dbSet.Update(entity);

        public void Delete(int id)
        {
            var obj = _dbSet.Find(id);
            if (obj != null)
                _dbSet.Remove(obj);
            else
                throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            var obj = _dbSet.Find(id);
            if (obj != null)
                _dbSet.Remove(obj);
            else
                throw new NotImplementedException();
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj);
        }

    }
}
