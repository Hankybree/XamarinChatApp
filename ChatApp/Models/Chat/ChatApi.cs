using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatApp.Models.Chat
{
    public class ChatApi
    {
        private readonly Uri _messageUri = new Uri(Keys.BaseApiUrl + "/chat/messages");

        private readonly string _token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyTmFtZSI6ImR1ZGRhbiIsInVzZXJJZCI6NSwiaWF0IjoxNjA0MTYwMDkzLCJleHAiOjE2MDQ3NjQ4OTN9.WerB0slubbjZi3R2nIV7yr_5bEmnNkUX9uPcVIewrRg";

        private readonly HttpClient _client;

        public ChatApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<Message[]> GetMessages()
        {
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _token);

            var response = await _client.GetAsync(_messageUri);
            
            var content = await response.Content.ReadAsStringAsync();  
            
            var result = JsonConvert.DeserializeObject<Message[]>(content);

            return result;
        }
    }
}