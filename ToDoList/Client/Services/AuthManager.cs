using System.Net;
using System.Net.Http.Json;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Services;

namespace ToDoList.Client.Services
{
    public class AuthManager : APIManager, IAuthService
    {
        public AuthManager(HttpClient httpClient) : 
            base(httpClient, "api/Auth/") { }

        public async Task<CurrentUser> CurrentUserInfo()
            => (await new HttpClient().GetFromJsonAsync<CurrentUser>(baseApiUrl + "CurrentUserInfo"))!;
        //=> await httpClient.GetFromJsonAsync<CurrentUser>(baseApiUrl + "CurrentUserInfo");
        
        public async Task LoginUserAsync(LoginModel model)
            => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl + "login", model));

        public async Task RegisterUserAsync(RegisterModel model)
            => await CheckResponseMessage(await httpClient.PostAsJsonAsync(baseApiUrl + "register", model));

        public async Task LogoutUserAsync()
            => await CheckResponseMessage(await httpClient.PostAsync(baseApiUrl + "logout", null));
    }
}
