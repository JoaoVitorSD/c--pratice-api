public class Sale
{
    public int Id { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}