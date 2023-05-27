using DAL;
using DAL.DTO;
using DAL.Models;
using Infrastructure.Automapper;
using Infrastructure.Processors;
using Infrastructure.Repositories.Generics;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductUploadProcessor, ICategory, ISubcategory
    {
        private ProductUploadProcessor _processor;
        private readonly StoreDBContext context;

        public ProductRepository(StoreDBContext _context) : base(_context)
        {
            context = _context;
        }
        public override void Update(Product product)
        {
            var prod = context.Products.Where(item => item.Product_Id == product.Product_Id).SingleOrDefault();

            if (prod != null)
            {
                prod.Product_Name = product.Product_Name;
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.Category_id = product.Category_id;
                prod.Subcategory_id = product.Subcategory_id;
                prod.QuantityPerUnit = product.QuantityPerUnit;

                base.Update(prod);
                SaveChanges();
            }
        }
        public override void Add(Product entity)
        {
                base.Add(entity);
                SaveChanges();
        }

        public override void Delete(Product entity)
        {
            if (entity != null)
            {
                context.Products.Remove(entity);
                SaveChanges();
            }
        }
        public override IQueryable<Product> All()
        {
            return context.Products.AsQueryable();
        }

        public override Product Get(int id)
        {
            var item = context.Products.Find(id);
            return item;
        }

        public override async Task ProcessUploadAsync(string data)
        {
            _processor = new ProductUploadProcessor();
            var records = _processor.Process(data);

            await context.Products.AddRangeAsync(records);
            SaveChanges();
        }

        //todo: make separate repository
        public override IEnumerable<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public override IEnumerable<Subcategory> GetSubcategories()
        {
            return context.Subcategories.ToList();
        }
    }
}
