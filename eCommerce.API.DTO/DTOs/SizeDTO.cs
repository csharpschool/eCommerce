namespace eCommerce.API.DTO;

public class SizePostDTO
{
    public string Name { get; set; } = string.Empty;
    public OptionType OptionType { get; set; }
    public bool IsSelected { get; set; }
}

public class SizePutDTO : SizePostDTO
{
    public int Id { get; set; }
}

public class SizeGetDTO : SizePutDTO
{
}


