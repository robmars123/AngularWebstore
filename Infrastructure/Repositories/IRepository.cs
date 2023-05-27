using DAL.DTO;
using DAL.Models;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> : IProductUploadProcessor, ICategory, ISubcategory
    {
        void Add(T entity);
        void Update(T entity);
        T Get(int id);
        void Delete(T entity);
        IQueryable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
