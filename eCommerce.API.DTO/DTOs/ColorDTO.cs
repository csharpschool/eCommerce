namespace eCommerce.API.DTO;

public class ColorPostDTO
{
    public string Name { get; set; } = string.Empty;
    public string ColorHex { get; set; }
    public string BkColorHex { get; set; }
    public OptionType OptionType { get; set; }
    public bool IsSelected { get; set; }
}

public class ColorPutDTO : ColorPostDTO
{
    public int Id { get; set; }
}

public class ColorGetDTO : ColorPutDTO
{
   
}


