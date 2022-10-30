using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Server.Models
{
    public class ToDoListContext : IdentityDbContext<User>
    {
        public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        //public DbSet<Subcategory> Subcategories { get; set; } = null!;

        public ToDoListContext(DbContextOptions<ToDoListContext> opts) : base(opts) 
            => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>()
            //    .HasMany(u => u.Categories)
            //    .WithOne(c => c.User)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<User>()
            //    .HasMany(u => u.Subcategories)
            //    .WithOne(c => c.User)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<User>()
            //    .HasMany(u => u.ToDoItems)
            //    .WithOne(c => c.User)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Category>()
            //    .HasMany(u => u.Subcategories)
            //    .WithOne(c => c.Category)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Category>()
            //    .HasMany(u => u.ToDoItems)
            //    .WithOne(c => c.Category)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<ToDoItem>()
            //    .HasOne(i => i.Subcategory)
            //    .WithMany(s => s.ToDoItems)
            //    .OnDelete(DeleteBehavior.NoAction);



            //builder.Entity<User>()
            //    .HasMany(u => u.Categories)
            //    .WithOne(c => c.User)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<ToDoItem>()
            //    .HasOne(i => i.Subcategory)
            //    .WithMany(s => s.ToDoItems)
            //    .OnDelete(DeleteBehavior.NoAction);


            //var categories = new List<Category>() { new() { Id = 1, Name = "Work" }, new() { Id = 2, Name = "Study" }, new() { Id = 3, Name = "Others" } };
            //builder.Entity<Category>().HasData(categories);

            base.OnModelCreating(builder);
        }
    }
}
