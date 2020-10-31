namespace ChatApp.Models.Authentication
{
    public class AuthPostBody
    {
        public string userName { get; set; }
        public string userPassword { get; set; }

        public AuthPostBody(string userName, string userPassword)
        {
            this.userName = userName;
            this.userPassword = userPassword;
        }
    }
}