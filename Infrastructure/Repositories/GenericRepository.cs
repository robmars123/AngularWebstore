using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected StoreDBContext context;
        public GenericRepository(StoreDBContext _storeContext)
        {
            this.context = _storeContext;
        }
        public virtual void Add(T entity)
        {
             context.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            context.Remove(entity);
        }
        public virtual IEnumerable<T> All()
        {
            return context.Set<T>().ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public virtual T Get(int id)
        {
            return context.Find<T>(id);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            context.Update(entity);
        }

        public virtual void ProcessUpload(string data)
        {

        }
    }
}
