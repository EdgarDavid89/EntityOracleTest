using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using DataAccess.Business;
using DataAccess.DTO;

namespace Parkings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IParkingB<decimal> _parkingB;

        public ParkingsController(ModelContext context,
        IParkingB<decimal> parkingB)
        {
            _context = context;
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
        public async Task<ActionResult<Parking>> CreateParking(Parking parking)
        {
            _context.Parkings.Add(parking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParking), new { id = parking.Id }, parking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParking(decimal id, Parking parking)
        {
            if (id != parking.Id)
            {
                return BadRequest();
            }

            _context.Entry(parking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParking(decimal id)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if (parking == null)
            {
                return NotFound();
            }

            _context.Parkings.Remove(parking);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("{parkingId}/workers")]
        public async Task<ActionResult> GetWorkeByParking(decimal? parkingId)
        {
            try
            {
                var workers = await _context.Workers.Where(x => x.Parkingid == parkingId).ToListAsync();

                if(workers == null)
                    return NotFound();

                return Ok(workers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        private bool ParkingExists(decimal id)
        {
            return _context.Parkings.Any(e => e.Id == id);
        }
        
    }
}