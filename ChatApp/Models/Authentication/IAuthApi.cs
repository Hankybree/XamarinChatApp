using System;
using System.Threading.Tasks;

namespace ChatApp.Models.Authentication
{
    public interface IAuthApi
    {
        Task<BaseResponse> ValidateSession();
        Task<BaseResponse> SignUp(string userName, string userPassword, string confirmPassword);
        Task<AuthData> LogIn(string userName, string userPassword);
    }
}