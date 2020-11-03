using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ChatApp.Models.Chat;
using ChatApp.Services;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class ChatRoomPageViewModel : BaseViewModel
    {
        private readonly ChatApi _chatApi;
        public ChatRoomPageViewModel(IPreferences preferences, INavigationService navigation, 
            ChatApi chatApi)
        {
            _chatApi = chatApi;
            
            GetMessages();
            
            SendButtonPressed = new Command(execute: () =>
            {
                
            }, canExecute: () => true);
            LogOutButtonPressed = new Command(execute: async() =>
            {
                preferences.ClearPreferences();

                await navigation.PopModalAsync();
            }, canExecute: () => true);
            
            buttonCommands.Add(SendButtonPressed);
            buttonCommands.Add(LogOutButtonPressed);
        }
        
        // Properties
        public ObservableCollection<Message> Items { get; }

        private string _text = "message";
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value || value == null) return;
                _text = value;
                OnPropertyChanged();
                RefreshCanExecute(buttonCommands);
            }
        }

        // Buttons
        public ICommand SendButtonPressed { private set; get; }
        public ICommand LogOutButtonPressed { private set; get; }
        
        private List<ICommand> buttonCommands = new List<ICommand>();
        
        // Methods
        private async void GetMessages()
        {
            var messages = await _chatApi.GetMessages();

            Text = messages[0].Msg;
        }
    }
}