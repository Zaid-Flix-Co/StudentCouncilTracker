using Newtonsoft.Json;
using System.Text;
using StudentCouncilTracker.Application.Entities.Base.Filters;

namespace StudentCouncilTracker.Web.Services;

public class HttpService<TEntity, TEntityDtoData> where TEntity : class
{
    private static string _url = $"api/{typeof(TEntity).Name}";

    public static async Task<TEntityDtoData> GetAsync(int id, HttpClient client)
    {
        _url += $"/{id}";

        var response = await client.GetAsync(_url);
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<TEntityDtoData>(responseContent);
    }

    public static async Task<TEntityDtoData> GetListAsync(ListFilter entity, HttpClient client)
    {
        _url += "/GetList";

        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(_url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<TEntityDtoData>(responseContent);
    }

    public static async Task<HttpResponseMessage> CreateAsync(TEntityDtoData entity, HttpClient client)
    {
        _url += "/Create";

        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

        return await client.PostAsync(_url, content);
    }

    public static async Task<HttpResponseMessage> PutAsync(int id, TEntityDtoData entity, HttpClient client)
    {
        _url += $"/{id}";

        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

        return await client.PutAsync(_url, content);
    }

    public static async Task<HttpResponseMessage> DeleteAsync(int id, HttpClient client)
    {
        _url += $"/{id}";

        return await client.DeleteAsync(_url);
    }
}