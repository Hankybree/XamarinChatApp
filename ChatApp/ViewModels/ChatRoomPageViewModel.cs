using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatApp.Models.Chat;
using ChatApp.Services;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ChatApp.ViewModels
{
    public class ChatRoomPageViewModel : BaseViewModel, IObserver<Message>
    {
        private readonly ChatApi _chatApi;
        private readonly ChatClient _chatClient;
        
        public ChatRoomPageViewModel(IPreferences preferences, INavigationService navigation, 
            ChatApi chatApi, ChatClient chatClient)
        {
            _chatApi = chatApi;
            _chatClient = chatClient;
            
            Items = new ObservableCollection<Message>();

            SendButtonPressed = new Command(execute: async () =>
            {
                await _chatClient.SendMessage(_message);
                Message = "";
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
        private IDisposable _unSubscriber;
        public ObservableCollection<Message> Items { get; }

        private string _message = "";
        public string Message
        {
            get => _message;
            set
            {
                if (_message == value || value == null) return;
                _message = value;
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
        }

        public async void LoadMessages()
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

        public void Connect()
        {
            Subscribe(_chatClient);
            //_chatClient.Connect();
        }

        public void Disconnect()
        {
            _chatClient.Disconnect();
            Unsubscribe();
        }

        // Observer
        private void Subscribe(IObservable<Message> provider)
        {
            _unSubscriber = provider.Subscribe(this);
        }

        private void Unsubscribe()
        {
            _unSubscriber.Dispose();
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Message value)
        {
            Items.Add(value);
        }
    }
}