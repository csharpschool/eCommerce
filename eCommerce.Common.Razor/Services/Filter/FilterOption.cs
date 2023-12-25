﻿namespace eCommerce.Common.Razor.Services.Filter;

public class FilterOption
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public FilterType FilterType { get; set; }
    public bool IsSelected { get; set; }
    public List<RadioModel> Options { get; set; } = []; // Used for RadioButton
    public int SelectedValue { get; set; }
    public int MinValue { get; set; } // Used for Range
    public int MaxValue { get; set; } // Used for Range

    public FilterOption(int id, string name, FilterType filterType, bool isSelected = false) => (Id, Name, FilterType, IsSelected) = (id, name, filterType, isSelected);
    public FilterOption(int id, string name, FilterType filterType, List<RadioModel> options, int selected) => (Id, Name, FilterType, Options, SelectedValue) = (id, name, filterType, options, selected);
    public FilterOption(int id, string name, FilterType filterType, int min, int max, int value) => (Id, Name, FilterType, MinValue, MaxValue, SelectedValue) = (id, name, filterType, min, max, value);
}