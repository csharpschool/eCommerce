namespace eCommerce.API.DTO;

public class FilterPostDTO
{
    public string Name { get; set; } = string.Empty;
    public int FilterTypeId { get; set; }
    public OptionType OptionType { get; set; }
}

public class FilterPutDTO : FilterPostDTO
{
    public int Id { get; set; }
}

public class FilterGetDTO : FilterPutDTO
{
    public List<OptionGetDTO>? Options { get; set; }
}

public class FilterRequestDTO : FilterGetDTO
{
    public int CategoryId { get; set; }

}