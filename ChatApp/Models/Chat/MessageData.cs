namespace ChatApp.Models.Chat
{
    public class MessageData : BaseResponse
    {
        public Message[] Messages { get; set; }
    }
}