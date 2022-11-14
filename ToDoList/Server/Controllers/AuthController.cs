using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Controllers;

public class AuthController : ApiController
{
    readonly IAuthService accManager;
    public AuthController(IAuthService accManager)
        => this.accManager = accManager;

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterModel register)
        => await ReturnOkIfEverithingIsGood(async () => await accManager.RegisterUserAsync(register));


    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(LoginModel login)
        => await ReturnOkIfEverithingIsGood(async () => await accManager.LoginUserAsync(login));


    [Authorize]
    [HttpPost(nameof(Logout))]
    public async Task<IActionResult> Logout()
        => await ReturnOkIfEverithingIsGood(async () => await accManager.LogoutUserAsync());

    [HttpGet(nameof(CurrentUserInfo))]
    public Task<CurrentUser> CurrentUserInfo()
    {
        var currentUser = new CurrentUser
        {
            IsAuthenticated = User.Identity.IsAuthenticated,
            UserName = User.Identity.Name,
            Claims = User.Claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value)).ToList()
        };
        return Task.FromResult(currentUser);
    }
}
