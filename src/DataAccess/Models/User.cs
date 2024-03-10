using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class User
    {
        public decimal Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime? Datecreated { get; set; }
        public DateTime? Dateupdated { get; set; }
        public decimal Usertypeid { get; set; }

        public virtual Userstype Usertype { get; set; } = null!;
    }
}
