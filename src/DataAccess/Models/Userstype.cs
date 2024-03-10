using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Userstype
    {
        public Userstype()
        {
            Users = new HashSet<User>();
        }

        public decimal Id { get; set; }
        public string Typeuser { get; set; } = null!;
        public string Endpoints { get; set; } = null!;
        public DateTime? Datecreated { get; set; }
        public DateTime? Dateupdated { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
