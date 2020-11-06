using System;
using System.Collections.Generic;
using System.Windows.Input;
using ChatApp.Models;
using ChatApp.Models.Authentication;
using ChatApp.Services;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        public SignUpPageViewModel(INavigationService navigation, IAuthApi authApi)
        {
            SignUpButtonPressed = new Command(execute: async () =>
            {
                var response = await authApi.SignUp(_userName, _password, _confirmPassword);

                if (response == null)
                {
                    Console.WriteLine("Passwords does not match");
                    return;
                }
                
                Console.WriteLine("ResponseFromServer: Status " + response.Status + " message: " + response.Msg);
                UserName = "";
                Password = "";
                ConfirmPassword = "";

                await navigation.PopAsync();
            }, canExecute: () => true);
            
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
        
        private string _confirmPassword = "";
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword == value || value == null) return;
                _confirmPassword = value;
                OnPropertyChanged();
                RefreshCanExecute(buttonCommands);
            }
        }
        
        // Buttons
        public ICommand SignUpButtonPressed { private set; get; }
        
        private List<ICommand> buttonCommands = new List<ICommand>();
    }
}