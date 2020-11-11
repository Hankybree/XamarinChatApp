namespace ChatApp.Models
{
    public static class Keys
    {
        private const string _localUrl = "192.168.1.173:4000";
        private const string _globalUrl = "195.201.32.3:4000";

        public const string TokenString = "token";
        public const string UserNameString = "user";
        public const string BaseApiUrl = "http://" + _globalUrl;
        public const string BaseSocketUrl = "ws://" + _globalUrl;

        public const string ChatRoomPageString = "ChatRoomPage";
        public const string SignUpPageString = "SignUpPage";
    }
}