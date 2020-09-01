using AnamelTask.ViewModel.GeneralResponse;
using AnamelTask.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnamelTask.Services.UserServices.User
{
    public interface IUserService
    {
        Task<ResponseObjVM<string>> Login(LoginVM loginVM);
        ResponseObjVM<string> Register(RegisterVM registerVM);
    }
}
