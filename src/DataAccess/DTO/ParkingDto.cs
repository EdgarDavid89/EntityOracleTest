using System;
using Swashbuckle.AspNetCore.Annotations;

namespace DataAccess.DTO
{
    public class ParkingDto
    {
        public decimal Id { get; set; }

        public string? Address { get; set; }
        public string Name { get; set; }
        public decimal? Numberspace { get; set; }
        public decimal Numbercarpark { get; set; }
        public List<WorkerDto> Workers { get; set; } = null!;
    }
}