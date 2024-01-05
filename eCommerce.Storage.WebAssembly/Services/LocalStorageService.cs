using Blazored.LocalStorage;

namespace eCommerce.Storage.WebAssembly.Services;

public class LocalStorageService(ILocalStorageService localStorage) : IStorageService
{
    public async Task<T> GetAsync<T>(string key) =>
        await localStorage.GetItemAsync<T>(key);

    public async Task RemoveAsync(string key) =>
        await localStorage.RemoveItemAsync(key);

    public async Task SetAsync<T>(string key, T value) =>
        await localStorage.SetItemAsync(key, value);
}
