namespace ChatApp.Models
{
    public static class Keys
    {
        private static string _localUrl = "192.168.0.6:4000";
        private static string _globalUrl = "195.201.32.3:4000";

        public static string TokenString = "token";
        public static string UserNameString = "user";
        public static string BaseApiUrl = "http://" + _localUrl;
        public static string BaseSocketUrl = "ws://" + _globalUrl;
    }
}