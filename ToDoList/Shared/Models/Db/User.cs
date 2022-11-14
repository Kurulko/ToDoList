using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Shared.Models.Db;

public class User : IdentityUser
{
    [Display(Name = "Registered datetime")]
    public DateTime DateTime { get; set; }

    public IList<ToDoItem>? ToDoItems { get; set; }
    public IList<Category>? Categories { get; set; }
}
