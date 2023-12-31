namespace eCommerce.API.DTO;

public class FilterTypePostDTO
{
    public string Name { get; set; } = string.Empty;
}

public class FilterTypePutDTO : FilterTypePostDTO
{
    public int Id { get; set; }
}

public class FilterTypeGetDTO : FilterTypePutDTO
{
}