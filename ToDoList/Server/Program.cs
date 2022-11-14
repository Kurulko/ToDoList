using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Services;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using ToDoList.Server.Initializer;
using ToDoList.Shared.Services;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

IServiceCollection services = builder.Services;

string connection = config.GetConnectionString("DefaultConnection");
services.AddDbContext<ToDoListContext>(opts =>
{
    opts.UseSqlServer(connection);
    opts.EnableSensitiveDataLogging();
});

services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ToDoListContext>();
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts => {
    opts.Cookie.HttpOnly = false;
    opts.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

services.AddScoped<IAuthService, AuthManager>();
services.AddScoped<IToDoItemService, ToDoItemManager>();
services.AddScoped<IUserService, UsersManager>();
services.AddScoped<ICategoryService, CategoryManager>();
services.AddScoped<IRoleService, RolesManager>();

services.AddControllers().AddNewtonsoftJson();
services.AddSwaggerGen();
services.AddRazorPages();


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;

    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(roleManager);

    string adminName = config.GetValue<string>("Admin:Name");
    string adminPassword = config.GetValue<string>("Admin:Password");
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    await UsersInitializer.AdminInitializeAsync(userManager, adminName, adminPassword);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

