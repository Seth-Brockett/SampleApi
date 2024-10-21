namespace SampleApi.Common.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int ProductQuantity { get; set; } = 0;
    }
}
