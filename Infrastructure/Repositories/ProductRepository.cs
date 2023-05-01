using DAL;
using DAL.DTO;
using DAL.Models;
using Infrastructure.Automapper;
using Infrastructure.EmailService;
using Infrastructure.Processors;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductUploadProcessor
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
            if(entity != null)
            {
                context.Products.Remove(entity);
                SaveChanges();
            }
        }
        public override IEnumerable<Product> All()
        {
            return context.Products.ToList();
        }

        public override Product Get(int id)
        {
            return context.Products.Find(id);
        }

        public async override void ProcessUpload(string data)
        {
            _processor = new ProductUploadProcessor();
            var records = _processor.Process(data);

            context.Products.AddRange(records);
            SaveChanges();
        }
    }
}
