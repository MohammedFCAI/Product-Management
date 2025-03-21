using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string GeneratedCode { get; private set; }
        /// Other way.
        ///public string GeneratedCode { get; set; } = $"PRO-{Guid.NewGuid().ToString().ToUpper().Substring(0, 8)}";
        public string Name { get; set; }
        public string Unit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Display(Name = "Initial Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int InitialQuantity { get; set; }


        public Product()
        {
            if (string.IsNullOrEmpty(GeneratedCode))
            {
                GeneratedCode = $"PRO-{Guid.NewGuid().ToString().ToUpper().Substring(0, 8)}";
            }
        }

    }

}
