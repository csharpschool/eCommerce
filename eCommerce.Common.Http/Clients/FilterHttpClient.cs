using System.Text.Json;

namespace eCommerce.Common.Http.Clients;

public class FilterHttpClient
{
    private readonly HttpClient _httpClient;

    public FilterHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
    }

    /*public async Task<List<CourseDTO>> GetCoursesAsync()
    {
        try
        {
            var token = await _storage.GetAsync(AuthConstants.TokenName);

            bool freeOnly = JwtParser.ParseIsNotInRoleFromPayload(token, UserRole.Customer);
            _http.AddBearerToken(token);

            using HttpResponseMessage response = await _http.Client.GetAsync($"courses?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<CourseDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new List<CourseDTO>();
        }
        catch
        {
            return new List<CourseDTO>();
        }
    }*/
}
