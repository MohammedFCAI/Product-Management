using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.ViewModels
{

    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            Date = DateTime.Today;
        }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Unit { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
