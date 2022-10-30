using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Server.Services;
using ToDoList.Shared;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ToDoListController : ControllerBase
    {
        readonly IToDoItemService iToDoList;
        public ToDoListController(IToDoItemService iToDoList)
            => this.iToDoList = iToDoList;


        [HttpGet("{userId}")]
        public async Task<IEnumerable<ToDoItem>> ToDoList(string userId)
            => await iToDoList.GetToDoItemsAsync(userId);

        [HttpGet("{userId}/{toDoItemId:long}")]
        public async Task<ToDoItem> ToDoItem(string userId, long toDoItemId)
            => await iToDoList.GetToDoItemAsync(userId, toDoItemId); 

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddItem(string userId, ToDoItem item)
            => await ReturnOkIfEverithingIsGood(async () => await iToDoList.AddToDoItemAsync(userId, item));

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateItem(string userId, ToDoItem item)
            => await ReturnOkIfEverithingIsGood(async () => await iToDoList.UpdateToDoItem(userId, item));

        [HttpDelete("{userId}/{toDoItemId:long}")]
        public async Task<IActionResult> DeleteItem(string userId, long toDoItemId)
            => await ReturnOkIfEverithingIsGood(async () => await iToDoList.DeleteToDoItem(userId, toDoItemId));

        async Task<IActionResult> ReturnOkIfEverithingIsGood(Func<Task> action)
        {
            try
            {
                await action();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}