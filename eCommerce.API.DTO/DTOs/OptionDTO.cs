namespace eCommerce.API.DTO;

public class OptionPostDTO
{
    public string Name { get; set; } = string.Empty;
    public OptionType OptionType { get; set; }
    public bool IsSelected { get; set; }
    public int FilterId { get; set; }
}

public class OptionPutDTO : OptionPostDTO
{
    public int Id { get; set; }
}

public class OptionGetDTO : OptionPutDTO
{
}