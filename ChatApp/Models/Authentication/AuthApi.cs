using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatApp.Models.Authentication
{
    public class AuthApi
    {
        private static string _baseUrl = "http://192.168.1.173:4000/auth/";
        private Uri _signUpUri = new Uri(_baseUrl + "signup");
        private Uri _logInUri = new Uri(_baseUrl + "login");

        private HttpClient _client;

        public AuthApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<BaseResponse> SignUp(string userName, string userPassword, string confirmPassword)
        {
            if (userPassword == confirmPassword)
            {
                AuthPostBody body = new AuthPostBody(userName, userPassword);
                HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            
                var response = await _client.PostAsync(_signUpUri, stringContent);
            
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseResponse>(content);
            
                return result;
            }
            else
            {
                return null;
            }
        }

        // public async Task<AuthData> LogIn()
        // {
        //     
        // }
    }
}