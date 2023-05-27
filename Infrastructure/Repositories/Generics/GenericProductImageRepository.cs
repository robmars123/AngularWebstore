using DAL;
using DAL.Models;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generics
{
    public class GenericProductImageRepository<T> : IProductImage<T> where T : class
    {
        private readonly StoreDBContext context;

        public GenericProductImageRepository(StoreDBContext _context)
        {
            context = _context;
        }
        public virtual IQueryable<T> GetProductImages(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task AddImageAsync(ProductImage img)
        {

        }
        public virtual async Task<IEnumerable<ProductImage>> GetImagesAsync(int productId)
        {
            throw new NotImplementedException();
        }
        public virtual async Task<IEnumerable<ProductImage>> GetAllImagesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
