namespace eCommerce.API.DTO;

public class FilterPostDTO
{
    public string Name { get; set; } = string.Empty;
    public string TypeName { get; set; } = string.Empty; // The filter entity/table name
    public OptionType OptionType { get; set; }
}

public class FilterPutDTO : FilterPostDTO
{
    public int Id { get; set; }
}

public class FilterGetDTO : FilterPutDTO
{
    public List<OptionDTO>? Options { get; set; }
}

public class FilterRequestDTO : FilterGetDTO
{
    public int CategoryId { get; set; }
}


