namespace eCommerce.Common.Database.DTOs;

public class FilterOptionPostDTO
{
    public int CategoryId { get; set; }
    public int FilterId { get; set; }
}
public class FilterOptionDeleteDTO : FilterOptionPostDTO
{
}
