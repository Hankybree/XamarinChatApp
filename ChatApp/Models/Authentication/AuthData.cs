namespace ChatApp.Models.Authentication
{
    public class AuthData : BaseResponse
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}