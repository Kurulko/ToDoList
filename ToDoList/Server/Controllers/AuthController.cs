using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        readonly IAuthService accManager;
        public AuthController(IAuthService accManager)
            => this.accManager = accManager;

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            try
            {
                await accManager.RegisterUserAsync(register);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                await accManager.LoginUserAsync(login);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize, HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await accManager.LogoutUserAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public CurrentUser CurrentUserInfo()
            => new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            };
    }
}
