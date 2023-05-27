using DAL;
using DAL.Models;
using Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Products
{
    public class ProductImageRepository : GenericProductImageRepository<ProductImage>
    {
        private readonly StoreDBContext context;

        public ProductImageRepository(StoreDBContext _context) : base(_context)
        {
            context = _context;
        }

        public override IQueryable<ProductImage> GetProductImages(int id)
        {
            return context.ProductImages.Where(x=>x.Product_Id == id).AsQueryable();
        }

        public override async Task AddImageAsync(ProductImage img)
        {
                await context.ProductImages.AddAsync(img);
                context.SaveChanges();
        }
        public override async Task<IEnumerable<ProductImage>> GetImagesAsync(int productId)
        {
            List<ProductImage> img = await context.ProductImages.Where(x => x.Product_Id == productId).Select(y => y).ToListAsync();
            foreach (var item in img)
            {
                string imreBase64Data = Convert.ToBase64String(item.Image);
                item.ConvertedProductImage = string.Format("data:image/png;base64,{0}", imreBase64Data);
            }
            return img;
        }

        public override async Task<IEnumerable<ProductImage>> GetAllImagesAsync()
        {
            var allImages = context.ProductImages.AsQueryable();
            return await allImages.ToListAsync();
        }
    }
}
