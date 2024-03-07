namespace DataAccess.DTO
{
    public class WorkerDto
    {
        public decimal Id { get; set; }

        public string? Name { get; set; }
        public string Rfc { get; set; } = null!;

        public decimal Age { get; set; }
    }
}