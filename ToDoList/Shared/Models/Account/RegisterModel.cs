using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Shared.Models.Account
{
    public class RegisterModel : AccountModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Confirm password*")]
        [Required(ErrorMessage = "Repeat password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least {1} characters long")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; }

        public static explicit operator User(RegisterModel register)
            => new() { Email = register.Email, UserName = register.Name, DateTime = DateTime.Now};
    }
}
