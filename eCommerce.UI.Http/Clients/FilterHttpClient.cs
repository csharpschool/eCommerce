using System.Text;

namespace eCommerce.UI.Http.Clients;

public class FilterHttpClient
{
    public HttpClient Client { get; }
    string baseAddress = "https://localhost:5501/api/";

    public FilterHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
        //Client.BaseAddress = new Uri("https://localhost:5501/api/categorys");
        Client.BaseAddress = new Uri($"{baseAddress}categorys");
    }

    public async Task<List<CategoryGetDTO>> GetCategoriesAsync()
    {
        try
        {
            //var token = await _storage.GetAsync(AuthConstants.TokenName);

            //bool freeOnly = JwtParser.ParseIsNotInRoleFromPayload(token, UserRole.Customer);
            //_http.AddBearerToken(token);

            using HttpResponseMessage response = await Client.GetAsync("");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<CategoryGetDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? [];
        }
        catch(Exception ex) 
        {
            return [];
        }
    }

    public async Task<List<ProductGetDTO>> FilterProductsAsync(List<FilterRequestDTO> dtos)
    {
        try
        {
            var json = JsonSerializer.Serialize(dtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync($"{baseAddress}filterproducts", content);
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
