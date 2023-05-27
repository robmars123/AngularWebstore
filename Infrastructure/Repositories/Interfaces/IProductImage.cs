using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IProductImage<T>
    {
        IQueryable<T> GetProductImages(int id);
        Task AddImageAsync(ProductImage img);
        Task<IEnumerable<ProductImage>> GetImagesAsync(int productId);
        Task<IEnumerable<ProductImage>> GetAllImagesAsync();
    }
}
