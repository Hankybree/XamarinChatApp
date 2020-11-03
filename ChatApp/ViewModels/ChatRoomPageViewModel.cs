using System.Collections.Generic;
using System.Windows.Input;
using ChatApp.Models.Chat;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class ChatRoomPageViewModel : BaseViewModel
    {
        private ChatApi _chatApi;

        public ChatRoomPageViewModel(ChatApi chatApi)
        {
            _chatApi = chatApi;
            
            SendButtonPressed = new Command(execute: () =>
            {
                
            }, canExecute: () => true);
            LogOutButtonPressed = new Command(execute: () =>
            {
                
            }, canExecute: () => true);
            
            buttonCommands.Add(SendButtonPressed);
            buttonCommands.Add(LogOutButtonPressed);
        }
        
        // Properties
        
        
        // Buttons
        public ICommand SendButtonPressed { private set; get; }
        public ICommand LogOutButtonPressed { private set; get; }
        
        private List<ICommand> buttonCommands = new List<ICommand>();
    }
}