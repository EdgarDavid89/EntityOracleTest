using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Parking
    {
        public Parking()
        {
            Workers = new HashSet<Worker>();
        }

        public decimal Id { get; set; }
        public string? Address { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Numberspace { get; set; }
        public decimal Numbercarpark { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
