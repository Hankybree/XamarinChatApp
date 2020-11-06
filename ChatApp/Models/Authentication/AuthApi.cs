using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Services;
using Newtonsoft.Json;

namespace ChatApp.Models.Authentication
{
    public class AuthApi : IAuthApi
    {
        private readonly Uri _signUpUri = new Uri(Keys.BaseApiUrl + "/auth/signup");
        private readonly Uri _logInUri = new Uri(Keys.BaseApiUrl + "/auth/login");
        private readonly Uri _validateUri = new Uri(Keys.BaseApiUrl + "/auth/validate");

        private readonly HttpClient _client;
        private readonly IPreferences _preferences;

        public AuthApi(IPreferences preferences, HttpClient client)
        {
            _client = client;
            _preferences = preferences;
        }

        public async Task<BaseResponse> ValidateSession()
        {
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _preferences.GetString
                    (Keys.TokenString));
            
            var response = await _client.GetAsync(_validateUri);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BaseResponse>(content);

            return result;
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