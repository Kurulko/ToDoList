using Microsoft.AspNetCore.Identity;
using ToDoList.Shared;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Server.Initializer;

public class UsersInitializer
{
    public static async Task AdminInitializeAsync(UserManager<User> userManager, string name, string password)
    {
        if (await userManager.FindByNameAsync(name) is null)
        {
            User admin = new() { UserName = name, DateTime = DateTime.Now };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(user: admin, role: Roles.Admin);
        }
    }
}
