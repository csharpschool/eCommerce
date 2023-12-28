namespace eCommerce.Common.Database.DTOs;

public class CategoryFilterPostDTO
{
    public int CategoryId { get; set; }
    public int FilterId { get; set; }
}
public class CategoryFilterDeleteDTO : CategoryFilterPostDTO
{
}
