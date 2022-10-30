using System.Net.Http.Json;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Client.Services
{
    public class UsersManager : APIManager, IUserService
    {
        public UsersManager(HttpClient httpClient) :
            base(httpClient, "api/Users/") { }

        public async Task<User> GetUserAsync(string userId)
            => (await httpClient.GetFromJsonAsync<User>(baseApiUrl + userId))!;

        public async Task<IEnumerable<User>> GetUsersAsync()
            => (await httpClient.GetFromJsonAsync<IEnumerable<User>>(baseApiUrl))!;
    }
}
