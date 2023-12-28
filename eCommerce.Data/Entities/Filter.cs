namespace eCommerce.Data.Entities;

public class Filter : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Category>? Categorys { get; } = [];
    public List<Option>? Options { get; } = [];
}
