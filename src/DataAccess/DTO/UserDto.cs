namespace DataAccess.DTO
{
    public class UserDto
    {
        public decimal Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? UserType { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUdated { get; set; }
        
    }
}