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
/*namespace eCommerce.UI.Http.Clients;

public class ProductHttpClient
{
    public HttpClient Client { get; }
    string baseAddress = "https://localhost:6001/api/";

    public ProductHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
        Client.BaseAddress = new Uri($"{baseAddress}products");
    }

    public async Task<List<ProductGetDTO>> GetProductsAsync(int categoryId)
    {
        try
        {
            Client.BaseAddress = new Uri($"{baseAddress}productsbycategory/{categoryId}");
            //var token = await _storage.GetAsync(AuthConstants.TokenName);

            //bool freeOnly = JwtParser.ParseIsNotInRoleFromPayload(token, UserRole.Customer);
            //_http.AddBearerToken(token);

            using HttpResponseMessage response = await Client.GetAsync("");
            response.EnsureSuccessStatusCode();

            var resultStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ProductGetDTO>>(resultStream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //TODO: Store the filter information in Local Storage
            //TODO: Filter products with filters and display pagination

            return result ?? [];
        }
        catch(Exception ex) 
        {
            return [];
        }
    }
}
*/