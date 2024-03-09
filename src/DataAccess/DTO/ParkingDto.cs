using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.DTO
{
    public class ParkingDto
    {
        public decimal Id { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal? Numberspace { get; set; }

        [Required]
        public decimal Numbercarpark { get; set; }
        
        [JsonIgnore]
        public List<WorkerDto> Workers { get; set; } = null!;
        
    }
}