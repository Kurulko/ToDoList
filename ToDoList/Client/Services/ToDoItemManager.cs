using System.Net;
using System.Net.Http.Json;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Client.Services
{
    public class ToDoItemManager : APIManager, IToDoItemService
    {
        public ToDoItemManager(HttpClient httpClient) :
            base(httpClient, "api/ToDoList/") { }

        public async Task<ToDoItem> GetToDoItemAsync(string userId, long toDoItemId)
            => (await httpClient.GetFromJsonAsync<ToDoItem>(baseApiUrl + $"{userId}/{toDoItemId}"))!;
       
        public async Task<IEnumerable<ToDoItem>> GetToDoItemsAsync(string userId)
            => (await httpClient.GetFromJsonAsync<IEnumerable<ToDoItem>>(baseApiUrl + userId))!;

        public async Task AddToDoItemAsync(string userId, ToDoItem item)
            => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl + userId, item));

        public async Task UpdateToDoItem(string userId, ToDoItem item)
            => await CheckResponseMessage(await httpClient.PutAsJsonAsync(baseApiUrl + userId, item));

        public async Task DeleteToDoItem(string userId, long toDoItemId)
            => await CheckResponseMessage(await httpClient.DeleteAsync(baseApiUrl + $"{userId}/{toDoItemId}"));
    }
}
