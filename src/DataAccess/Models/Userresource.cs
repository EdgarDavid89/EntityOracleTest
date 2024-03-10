using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Userresource
    {
        public decimal Id { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Resourceid { get; set; }
        public bool? Actread { get; set; }
        public bool? Actupdate { get; set; }
        public bool? Actdelete { get; set; }
        public bool? Actcreate { get; set; }
        public DateTime? Datecreated { get; set; }
        public DateTime? Dateupdated { get; set; }

        public virtual Resource? Resource { get; set; }
        public virtual User? User { get; set; }
    }
}
