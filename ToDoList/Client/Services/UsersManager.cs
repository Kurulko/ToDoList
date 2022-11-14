using System.Net.Http.Json;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Models;

namespace ToDoList.Client.Services;

public class UsersManager : APIManager, IUserService
{
    public UsersManager(HttpClient httpClient) :
        base(httpClient, "api/Users/") { }

    public async Task<User> GetUserAsync(string userId)
        => (await httpClient.GetFromJsonAsync<User>(baseApiUrl + userId))!;

    public async Task<IEnumerable<User>> GetUsersAsync()
        => (await httpClient.GetFromJsonAsync<IEnumerable<User>>(baseApiUrl))!;

    public async Task CreateUserAsync(User user)
        => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl, user));

    public async Task UpdateUserAsync(User user)
        => await CheckResponseMessage(await httpClient.PutAsJsonAsync(baseApiUrl, user));

    public async Task DeleteUserAsync(string userId)
        => await CheckResponseMessage(await httpClient.DeleteAsync(baseApiUrl + userId));

    public async Task ChangeUserPasswordAsync(ModelWithUserId<ChangePassword> model)
        => await CheckResponseMessage(await httpClient.PutAsJsonAsync(baseApiUrl + "password", model));

    public async Task AddUserPasswordAsync(ModelWithUserId<ChangePassword> model)
        => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl + "password", model));

    public async Task<bool> HasUserPasswordAsync(string userId)
         => (await httpClient.GetFromJsonAsync<bool>($"{baseApiUrl}{userId}/password"))!;

    public async Task<IEnumerable<string>> GetRolesAsync(string userId)
        => (await httpClient.GetFromJsonAsync<IEnumerable<string>>($"{baseApiUrl}{userId}/roles"))!;

    public async Task AddRoleToUserAsync(ModelWithUserId<string> model)
        => await CheckResponseMessage(await httpClient.PostAsJsonAsync($"{baseApiUrl}{model.UserId}/role", model.Model));

    public async Task DeleteRoleFromUserAsync(string userId, string roleName)
        => await CheckResponseMessage(await httpClient.DeleteAsync($"{baseApiUrl}{userId}/{roleName}"));
}
