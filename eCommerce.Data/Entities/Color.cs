namespace eCommerce.Data.Entities;

public class Color : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public OptionType? OptionType { get; set; }
    public List<Product> Products { get; } = [];
}

