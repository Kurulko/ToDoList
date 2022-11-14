using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;
using ToDoList.Shared.Services;

namespace ToDoList.Client.Services;

public class RolesManager : APIManager, IRoleService
{
    public RolesManager(HttpClient httpClient) :
        base(httpClient, "api/Roles/") { }

    public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        => (await httpClient.GetFromJsonAsync<IEnumerable<IdentityRole>>(baseApiUrl))!;
    public async Task<IdentityRole> GetRoleAsync(string roleId)
        => (await httpClient.GetFromJsonAsync<IdentityRole>(baseApiUrl + roleId))!;

    public async Task CreateRoleAsync(IdentityRole role)
        => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl, role));

    public async Task DeleteRoleAsync(string roleId)
        => await CheckResponseMessage(await httpClient.DeleteAsync(baseApiUrl + roleId));


    public async Task UpdateRoleAsync(IdentityRole role)
        => await CheckResponseMessage(await httpClient.PutAsJsonAsync(baseApiUrl, role));
}
