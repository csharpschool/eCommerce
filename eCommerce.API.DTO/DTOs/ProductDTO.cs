namespace eCommerce.API.DTO;

public class ProductPostDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
}
public class ProductPutDTO : ProductPostDTO
{
    public int Id { get; set; }
}
public class ProductGetDTO : ProductPutDTO
{
    public List<CategorySmallGetDTO>? Categories { get; set; }
    public List<SizeGetDTO>? Sizes { get; set; }
    public List<ColorGetDTO>? Colors { get; set; }
}


