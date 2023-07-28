using Business.ViewModels.FAQCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface IFAQService
    {
        Task<FAQIndexVM> GetAllAsync();
    }
}
