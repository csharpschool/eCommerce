using AutoMapper;
using eCommerce.Common.Database.DTOs;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;

namespace eCommerce.Common.Http.Clients;

public class FilterHttpClient
{
    public HttpClient Client { get; }

    public FilterHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
        Client.BaseAddress = new Uri("https://localhost:5501/api/categorys");
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
}
