using System;
using System.Collections.Generic;
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
                var content = await PostAuth(userName, userPassword, _signUpUri);
                
                var result = JsonConvert.DeserializeObject<BaseResponse>(content);
            
                return result;
            }

            return null;
        }

        public async Task<AuthData> LogIn(string userName, string userPassword)
        {
            var content = await PostAuth(userName, userPassword, _logInUri);
            
            var result = JsonConvert.DeserializeObject<AuthData>(content);

            return result;
        }

        private async Task<string> PostAuth(string userName, string userPassword, Uri uri)
        {
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"userName", userName},
                {"userPassword", userPassword}
            };
            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(body), 
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, stringContent);
            
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}