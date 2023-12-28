using eCommerce.Common.Database.Enums;

namespace eCommerce.Data.Entities;

public class Option : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public OptionType OptionType { get; set; }
    public bool IsSelected { get; set; }

    public List<Filter>? Filters { get; } = [];
}
