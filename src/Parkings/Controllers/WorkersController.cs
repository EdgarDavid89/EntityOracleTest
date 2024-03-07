using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Business;
using DataAccess.DTO;

namespace Parkings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IWorkerB _workerB;

        public WorkersController(ModelContext context,
        IWorkerB workerB)
        {
            _context = context;
            _workerB = workerB;
        }

        // GET: api/Workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            return await _context.Workers.ToListAsync();
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDto>> GetWorker(decimal id)
        {
            var worker = await _workerB.GetWorker(id);

            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }

        // POST: api/Workers
        [HttpPost]
        public async Task<ActionResult<Worker>> CreateWorker(Worker worker)
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorker), new { id = worker.Id }, worker);
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(decimal id, Worker worker)
        {
            if (id != worker.Id)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool WorkerExists(decimal id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }
    }
}