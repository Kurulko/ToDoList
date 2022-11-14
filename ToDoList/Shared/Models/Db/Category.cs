using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Shared.Models.Db;

public class Category
{
    public long Id { get; set; }

    [Display(Name = "Category name")]
    [Required(ErrorMessage = "Please, enter a name")]
    public string Name { get; set; }

    public IList<ToDoItem>? ToDoItems { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}
