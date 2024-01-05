namespace eCommerce.Data.Entities;

public class Color : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ColorHex { get; set; } = "#000";
    public string BkColorHex { get; set; } = "#FFF";
    public OptionType? OptionType { get; set; }
    public List<Product> Products { get; } = [];
}

