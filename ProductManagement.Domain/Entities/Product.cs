using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string GeneratedCode { get; set; } = $"PRO-{Guid.NewGuid().ToString().ToUpper().Substring(0, 8)}";
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Initial Quantity")]
        public int InitialQuantity { get; set; }
    }
}
