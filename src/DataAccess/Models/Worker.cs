using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Worker
    {
        public decimal Id { get; set; }
        public string? Name { get; set; }
        public string Rfc { get; set; } = null!;
        public decimal Age { get; set; }
        public decimal? Parkingid { get; set; }

        public virtual Parking? Parking { get; set; }
    }
}
