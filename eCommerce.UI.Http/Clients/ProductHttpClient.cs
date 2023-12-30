namespace eCommerce.UI.Http.Clients;

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

            var result = JsonSerializer.Deserialize<List<ProductGetDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //TODO: Store the filter information in Local Storage (see ChatGPT for example)
            //TODO: Filter products with filters and display pagination

            return result ?? [];
        }
        catch(Exception ex) 
        {
            return [];
        }
    }
}
