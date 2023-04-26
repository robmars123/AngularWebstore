using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactWebstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDBContext storeDBContext;

        public ProductsController(StoreDBContext _storeDBContext)
        {
            storeDBContext = _storeDBContext;
        }
        // GET: api/<Product>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return storeDBContext.Products.ToList();
        }

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Product>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Product>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
