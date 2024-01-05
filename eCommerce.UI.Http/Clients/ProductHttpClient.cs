namespace eCommerce.UI.Http.Clients;

public class ProductHttpClient
{
    private readonly HttpClient _client;
    private readonly string _baseAddress = "https://localhost:6001/api/";

    public ProductHttpClient(HttpClient httpClient)
    {
        _client = httpClient;
        // Set the base address once in the constructor
        _client.BaseAddress = new Uri(_baseAddress);
    }

    public async Task<List<ProductGetDTO>> GetProductsAsync(int categoryId)
    {
        try
        {
            // Use the relative path, not the base address here
            string relativePath = $"productsbycategory/{categoryId}";
            using HttpResponseMessage response = await _client.GetAsync(relativePath);
            response.EnsureSuccessStatusCode();

            var resultStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ProductGetDTO>>(resultStream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? [];
        }
        catch (Exception ex)
        {
            return [];
        }
    }
}
