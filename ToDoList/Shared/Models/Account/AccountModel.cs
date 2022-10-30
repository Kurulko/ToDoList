using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Shared.Models.Account
{
    public abstract class AccountModel
    {
        [Display(Name = "Your name*")]
        [Required(ErrorMessage = "Enter your name!")]
        public string Name { get; set; }

        [Display(Name = "Your password*")]
        [Required(ErrorMessage = "Enter your password!")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "{0} must be at least {1} characters long")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
