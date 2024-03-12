
using System;
using System.ComponentModel.DataAnnotations;

namespace Parkings.Models
{
    public class AccessRequest
    {
        [Required]
        public string User { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;      
    }
}
