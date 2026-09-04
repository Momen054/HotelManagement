using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelManagement.Repositories.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task PostAsync(T entity);

        Task<IEnumerable<T>> GetAllAsunc(Expression<Func<T, bool>>? pred = null);

        Task<T?> GetByIdAsync(Expression<Func<T, bool>>? pred = null);

        void Put(T entity);

        void Delete(int id);

        void Delete(string id);

        void Delete(T obj);



    }
}
