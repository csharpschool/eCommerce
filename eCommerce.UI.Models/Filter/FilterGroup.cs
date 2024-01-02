namespace eCommerce.UI.Models.Filter;

public class FilterGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TypeName { get; set; } = string.Empty;
    public OptionType OptionType { get; set; }
    public List<FilterOption> FilterOptions { get; set; } = [];
}
