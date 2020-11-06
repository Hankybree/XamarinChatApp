using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApp.Models;
using ChatApp.Views;
using Xamarin.Forms;

namespace ChatApp.Services
{
    public class NavigationService : INavigationService
    {
        private Dictionary<string, Type> _pages = new Dictionary<string, Type>()
        {
            {Keys.ChatRoomPageString, typeof(ChatRoomPage)},
            {Keys.SignUpPageString, typeof(SignUpPage)}
            
        };
        
        public async Task PushAsync(string page)
        {
            await Application.Current.MainPage.Navigation.PushAsync((Page)Activator.CreateInstance(_pages[page]));
        }

        public async Task<Page> PopAsync()
        {
            return await Application.Current.MainPage.Navigation.PopAsync();
        }
        
        public async Task PushModalAsync(string page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync((Page)Activator.CreateInstance(_pages[page]));
        }

        public async Task<Page> PopModalAsync()
        {
            return await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}