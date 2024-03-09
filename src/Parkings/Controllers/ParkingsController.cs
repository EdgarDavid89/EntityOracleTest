using Microsoft.AspNetCore.Mvc;
using DataAccess.Business;
using DataAccess.DTO;
using System.ComponentModel.DataAnnotations;

namespace Parkings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly IParkingB _parkingB;

        public ParkingsController(IParkingB parkingB)
        {
            _parkingB = parkingB;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingDto>>> GetParkings()
        {
            try
            {
                var result = await _parkingB.GetParkings();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingDto>> GetParking(decimal id)
        {
            var parking = await _parkingB.GetParkingById(id);

            if (parking == null)
            {
                return NotFound();
            }

            return parking;
        }

        [HttpPost]
        public async Task<ActionResult> CreateParking(ParkingDto parking)
        {
            var context = new ValidationContext(parking);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(parking, context, results, true))
            {
                return BadRequest(results.Select(r => r.ErrorMessage));
            }

            var parkingSaved = await _parkingB.CreateParking(parking);

            return CreatedAtAction(nameof(GetParking), new { id = parkingSaved.Id }, parkingSaved);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParking(decimal id)
        {
            try
            {
                var parking = await _parkingB.DeleteParking(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("{parkingId}/workers")]
        public async Task<ActionResult> GetWorkeByParking(decimal parkingId)
        {
            try
            {
                if (parkingId <= 0)
                    return BadRequest("Invalid parking id");

                var workers = await _parkingB.GetWorkersByParking(parkingId);

                if (workers == null)
                    return NotFound();

                return Ok(workers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParking(decimal id, ParkingDto parking)
        {
            var context = new ValidationContext(parking);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(parking, context, results, true))
            {
                return BadRequest(results.Select(r => r.ErrorMessage));
            }

            if (id != parking.Id)
            {
                return BadRequest();
            }
            try
            {
                var result = await _parkingB.UpdateParking(parking, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}