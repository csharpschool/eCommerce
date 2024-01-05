namespace eCommerce.UI.Http.Clients;

public class CategoryHttpClient
{
    public HttpClient Client { get; }
    string baseAddress = "https://localhost:6501/api/";
    private readonly SessionStorageService _session;

    public CategoryHttpClient(HttpClient httpClient, SessionStorageService session)
    {
        Client = httpClient;
        _session = session;
        Client.BaseAddress = new Uri($"{baseAddress}categorys");
    }

    public async Task<List<CategoryGetDTO>> GetCategoriesAsync()
    {
        try
        {
            var categoryData = await _session.GetAsync<CategoryStorageDTO>("CategoryData");

            if (categoryData is null || categoryData?.Date != DateOnly.FromDateTime(DateTime.Now.Date))
            {
                Client.BaseAddress = new Uri($"{baseAddress}categorieswithdata");
                using HttpResponseMessage response = await Client.GetAsync("");
                response.EnsureSuccessStatusCode();

                var result = JsonSerializer.Deserialize<List<CategoryGetDTO>>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                await _session.SetAsync<CategoryStorageDTO>("CategoryData", new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.Date),
                    Categories = [.. result]
                }); ;

                return result ?? [];
            }

            return categoryData.Categories ?? [];
        }
        catch(Exception ex)
        {
            return [];
        }
    }
}
