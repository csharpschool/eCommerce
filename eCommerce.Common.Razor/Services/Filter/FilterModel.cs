namespace eCommerce.Common.Razor.Services.Filter;

public class FilterModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public FilterType FilterType { get; set; }
    public bool IsSelected { get; set; }
    public List<RadioModel> Options { get; set; } = []; // Used for RadioButton
    public int SelectedValue { get; set; }
    public int MinValue { get; set; } // Used for Range
    public int MaxValue { get; set; } // Used for Range

    public FilterModel(int id, string name, FilterType filterType, bool isSelected = false) => (Id, Name, FilterType, IsSelected) = (id, name, filterType, isSelected);
    public FilterModel(int id, string name, FilterType filterType, List<RadioModel> options) => (Id, Name, FilterType, Options) = (id, name, filterType, options);
    public FilterModel(int id, string name, FilterType filterType, int min, int max) => (Id, Name, FilterType, MinValue, MaxValue) = (id, name, filterType, min, max);
}