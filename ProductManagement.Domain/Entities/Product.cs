namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string GeneratedCode { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int InitialQuantity { get; set; }
    }
}
