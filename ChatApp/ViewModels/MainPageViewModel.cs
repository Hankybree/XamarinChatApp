using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ChatApp.Annotations;
using ChatApp.Models;
using ChatApp.Models.Authentication;
using ChatApp.Services;
using ChatApp.Views;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IAuthApi _authApi;
        private readonly INavigationService _navigation;
        private readonly IPreferences _preferences;
        private readonly Speaker _speaker;
        
        public MainPageViewModel(IPreferences preferences, INavigationService navigation,
            Speaker speaker, IAuthApi authApi)
        {
            _authApi = authApi;
            _navigation = navigation;
            _preferences = preferences;
            _speaker = speaker;
            
            ValidateSession();
            
            LogInButtonPressed = new Command(execute: async () =>
            {
                var response = await authApi.LogIn(_userName, _password);
                
                if (response.Status == 1)
                {
                    UserName = "";
                    Password = "";   
                    
                    preferences.SetString(Keys.TokenString, response.Token);
                    preferences.SetString(Keys.UserNameString, response.User.UserName);

                    await navigation.PushModalAsync(Keys.ChatRoomPageString);
                }
                else
                {
                    await _speaker.Speak(response.Msg);
                }
            }, canExecute: () => true);

            SignUpButtonPressed = new Command(execute: async () =>
            {
                await navigation.PushAsync(Keys.SignUpPageString);
            }, canExecute: () => true);
            
            buttonCommands.Add(LogInButtonPressed);
            buttonCommands.Add(SignUpButtonPressed);
        }

        // Properties
        private string _userName = "";
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value || value == null) return;
                _userName = value;
                OnPropertyChanged();
                RefreshCanExecute(buttonCommands);
            }
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value || value == null) return;
                _password = value;
                OnPropertyChanged();
                RefreshCanExecute(buttonCommands);
            }
        }
        
        // Buttons
        public ICommand LogInButtonPressed { private set; get; }
        public ICommand SignUpButtonPressed { private set; get; }
        
        private List<ICommand> buttonCommands = new List<ICommand>();
        
        // Methods
        private async void ValidateSession()
        {
            if (_preferences.GetString(Keys.TokenString) != null)
            {
                var result = await _authApi.ValidateSession();

                if (result.Status == 1)
                {
                    await _navigation.PushModalAsync(Keys.ChatRoomPageString);
                }
            }
        }
    }
}