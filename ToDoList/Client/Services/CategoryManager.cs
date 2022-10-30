using System.Net.Http.Json;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Client.Services
{
    public class CategoryManager : APIManager, ICategoryService
    {
        public CategoryManager(HttpClient httpClient) :
            base(httpClient, "api/Categories/") { }

        public async Task AddCategoryAsync(string userId, Category category)
            => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl + userId, category));

        public async Task DeleteCategoryAsync(string userId, long categoryId)
            => await CheckResponseMessage(await httpClient.DeleteAsync(baseApiUrl + $"{userId}/{categoryId}"));

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string userId)
            => (await httpClient.GetFromJsonAsync<IEnumerable<Category>>(baseApiUrl + userId))!;

        public async Task<Category> GetCategoryAsync(string userId, long categoryId)
            => (await httpClient.GetFromJsonAsync<Category>(baseApiUrl + $"{userId}/{categoryId}"))!;

        public async Task UpdateCategoryAsync(string userId, Category category)
            => await CheckResponseMessage(await httpClient.PutAsJsonAsync(baseApiUrl + userId, category));
    }
}
