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
        private ChatApi _chatApi;

        public ChatRoomPageViewModel(IPreferences preferences, INavigationService navigation, 
            ChatApi chatApi)
        {
            _chatApi = chatApi;
            
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
        
        // Buttons
        public ICommand SendButtonPressed { private set; get; }
        public ICommand LogOutButtonPressed { private set; get; }
        
        private List<ICommand> buttonCommands = new List<ICommand>();
    }
}