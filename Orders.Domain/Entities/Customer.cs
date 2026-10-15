using System.ComponentModel.DataAnnotations;

namespace Orders.Domain.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
    }    
}
