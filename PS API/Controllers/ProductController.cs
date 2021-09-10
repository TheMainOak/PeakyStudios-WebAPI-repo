using PS_API.Models;
using PS_API.Services;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System; 

namespace PS_API
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {

        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts() => ProductService.GetAll();     //GET all action

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)       //GET by ID action
        {
            if (ProductService.GetById(id) == null)
            {
                return NotFound(id);
            }
            return ProductService.GetById(id);
        }   

        [HttpPost]
        public IActionResult Create(Product p)    
        {
            ProductService.Add(p);
            return CreatedAtAction(nameof(Create), new {ID = p.Id}, p);
        }   

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product p)    //PUT action
        {
            if(id != p.Id)
            {
                return BadRequest(id);
            }

            var existingProduct = ProductService.GetById(id);
            if(existingProduct is null)
            {
                return NotFound("Unlucky, not found cousin.");
            }

            ProductService.Update(p);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) //DELETE action
        {
            var p = ProductService.GetById(id);

            if(p is null)
            {
                return NotFound();
            }

            ProductService.Delete(id);
            
            return NoContent();
        }
    }
}