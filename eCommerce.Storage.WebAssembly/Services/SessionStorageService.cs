using Blazored.SessionStorage;

namespace eCommerce.Storage.WebAssembly.Services;

public class SessionStorageService(ISessionStorageService sessionStorage) : IStorageService
{
    public async Task<T> GetAsync<T>(string key) =>
        await sessionStorage.GetItemAsync<T>(key);

    public async Task RemoveAsync(string key) =>
        await sessionStorage.RemoveItemAsync(key);

    public async Task SetAsync<T>(string key, T value) =>
        await sessionStorage.SetItemAsync(key, value);
}