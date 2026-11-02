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

        public void Reserve(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "A quantidade deve ser maior que zero.");

            if (AvailableQuantity < quantity)
                throw new InvalidOperationException($"Estoque insuficiente para o produto {Name}. Disponível no momento: {AvailableQuantity}, Precisa ter: {quantity}");

            AvailableQuantity -= quantity;
        }

        public void Restore(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "A quantidade deve ser maior que zero.");

            AvailableQuantity += quantity;
        }
	
    }
}
