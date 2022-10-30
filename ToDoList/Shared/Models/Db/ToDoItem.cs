using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Shared.Models.Db
{
    public class ToDoItem
    {
        public long Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please, enter a name")]
        public string Name { get; set; }

        [Display(Name = "How important?")]
        [Required(ErrorMessage = "Please, choose priority")]
        public Priority Priority { get; set; } = Priority.Medium;

        [Display(Name = "Deadline")]
        [Required(ErrorMessage = "Please, choose a deadline")]
        public DateTime Deadline { get; set; } = DateTime.Now;

        [Display(Name = "How difficult?")]
        [Range(1, 10, ErrorMessage = "Please, choose a value between {1} and {2}")]
        [Required(ErrorMessage = "Please, choose a complication")]
        public int Complication { get; set; } = 1;

        [Display(Name = "Is completed?")]
        public bool IsCompleted { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please, choose a category")]
        public long CategoryId { get; set; }
        public Category? Category { get; set; }
        //public long SubcategoryId { get; set; }
        //public Subcategory? Subcategory { get; set; }
    }
}
