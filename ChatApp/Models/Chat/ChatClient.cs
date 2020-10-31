using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatApp.Models.Chat
{
    public class ChatClient
    {
        private const string BaseUrl = "http://192.168.1.173:4000/chat/";
        private readonly Uri _messageUri = new Uri(BaseUrl + "messages");
        
        private readonly string _token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyTmFtZSI6ImR1ZGRhbiIsInVzZXJJZCI6NSwiaWF0IjoxNjA0MTYwMDkzLCJleHAiOjE2MDQ3NjQ4OTN9.WerB0slubbjZi3R2nIV7yr_5bEmnNkUX9uPcVIewrRg";

        private readonly HttpClient _client;

        public ChatClient(HttpClient client)
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