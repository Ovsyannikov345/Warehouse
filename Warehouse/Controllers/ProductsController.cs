using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ProductsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductGetDto>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            return await _context.Products.Select(prod => new ProductGetDto
            {
                Id = prod.Id,
                Name = prod.Name,
                Department = new DepartmentGetDto
                {
                    Id = prod.Department!.Id,
                    Name = prod.Department.Name,
                }
            }).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDto>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(prod => prod.Department).FirstOrDefaultAsync(prod => prod.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Department = new DepartmentGetDto
                {
                    Id = product.Department!.Id,
                    Name = product.Department.Name,
                }
            };
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductPutDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.Include(prod => prod.Department)
                                                 .FirstOrDefaultAsync(prod => prod.Id ==  productDto.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDto.Name;
            product.DepartmentId = productDto.DepartmentId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductPostDto product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationContext.Products'  is null.");
            }

            var createdProduct = new Product
            {
                Name = product.Name,
                DepartmentId = product.DepartmentId,
            };

            _context.Products.Add(createdProduct);
            await _context.SaveChangesAsync();

            return Ok(createdProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
