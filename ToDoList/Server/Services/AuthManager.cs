using Microsoft.AspNetCore.Identity;
using ToDoList.Server.Models;
using ToDoList.Shared;
using ToDoList.Shared.Models;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Services
{
    public class AuthManager : IAuthService
    {
        readonly SignInManager<User> signInManager;
        readonly UserManager<User> userManager;
        readonly ToDoListContext db;
        public AuthManager(SignInManager<User> signInManager, UserManager<User> userManager, ToDoListContext db)
            => (this.signInManager, this.userManager, this.db) = (signInManager, userManager, db);

        public async Task LoginUserAsync(LoginModel login)
        {
            SignInResult res = await signInManager.PasswordSignInAsync(login.Name, login.Password, login.RememberMe, false);
            if (!res.Succeeded)
                throw new Exception("Password or/and login invalid");
        }

        async Task AddCategoriesToUser(User user)
        {
            var categories = new List<Category>() { new() { Name = "Work" }, new() { Name = "Study" }, new() {  Name = "Others" } };
            user.Categories = categories;
            await db.SaveChangesAsync();
        }
        public async Task RegisterUserAsync(RegisterModel register)
        {
            User user = (User)register;
            user.DateTime = DateTime.Now;
            IdentityResult result = await userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, register.RememberMe);
                await userManager.AddToRolesAsync(user, new List<string>() { Roles.User });
                await AddCategoriesToUser(user);
            }
            else
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        public async Task LogoutUserAsync()
            => await signInManager.SignOutAsync();
    }
}
