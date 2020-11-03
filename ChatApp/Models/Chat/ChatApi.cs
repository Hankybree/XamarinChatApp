using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Services;
using Newtonsoft.Json;

namespace ChatApp.Models.Chat
{
    public class ChatApi
    {
        private readonly Uri _messageUri = new Uri(Keys.BaseApiUrl + "/chat/messages");

        private readonly HttpClient _client;
        private readonly IPreferences _preferences;

        public ChatApi(IPreferences preferences, HttpClient client)
        {
            _client = client;
            _preferences = preferences;
        }

        public async Task<Message[]> GetMessages()
        {
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", 
                    _preferences.GetString(Keys.TokenString));

            var response = await _client.GetAsync(_messageUri);
            
            var content = await response.Content.ReadAsStringAsync();  
            
            var result = JsonConvert.DeserializeObject<Message[]>(content);

            return result;
        }
    }
}