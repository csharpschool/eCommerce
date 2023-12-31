namespace eCommerce.Storage.WebAssembly.Services;

public interface IStorageService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key);
}