using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Shared.Models.Db
{
    public class User : IdentityUser
    {
        public IList<ToDoItem>? ToDoItems { get; set; }
        //public IList<Subcategory>? Subcategories { get; set; }
        public IList<Category>? Categories { get; set; }
        public DateTime DateTime { get; set; }
    }
}
