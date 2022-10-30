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

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ToDoListContext>(opts => opts.UseSqlServer(connection));

services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ToDoListContext>();
services.ConfigureApplicationCookie(opts =>
{
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
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(userManager, roleManager);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
