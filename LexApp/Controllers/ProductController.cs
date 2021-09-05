using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexApp.Models;

namespace LexApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        ApplicationContext _appcontext;

        public ProductController(ApplicationContext context)
        {
            _appcontext = context;
            if (!_appcontext.Products.Any())
            {
                _appcontext.Products.Add(new Product { Name = "iPhone X", Company = "Apple", Price = 79900 });
                _appcontext.Products.Add(new Product { Name = "Galaxy S8", Company = "Samsung", Price = 49900 });
                _appcontext.Products.Add(new Product { Name = "Pixel 2", Company = "Google", Price = 52900 });
                _appcontext.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _appcontext.Products.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {

            return _appcontext.Products.FirstOrDefault(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(Product prod)
        {
            if (ModelState.IsValid)
            {
                _appcontext.Products.Add(prod);
                _appcontext.SaveChanges();
                return Ok(prod);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Put(Product prod)
        {
            if (ModelState.IsValid)
            {
                _appcontext.Products.Update(prod);
                _appcontext.SaveChanges();
                return Ok(prod);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product prod = _appcontext.Products.FirstOrDefault(t => t.Id == id);
            if (prod != null)
            {
                _appcontext.Products.Remove(prod);
                _appcontext.SaveChanges();
                return Ok(prod);
            }
            else
                return BadRequest(prod);
        }

    }
}
