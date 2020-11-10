namespace ChatApp.Models.Chat
{
    public class Message
    {
        public string UserName { get; set; }
        public string Content { get; set; }

        public Message(string userName, string content)
        {
            UserName = userName;
            Content = content;
        }
    }
}