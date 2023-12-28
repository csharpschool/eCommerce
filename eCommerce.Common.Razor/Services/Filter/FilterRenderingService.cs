using Microsoft.AspNetCore.Components;

namespace eCommerce.Common.Razor.Services.Filter;

public class FilterRenderingService
{
    private readonly Dictionary<OptionType, Type> _optionTypeToComponentMapping = new()
    {
        { OptionType.Checkbox, typeof(CheckboxFilter) },
        { OptionType.RadioButton, typeof(RadioButtonFilter) },
        { OptionType.Range, typeof(RangeFilter) }
    };

    public RenderFragment RenderFilter(FilterOption filter)
    {
        return builder =>
        {
            var componentType = GetComponentType(filter.OptionType);
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
    private Type GetComponentType(OptionType oprionType) => 
        _optionTypeToComponentMapping.TryGetValue(oprionType, out var componentType)
            ? componentType
            : throw new ArgumentNullException("Couldn't find a matching component.");
}
