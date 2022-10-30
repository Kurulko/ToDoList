using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models.Account;

namespace ToDoList.Shared.Services
{
    public interface IAuthService
    {
        Task LoginUserAsync(LoginModel model);
        Task RegisterUserAsync(RegisterModel model);
        Task LogoutUserAsync();
    }
}
