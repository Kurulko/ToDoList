using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Shared.Models.Db
{
    public class Subcategory
    {
        public long Id { get; set; }

        [Display(Name = "Subcategory name")]
        [Required(ErrorMessage = "Please, enter a name")]
        public string Name { get; set; }

        public long CategoryId { get; set; }
        public Category? Category { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
        public IList<ToDoItem>? ToDoItems { get; set; }
    }
}
