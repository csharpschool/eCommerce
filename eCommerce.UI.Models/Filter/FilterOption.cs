namespace eCommerce.UI.Models.Filter;

public class FilterOption
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public OptionType OptionType { get; set; }
    public bool IsSelected { get; set; }
    public List<RadioModel> Options { get; set; } = []; // Used for RadioButton
    public int SelectedValue { get; set; }
    public int MinValue { get; set; } // Used for Range
    public int MaxValue { get; set; } // Used for Range
    public FilterOption() { }
    public FilterOption(int id, string name, OptionType filterType, bool isSelected = false) => (Id, Name, OptionType, IsSelected) = (id, name, filterType, isSelected);
    public FilterOption(int id, string name, OptionType filterType, List<RadioModel> options, int selected) => (Id, Name, OptionType, Options, SelectedValue) = (id, name, filterType, options, selected);
    public FilterOption(int id, string name, OptionType filterType, int min, int max, int value) => (Id, Name, OptionType, MinValue, MaxValue, SelectedValue) = (id, name, filterType, min, max, value);
}