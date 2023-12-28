namespace eCommerce.Data.Entities;

public class CategoryFilter
{
    public int CategoryId { get; set; }
    public int FilterId { get; set; }
    public Category? Category { get; set; }
    public Filter? Filter { get; set; }
}
