using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ChatApp.Annotations;
using ChatApp.Models.Authentication;
using ChatApp.Services;
using ChatApp.Views;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private IHelloService _helloService;
        public MainPageViewModel(IHelloService helloService, AuthApi authApi)
        {
            _helloService = helloService;
            _helloService.GetMessage();
            
            LogInButtonPressed = new Command(execute: async () =>
            {
                var response = await authApi.LogIn(_userName, _password);
                
                Console.WriteLine("ResponseFromServer: Status: " + response.Status + " Message: " + 
                                  response.Msg + " Token: " + response.Token + " UserName: " + 
                                  response.User.UserName + " UserId: " + response.User.UserId);
                UserName = "";
                Password = "";
            }, canExecute: () => true);

            SignUpButtonPressed = new Command(execute: () =>
            {
                Console.WriteLine("Go to sign up page");
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
    }
}