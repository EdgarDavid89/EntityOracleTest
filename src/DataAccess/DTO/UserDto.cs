using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.DTO
{
    public class UserDto
    {
        public decimal Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string? Email { get; set; }

        public string? UserType { get; set; }

        [JsonIgnore]
        public DateTime? DateCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateUpdated { get; set; }
        
    }
}