using System;
using DataAccess.Models;

namespace DataAccess.DTO
{
    public class UserResourceDto
    {
        public decimal Id { get; set; } 
        public decimal? UserId { get; set; }
        public decimal? ResourceId { get; set; }

        public bool? ActRead { get; set; }
        public bool? ActUpdate { get; set; }
        public bool? ActDelete { get; set; }
        public bool? ActCreate { get; set; }
        protected DateTime? DateCreated { get; set; }
        protected DateTime? DateUpdated { get; set; }
        public Resource? Resource { get; set; }   
    }
}