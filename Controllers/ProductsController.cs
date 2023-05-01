using DAL.Models;
using Infrastructure.EmailService;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactWebstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public readonly IEmailService<Email> emailService;

        public ProductsController(IRepository<Product> productRepository, IEmailService<Email> _emailService)
        {
            _productRepository = productRepository;
            emailService = _emailService;
        }
        // GET: api/<Product>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepository.All();
        }

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productRepository.Get(id);
        }

        // POST api/<Product> ADD
        [HttpPost]
        public void Post(Product product)
        {
            _productRepository.Add(product);
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public void Put(int id, Product product)
        {
            _productRepository.Update(product);
        }

        // DELETE api/<Product>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = _productRepository.Get(id);
            _productRepository.Delete(product);
        }
        [HttpPost("UploadCSV")]
        [Consumes("application/json")]
        public async Task UploadCSV([FromBody] string data)
        {
            _productRepository.ProcessUpload(data);

            emailService.ProcessEmail();
        }
    }


}
