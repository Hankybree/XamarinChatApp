using Android.App;
using Android.Content;
using ChatApp.Services;

namespace ChatApp.Android.Services
{
    public class Preferences : IPreferences
    {
        private readonly ISharedPreferences _prefs;
        private readonly ISharedPreferencesEditor _prefEditor;
        
        public Preferences()
        {
            _prefs = Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
            _prefEditor = _prefs.Edit();
        }
        
        public void SetString(string key, string value)
        {
            _prefEditor.PutString(key, value);
            _prefEditor.Commit();
        }
        
        public string GetString(string key)
        {
            return _prefs.GetString(key, null);
        }
        
        public void ClearPreferences()
        {
            _prefEditor.Clear();
            _prefEditor.Commit();
        }
    }
}