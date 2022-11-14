using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Server.Services;
using ToDoList.Shared;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models;

namespace ToDoList.Server.Controllers;

[Authorize]
public class ToDoListController : ApiController
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

    [HttpPost]
    public async Task<IActionResult> AddItem(ModelWithUserId<ToDoItem> model)
        => await ReturnOkIfEverithingIsGood(async () => await iToDoList.AddToDoItemAsync(model));

    [HttpPut]
    public async Task<IActionResult> UpdateItem(ModelWithUserId<ToDoItem> model)
        => await ReturnOkIfEverithingIsGood(async () => await iToDoList.UpdateToDoItem(model));

    [HttpDelete("{userId}/{toDoItemId:long}")]
    public async Task<IActionResult> DeleteItem(string userId, long toDoItemId)
        => await ReturnOkIfEverithingIsGood(async () => await iToDoList.DeleteToDoItem(userId, toDoItemId));
}
