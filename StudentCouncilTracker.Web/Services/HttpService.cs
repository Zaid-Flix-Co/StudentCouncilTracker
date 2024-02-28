using Newtonsoft.Json;
using System.Text;
using StudentCouncilTracker.Application.Entities.Base.Filters;

namespace StudentCouncilTracker.Web.Services;

public class HttpService<TEntity> where TEntity : class
{
    protected string Url = $"api/{typeof(TEntity)}";

    public async Task<TEntity> GetAsync(int id, HttpClient client)
    {
        Url += $"/{id}";

        using (client)
        {
            var response = await client.GetAsync(Url);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TEntity>(responseContent);
        }
    }

    public async Task<TEntity> GetListAsync(ListFilter entity, HttpClient client)
    {
        Url += "/GetList";

        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

        using (client)
        {
            var response = await client.PostAsync(Url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TEntity>(responseContent);
        }
    }
    
    public async Task<HttpResponseMessage> CreateAsync(TEntity entity, HttpClient client)
    {
        Url += "/Create";

        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

        using (client)
        {
            return await client.PostAsync(Url, content);
        }
    }

    public async Task<HttpResponseMessage> PutAsync(int id, TEntity entity, HttpClient client)
    {
        Url += $"/{id}";

        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

        using (client)
        {
            return await client.PutAsync(Url, content);
        }
    }

    public async Task<HttpResponseMessage> DeleteAsync(int id, HttpClient client)
    {
        Url += $"/{id}";

        using (client)
        {
            return await client.DeleteAsync(Url);
        }
    }
}