using Microsoft.AspNetCore.Components;

namespace eCommerce.Common.Razor.Services.Filter;

public class FilterRenderingService
{
    private readonly Dictionary<FilterType, Type> _filterTypeToComponentMapping = new()
        {
            { FilterType.Checkbox, typeof(CheckboxFilter) },
            { FilterType.RadioButton, typeof(RadioButtonFilter) },
            { FilterType.Range, typeof(RangeFilter) }
        };

    public RenderFragment RenderFilter(FilterOption filter)
    {
        return builder =>
        {
            var componentType = GetComponentType(filter.FilterType);
            if (componentType != null)
            {
                builder.OpenComponent(0, componentType);
                builder.AddAttribute(1, "Filter", filter);
                builder.CloseComponent();
            }
        };
    }
    public RenderFragment RenderFilters(List<FilterOption> filters)
    {
        return builder =>
        {
            foreach (var filter in filters)
            {
                RenderFragment fragment = RenderFilter(filter);
                fragment(builder);
            }
        };
    }
    private Type GetComponentType(FilterType filterType) => 
        _filterTypeToComponentMapping.TryGetValue(filterType, out var componentType)
            ? componentType
            : throw new ArgumentNullException("Couldn't find a matching component.");
}
