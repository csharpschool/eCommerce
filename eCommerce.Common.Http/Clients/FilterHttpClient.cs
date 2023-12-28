namespace eCommerce.Common.Http.Clients;

public class FilterHttpClient
{
    private readonly HttpClient _httpClient;

    public FilterHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
    }
}
