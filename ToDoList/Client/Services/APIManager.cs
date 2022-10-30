using System.Net;
using System.Net.Http.Json;

namespace ToDoList.Client.Services;

public abstract class APIManager
{
    protected readonly string baseApiUrl;
    protected readonly HttpClient httpClient;
    public APIManager(HttpClient httpClient, string baseApiUrl)
        => (this.httpClient, this.baseApiUrl) = (httpClient, baseApiUrl);

    protected async Task CheckResponseMessage(HttpResponseMessage message)
    {
        if (message.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(await message.Content.ReadAsStringAsync());
        message.EnsureSuccessStatusCode();
    }
}
