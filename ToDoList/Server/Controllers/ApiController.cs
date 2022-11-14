using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    protected async Task<IActionResult> ReturnOkIfEverithingIsGood(Func<Task> action)
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
