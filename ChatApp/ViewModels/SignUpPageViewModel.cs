using System;
using System.Windows.Input;
using ChatApp.Models;
using ChatApp.Models.Authentication;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        private AuthApi _authApi;

        public SignUpPageViewModel(AuthApi authApi)
        {
            _authApi = authApi;

            SignUpButtonPressed = new Command(execute: async () =>
            {
                var response = await _authApi.SignUp("Klasse", "secret");
                Console.WriteLine("ResponseFromServer: Status " + response.Status + " message: " + response.Msg);
            }, canExecute: () => true);
        }

        public ICommand SignUpButtonPressed { private set; get; }
    }
}