namespace eCommerce.Data.Entities;

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public List<Category>? Categories { get; } = [];
    public List<Size>? Sizes { get; } = [];
    public List<Color>? Colors { get; } = [];
}

