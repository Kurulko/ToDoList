using System.Net.Http.Json;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models;

namespace ToDoList.Client.Services;

public class CategoryManager : APIManager, ICategoryService
{
    public CategoryManager(HttpClient httpClient) :
        base(httpClient, "api/Categories/") { }

    public async Task AddCategoryAsync(ModelWithUserId<Category> model)
        => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl, model));

    public async Task DeleteCategoryAsync(string userId, long categoryId)
        => await CheckResponseMessage(await httpClient.DeleteAsync(baseApiUrl + $"{userId}/{categoryId}"));

    public async Task<IEnumerable<Category>> GetCategoriesAsync(string userId)
        => (await httpClient.GetFromJsonAsync<IEnumerable<Category>>(baseApiUrl + userId))!;

    public async Task<Category> GetCategoryAsync(string userId, long categoryId)
        => (await httpClient.GetFromJsonAsync<Category>(baseApiUrl + $"{userId}/{categoryId}"))!;

    public async Task UpdateCategoryAsync(ModelWithUserId<Category> model)
        => await CheckResponseMessage(await httpClient.PutAsJsonAsync(baseApiUrl, model));
}
