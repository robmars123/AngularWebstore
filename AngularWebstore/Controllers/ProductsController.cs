using DAL.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Wrappers;
using Microsoft.AspNetCore.Routing;
using Infrastructure.Automapper;
using AutoMapper;
using AutoMapper.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactWebstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> productRepository;
        public readonly IEmailService<Email> emailService;
        private readonly IProductImage<ProductImage> productImageRepository;
        private bool _processing = true;

        public ProductsController(IRepository<Product> _productRepository, 
            IEmailService<Email> _emailService, 
            IProductImage<ProductImage> _productImageRepository)
        {
            productRepository = _productRepository;
            emailService = _emailService;
            productImageRepository = _productImageRepository;
        }
        // GET: api/<Product>
        [HttpGet]
        public async Task<PagedResponse<List<ProductDTO>>> Get([FromQuery] PaginationFilter filter, string filterString = null)
        {
            var map = AutoMapperProfile.InitializeAutoMapperProfile();
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            //pull list of product_Ids instead of whole entity
            IQueryable<int> list = productRepository.All().Select(x => x.Product_Id);

            //inner join
            var pagedData = await productRepository.All().OrderBy(x=>x.Product_Id)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            //filtered
            var filteredList = new List<Product>();

            if (filterString.ToLower() == "price")
                filteredList = pagedData.OrderBy(prod => prod.Price).ToList();
            else if (filterString.ToLower() == "date_added")
                filteredList = pagedData.OrderBy(prod => prod.Date_Added).ToList();
            else
                filteredList = pagedData.ToList();

            //use Automapper
            List<ProductDTO> products = new List<ProductDTO>();
            products = map.Map<List<ProductDTO>>(filteredList);

            var totalRecords = await list.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ProductDTO>(products, validFilter, totalRecords);
            return pagedReponse;
        }
        
        [HttpGet("GetImagesAsync")]
        public async Task<IEnumerable<ProductImage>> GetImagesAsync(int productId)
        {
            IEnumerable<ProductImage> images = await productImageRepository.GetImagesAsync(productId);
            return images;
        }

        [HttpGet("GetAllImagesAsync")]
        public async Task<List<ProductImage>> GetAllImagesAsync()
        {
           IEnumerable<ProductImage> allImages = await productImageRepository.GetAllImagesAsync();
            return allImages.ToList();
        }
        // GET api/<Product>/5
        [HttpGet("{id}")]
        public ProductDTO Get(int id)
        {
            Product product = productRepository.Get(id);
            ProductDTO prod = new ProductDTO();
            prod.Product_Id = product.Product_Id;
            prod.Description = product.Description;
            prod.Price = product.Price;
            prod.Category = product.Category;
            prod.Product_Name = product.Product_Name;
            prod.Subcategory = product.Subcategory;
            prod.QuantityPerUnit = product.QuantityPerUnit;

            prod.Images = productImageRepository.GetProductImages(id);
            prod.Categories = productRepository.GetCategories();
            prod.Subcategories = productRepository.GetSubcategories();
            return prod;
        }

        // POST api/<Product> ADD
        [HttpPost]
        public void Post(Product product)
        {
            productRepository.Add(product);
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public void Put(int id, Product product)
        {
            productRepository.Update(product);
        }

        // DELETE api/<Product>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = productRepository.Get(id);
            productRepository.Delete(product);
        }
        [HttpPost("UploadCSV")]
        [Consumes("application/json")]
        public async Task UploadCSV([FromBody] string data)
        {
            await productRepository.ProcessUploadAsync(data);

            emailService.ProcessEmail();
        }

        [HttpPost]
        [Route("UploadImageAsync")]
        public async Task UploadImageAsync(int productId, [FromForm] IFormFile file)
        {
            string filePath = Path.GetTempFileName();


            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            //convers image file into byte[]

            byte[] imageData = await System.IO.File.ReadAllBytesAsync(filePath);

            ProductImage image = new ProductImage();
            image.Image = imageData;
            image.Product_Id = productId;
            await ImageUploadAsync(image);
        }

        protected async Task ImageUploadAsync(ProductImage img)
        {
            if (!_processing) return; 
            try
            {
                await productImageRepository.AddImageAsync(img);
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            finally
            {
                _processing = false;
            }
        }
    }


}
