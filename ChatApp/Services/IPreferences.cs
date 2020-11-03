namespace ChatApp.Services
{
    public interface IPreferences
    {
        void SetString(string key, string value);
        string GetString(string key);
        void ClearPreferences();
    }
}