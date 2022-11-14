using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Shared;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models;

namespace ToDoList.Server.Controllers;

[Authorize]
public class UsersController : ApiController
{
    readonly IUserService iUser;
    public UsersController(IUserService iUser)
        => this.iUser = iUser;

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IEnumerable<User>> Users()
         => await iUser.GetUsersAsync();

    [HttpGet("{userId}")]
    public async Task<User> User(string userId)
        => await iUser.GetUserAsync(userId);


    [HttpGet("{userId}/password")]
    public async Task<bool> HasPassword(string userId)
        => await iUser.HasUserPasswordAsync(userId);

    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword(ModelWithUserId<ChangePassword> model)
        => await ReturnOkIfEverithingIsGood(async () => await iUser.ChangeUserPasswordAsync(model));

    [HttpPost("password")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreatePassword(ModelWithUserId<ChangePassword> model)
        => await ReturnOkIfEverithingIsGood(async () => await iUser.AddUserPasswordAsync(model));


    [HttpGet("{userId}/roles")]
    public async Task<IEnumerable<string>> GetRoles(string userId)
        => await iUser.GetRolesAsync(userId);

    [HttpPost("{userId}/role")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> AddRole(string userId, [FromBody]string roleName)
        => await ReturnOkIfEverithingIsGood(async () => await iUser.AddRoleToUserAsync(new(userId, roleName)));

    [HttpDelete("{userId}/{roleName}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteRole(string userId, string roleName)
        => await ReturnOkIfEverithingIsGood(async () => await iUser.DeleteRoleFromUserAsync(userId, roleName));

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreateUser(User user)
        => await ReturnOkIfEverithingIsGood(async () => await iUser.CreateUserAsync(user));

    [HttpPut]
    public async Task<IActionResult> UpdateUser(User user)
        => await ReturnOkIfEverithingIsGood(async () => await iUser.UpdateUserAsync(user));

    [HttpDelete("{userId}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteUser(string userId)
        => await ReturnOkIfEverithingIsGood(async () => await iUser.DeleteUserAsync(userId));
}
