namespace eCommerce.Common.Razor.Services.Filter;

public class FilterGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<FilterOption> FilterOptions { get; set; } = [];
}
