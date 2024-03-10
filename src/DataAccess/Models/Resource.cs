using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Resource
    {
        public Resource()
        {
            Userresources = new HashSet<Userresource>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? Datecreated { get; set; }
        public DateTime? Dateupdated { get; set; }

        public virtual ICollection<Userresource> Userresources { get; set; }
    }
}
