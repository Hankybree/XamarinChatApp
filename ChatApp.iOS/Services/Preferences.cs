using ChatApp.Models;
using ChatApp.Services;
using Foundation;

namespace ChatApp.iOS.Services
{
    public class Preferences : IPreferences
    {
        private readonly NSUserDefaults _prefs;
        
        public Preferences()
        {
            _prefs = NSUserDefaults.StandardUserDefaults;
        }
        
        public void SetString(string key, string value)
        {
            _prefs.SetString(value,key);
            _prefs.Synchronize();
        }

        public string GetString(string key)
        {
            return _prefs.StringForKey(key);
        }

        public void ClearPreferences()
        {
            _prefs.RemoveObject(Keys.TokenString);
            _prefs.RemoveObject(Keys.UserNameString);
        }
    }
}