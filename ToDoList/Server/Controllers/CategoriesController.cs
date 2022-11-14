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
public class CategoriesController : ApiController
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

    [HttpPost]
    public async Task<IActionResult> AddCategory(ModelWithUserId<Category> model)
        => await ReturnOkIfEverithingIsGood(async () => await iCategory.AddCategoryAsync(model));

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(ModelWithUserId<Category> model)
        => await ReturnOkIfEverithingIsGood(async () => await iCategory.UpdateCategoryAsync(model));

    [HttpDelete("{userId}/{categoryId:long}")]
    public async Task<IActionResult> DeleteCategory(string userId, long categoryId)
        => await ReturnOkIfEverithingIsGood(async () => await iCategory.DeleteCategoryAsync(userId, categoryId));
}
