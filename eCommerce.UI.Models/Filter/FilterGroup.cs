namespace eCommerce.UI.Models.Filter;

public class FilterGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int FilterTypeId { get; set; }
    public OptionType OptionType { get; set; }
    public List<FilterOption> FilterOptions { get; set; } = [];
}
