using System;

namespace DataAccess.DTO
{
    public class ResourceDto
    {
        public decimal Id { get; set; } 
        public string Name { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

    }
}