using Business.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(AccountRegisterVM model);
        Task<bool> LoginAsync(AccountLoginVM model);
    }
}
