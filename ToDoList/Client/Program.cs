using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoList.Client;
using ToDoList.Client.Services;
using ToDoList.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


var rootComponents = builder.RootComponents;

rootComponents.Add<App>("#app");
rootComponents.Add<HeadOutlet>("head::after");


IServiceCollection services = builder.Services;

services.AddScoped<IAuthService, AuthManager>();
services.AddScoped<IToDoItemService, ToDoItemManager>();
services.AddScoped<ICategoryService, CategoryManager>();
services.AddScoped<IRoleService, RolesManager>();
services.AddScoped<IUserService, UsersManager>();

services.AddOptions();
services.AddAuthorizationCore();
services.AddScoped<CustomStateProvider>();
services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();
