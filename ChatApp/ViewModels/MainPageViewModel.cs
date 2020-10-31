using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ChatApp.Annotations;
using ChatApp.Services;
using ChatApp.Views;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private IHelloService _helloService;
        public MainPageViewModel(IHelloService helloService)
        {
            _helloService = helloService;
            _helloService.GetMessage();
            
            LogInButtonPressed = new Command(execute: () =>
            {
                Header = "You logged in!";
                Console.WriteLine("Log in pressed");
            }, canExecute: () => !header.Equals("You logged in!"));

            SignUpButtonPressed = new Command(execute: () =>
            { 
                Header = "You signed up!";
                Console.WriteLine("Sign up pressed");
            }, canExecute: () => !Header.Equals("You signed up!"));
            
            buttonCommands.Add(LogInButtonPressed);
            buttonCommands.Add(SignUpButtonPressed);
        }
        
        
        // Properties
        private string header = "Welcome to BitChat!";
        public string Header
        {
            get => header;
            set
            {
                if (header == value || value == null) return;
                header = value;
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