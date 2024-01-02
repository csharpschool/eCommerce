using System.Text;

namespace eCommerce.UI.Http.Clients;

public class FilterHttpClient
{
    public HttpClient Client { get; }
    string baseAddress = "https://localhost:5501/api/";

    public FilterHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
        Client.BaseAddress = new Uri($"{baseAddress}filters");
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
