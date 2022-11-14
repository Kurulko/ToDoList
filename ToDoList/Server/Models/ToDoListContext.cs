using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Server.Models;

public class ToDoListContext : IdentityDbContext<User>
{
    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    public ToDoListContext(DbContextOptions<ToDoListContext> opts) : base(opts) 
        => Database.EnsureCreated();
}
