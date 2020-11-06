using ChatApp.Models;
using ChatApp.Services;

namespace ChatApp.Test.Doubles
{
    public class PreferencesSpy : IPreferences
    {
        public string Token { get; set; }
        public string User { get; set; }
        
        public void SetString(string key, string value)
        {
            if (key == Keys.TokenString)
            {
                Token = value;
            } 
            else if (key == Keys.UserNameString)
            {
                User = value;
            }
        }

        public string GetString(string key)
        {
            return null;
        }

        public void ClearPreferences()
        {
            throw new System.NotImplementedException();
        }
    }
}