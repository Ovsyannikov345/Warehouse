using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public WorkersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerGetDto>>> GetWorkers()
        {
            if (_context.Workers == null)
            {
                return NotFound();
            }

            return await _context.Workers.Include(w => w.Departments)
            .Select(worker => new WorkerGetDto
            {
                Id = worker.Id,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Departments = worker.Departments.Select(d => new DepartmentGetDto
                {
                    Id = d.Id,
                    Name = d.Name,
                }).ToList(),
            }).ToListAsync();
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerGetDto>> GetWorker(int id)
        {
            if (_context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.Include(worker => worker.Departments).FirstOrDefaultAsync(worker => worker.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            return new WorkerGetDto
            {
                Id = worker.Id,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Departments = worker.Departments.Select(dep => new DepartmentGetDto
                {
                    Id = dep.Id,
                    Name = dep.Name,
                }).ToList()
            };
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, WorkerPutDto workerDto)
        {
            if (id != workerDto.Id)
            {
                return BadRequest();
            }

            var worker = _context.Workers.Include(worker => worker.Departments)
                                         .FirstOrDefault(worker => worker.Id == id);
            
            if (worker == null)
            {
                return NotFound();
            }

            worker.FirstName = workerDto.FirstName;
            worker.LastName = workerDto.LastName;

            var departmentsIds = workerDto.DepartmentIds.Select(dep => dep.Id);

            await UpdateDepartments(worker, departmentsIds);

            await _context.SaveChangesAsync();

            return NoContent();

            async Task<IActionResult> UpdateDepartments(Worker worker, IEnumerable<int> departmentIds)
            {
                // Removing departments.
                worker.Departments.RemoveAll(dep => !departmentIds.Contains(dep.Id));

                // Adding departments.
                var currentDepartmentIds = worker.Departments.Select(dep => dep.Id);

                var additionalDepartmentIds = departmentIds.Except(currentDepartmentIds);

                foreach (var departmentId in additionalDepartmentIds)
                {
                    var department = await _context.Departments.FirstOrDefaultAsync(dep => dep.Id == departmentId);

                    if (department == null)
                    {
                        continue;
                    }

                    worker.Departments.Add(department);
                }

                return NoContent();
            }
        }

        // POST: api/Workers
        [HttpPost]
        public async Task<ActionResult<Worker>> PostWorker(WorkerPostDto worker)
        {
            if (_context.Workers == null)
            {
                return Problem("Entity set 'ApplicationContext.Workers'  is null.");
            }

            var departments = _context.Departments;

            Worker createdWorker = new Worker
            {
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Departments = worker.Departments.Select(dep => departments.FirstOrDefault(x => x.Id == dep.Id))
                                                .Where(dep => dep != null)
                                                .ToList()!,
            };

            _context.Workers.Add(createdWorker);
            await _context.SaveChangesAsync();

            return Ok(worker);
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            if (_context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.FindAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
