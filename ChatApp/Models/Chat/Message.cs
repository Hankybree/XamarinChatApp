namespace ChatApp.Models.Chat
{
    public class Message : BaseResponse
    {
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}