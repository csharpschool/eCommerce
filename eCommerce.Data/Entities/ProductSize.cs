namespace eCommerce.Data.Entities;
public class ProductSize
{
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public Product? Product { get; set; }
    public Size? Size { get; set; }
}