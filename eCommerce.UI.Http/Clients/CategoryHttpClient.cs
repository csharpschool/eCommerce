namespace eCommerce.UI.Http.Clients;

public class CategoryHttpClient
{
    public HttpClient Client { get; }
    string baseAddress = "https://localhost:6501/api/";

    public CategoryHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
        Client.BaseAddress = new Uri($"{baseAddress}categorys");
    }

    public async Task<List<CategoryGetDTO>> GetCategoriesAsync()
    {
        try
        {
            Client.BaseAddress = new Uri($"{baseAddress}categorieswithdata");
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
}
