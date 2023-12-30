namespace eCommerce.UI.Models.Filter;

public class RadioModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public RadioModel(int id, string name) => (Id, Name) = (id, name);
}