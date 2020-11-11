using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChatApp.Services
{
    public class Speaker
    {
        public async Task Speak(string text)
        {
            try
            {
                await TextToSpeech.SpeakAsync(text);
            }
            catch (Exception e)
            {
                Console.WriteLine("SPEAKERROR: " + e);
            }
        }
    }
}