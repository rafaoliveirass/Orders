using System.ComponentModel.DataAnnotations;

namespace Orders.Domain.Entities
{
    public class Login
    {
        [Key]
        public Guid id { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }
        public string? email { get; set; }
        public DateTime created_at { get; set; }
    }
}
