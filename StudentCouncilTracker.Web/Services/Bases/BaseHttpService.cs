using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using StudentCouncilTracker.Application.OperationResults;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace StudentCouncilTracker.Web.Services.Bases;

public abstract class BaseHttpService
{
    private readonly HttpClient _client;

    protected BaseHttpService(HttpClient httpClient)
    {
        _client = httpClient;
        _client.Timeout = TimeSpan.FromMinutes(5);
    }

    protected abstract string BasePath { get; }

    private static HttpContent CreateContent(object model)
    {
        if (model is HttpContent cont)
            return cont;

        var content = new ByteArrayContent(model == null
            ? []
            : Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Headers.ContentEncoding.Add("UTF-8");
        return content;
    }

    private static HttpRequestMessage CreateMessage(string uri, HttpMethod method, object model)
    {
        var message = new HttpRequestMessage(method, uri);
        if (method != HttpMethod.Post && method != HttpMethod.Put)
            return message;

        message.Content = CreateContent(model);
        return message;
    }

    protected async Task<OperationResult<T>> SendAsync<T>(string action, HttpMethod method, object model = null)
        where T : class, new()
    {
        try
        {
            var uri = $"api/{BasePath}/{action}";
            var message = CreateMessage(uri, method, model);

            var response = await _client.SendAsync(message);
            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return new OperationResult<T>();
                case HttpStatusCode.Forbidden:
                    throw new Exception(HttpStatusCode.Forbidden.ToString());
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{uri}, content: {content}");
            var res = JsonConvert.DeserializeObject<T>(content);

            if (res == null)
            {
                var operation = OperationResult.CreateResult(new T());
                operation.AddError($"Cant convert response from URI = '{uri}', content returned = '{content}' to type = {typeof(T)} ");
                return operation;
            }

            return res;
        }
        catch (Exception exc)
        {
            var operation = OperationResult.CreateResult(new T());
            operation.AddError(exc);
            return operation;
        }
    }
}