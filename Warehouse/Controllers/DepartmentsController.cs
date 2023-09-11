using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DepartmentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentGetDto>>> GetDepartments()
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments.Select(dep => new DepartmentGetDto
            {
                Id = dep.Id,
                Name = dep.Name,
                Products = dep.Products.Select(prod => new ProductGetDto
                {
                    Id = prod.Id,
                    Name = prod.Name
                }).ToList(),
                Workers = dep.Workers.Select(worker => new WorkerGetDto
                {
                    Id = worker.Id,
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                }).ToList(),
            }).ToListAsync();

            return departments;
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentGetDto>> GetDepartment(int id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.Include(dep => dep.Products)
                                                       .Include(dep => dep.Workers)
                                                       .FirstOrDefaultAsync(dep => dep.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return new DepartmentGetDto
            {
                Id = department.Id,
                Name = department.Name,
                Products = department.Products.Select(prod => new ProductGetDto
                {
                    Id = prod.Id,
                    Name = prod.Name
                }).ToList(),
                Workers = department.Workers.Select(worker => new WorkerGetDto
                {
                    Id = worker.Id,
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                }).ToList(),
            };
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(DepartmentPostDto department)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'ApplicationContext.Departments'  is null.");
            }

            var createdDepartment = new Department
            {
                Name = department.Name,
            };

            _context.Departments.Add(createdDepartment);
            await _context.SaveChangesAsync();

            return Ok(createdDepartment);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
