using System.Threading.Tasks;
using ChatApp.Models;
using ChatApp.Models.Authentication;

namespace ChatApp.Test.Doubles
{
    public class AuthFake : IAuthApi
    {
        public async Task<BaseResponse> ValidateSession()
        {
            var fakeRes = new BaseResponse();

            fakeRes.Status = 2;

            return fakeRes;
        }

        public Task<BaseResponse> SignUp(string userName, string userPassword, string confirmPassword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuthData> LogIn(string userName, string userPassword)
        {
            var fakeRes = new AuthData();
            
            var fakeUser = new User();
            fakeUser.UserName = "User";

            fakeRes.Status = 1;
            fakeRes.Token = "Token";
            fakeRes.User = fakeUser;

            return fakeRes;
        }
    }
}