using System.Net;
using System.Net.Http.Json;
using GymPro.Web.Models;

namespace GymPro.Web.Services;

public class GymApiClient(HttpClient http)
{
    public async Task<T?> Get<T>(string path) => await http.GetFromJsonAsync<T>(path);

    public async Task<(bool Ok, string Message)> Save<T>(string path, T item, int? id = null)
    {
        HttpResponseMessage response = id.HasValue
            ? await http.PutAsJsonAsync($"{path}/{id.Value}", item)
            : await http.PostAsJsonAsync(path, item);

        if (response.IsSuccessStatusCode) return (true, "Saved successfully.");
        var body = await response.Content.ReadAsStringAsync();
        return (false, string.IsNullOrWhiteSpace(body) ? $"Request failed: {(int)response.StatusCode}" : body);
    }

    public async Task<(bool Ok, string Message)> Delete(string path, int id)
    {
        var response = await http.DeleteAsync($"{path}/{id}");
        if (response.IsSuccessStatusCode) return (true, "Deleted successfully.");
        var body = await response.Content.ReadAsStringAsync();
        return (false, string.IsNullOrWhiteSpace(body) ? $"Request failed: {(int)response.StatusCode}" : body);
    }
}
