using Microsoft.AspNetCore.Mvc;
using DataAccess.Business;
using DataAccess.DTO;
using System.ComponentModel.DataAnnotations;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  ResourceController : ControllerBase
    {
        private readonly IResourceB _resourceB;

        public ResourceController(IResourceB resourceB)
        {
            _resourceB = resourceB;   
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResources()
        {
            try
            {
                var result = await _resourceB.GetResources();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetResource(decimal id)
        {
            var user = await _resourceB.GetResourceById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult> CreateResource(ResourceDto resource)
        {
            var context = new ValidationContext(resource);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(resource, context, results, true))
            {
                return BadRequest(results.Select(r => r.ErrorMessage));
            }

            var resourceSaved = await _resourceB.CreateResource(resource);

            return CreatedAtAction(nameof(GetResource), new { id = resourceSaved.Id }, resourceSaved);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(decimal id, ResourceDto resource)
        {
            var context = new ValidationContext(resource);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(resource, context, results, true))
            {
                return BadRequest(results.Select(r => r.ErrorMessage));
            }

            if (id != resource.Id)
            {
                return BadRequest();
            }
            try
            {
                var result = await _resourceB.UpdateResource(resource, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}