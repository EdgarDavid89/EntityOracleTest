using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Business;
using DataAccess.DTO;

namespace Parkings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerB<decimal> _workerB;
        private readonly ILogger<WorkersController> _logger;

        public WorkersController(IWorkerB<decimal> workerB, 
        ILogger<WorkersController> logger)
        {
            _workerB = workerB;
            _logger = logger;
        }

        // GET: api/Workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkers()
        {
            var result = await _workerB.GetWorkers();
            return Ok(result);
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDto>> GetWorker(decimal id)
        {
            var worker = await _workerB.GetWorkerById(id);

            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }

        // POST: api/Workers
        [HttpPost]
        public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerDto worker)
        {
            var result = await _workerB.CreateWorker(worker);
            return CreatedAtAction(nameof(GetWorker), new { id = result.Id }, result);
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(decimal id, WorkerDto worker)
        {
            if (id != worker.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _workerB.UpdateWorker(worker, id);
                _logger.LogInformation($"Worker with id {result.Id} was updated");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(decimal id)
        {
            try
            {
                var result =  await _workerB.DeleteWorker(id);
                _logger.LogInformation($"Worker with id {result.Id} was deleted");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }


    }
}