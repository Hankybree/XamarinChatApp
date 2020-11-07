using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
            
            Items = new ObservableCollection<Message>();
            LoadMessages();
            
            SendButtonPressed = new Command(execute: () =>
            {
                
            }, canExecute: () => true);
            LogOutButtonPressed = new Command(execute: async() =>
            {
                preferences.ClearPreferences();

                await navigation.PopModalAsync();
            }, canExecute: () => true);
            ExecuteLoadMessages = new Command(() => LoadMessages(), 
                () => true);
            
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
        public ICommand ExecuteLoadMessages { private set; get; }
        
        private List<ICommand> buttonCommands = new List<ICommand>();
        
        // Methods
        private async Task<Message[]> GetMessages()
        {
            var messageData = await _chatApi.GetMessages();

            return messageData.Messages;

            // Text = messageData.Messages[0].UserName + ": " + messageData.Messages[0].Content;
        }

        private async void LoadMessages()
        {
            IsBusy = true;
            var items = await GetMessages();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);   
            }
            IsBusy = false;
        }
    }
}