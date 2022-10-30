using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Shared;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
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
    }
}
