namespace eCommerce.Data.Entities;

public class FilterOption
{
    public int FilterId { get; set; }
    public int OptionId { get; set; }
    public Filter? Filter { get; set; }
    public Option? Option { get; set; }
}
