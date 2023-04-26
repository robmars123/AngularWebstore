using AutoMapper;
using DAL.DTO;
using DAL.Models;
using Infrastructure.Automapper;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactWebstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        
        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
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
        }
    }
}
