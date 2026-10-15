using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace Orders.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public String Name { get; set; }
	    public int AvailableQuantity { get; set; }
	
    }
}
