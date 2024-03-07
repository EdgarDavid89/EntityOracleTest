using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace DataAccess.DTO
{
    public class WorkerDto
    {
        public decimal Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string Rfc { get; set; } = null!;

        [Required]
        public decimal Age { get; set; }

        public decimal? ParkingId { get; set; }
    }
}