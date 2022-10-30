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
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService iCategory;
        public CategoriesController(ICategoryService iCategory)
            => this.iCategory = iCategory;

        [HttpGet("{userId}")]
        public async Task<IEnumerable<Category>> Categories(string userId)
            => await iCategory.GetCategoriesAsync(userId);

        [HttpGet("{userId}/{categoryId:long}")]
        public async Task<Category> Categories(string userId, long categoryId)
            => await iCategory.GetCategoryAsync(userId, categoryId);

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddCategory(string userId, Category category)
            => await ReturnOkIfEverithingIsGood(async () => await iCategory.AddCategoryAsync(userId, category));

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateCategory(string userId, Category category)
            => await ReturnOkIfEverithingIsGood(async () => await iCategory.UpdateCategoryAsync(userId, category));

        [HttpDelete("{userId}/{categoryId:long}")]
        public async Task<IActionResult> DeleteCategory(string userId, long categoryId)
            => await ReturnOkIfEverithingIsGood(async () => await iCategory.DeleteCategoryAsync(userId, categoryId));

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