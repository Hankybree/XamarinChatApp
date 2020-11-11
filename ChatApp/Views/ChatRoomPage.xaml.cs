using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ChatApp.Setup;
using ChatApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatRoomPage : ContentPage
    {
        private readonly ChatRoomPageViewModel _chatRoomPageViewModel;
        public ChatRoomPage()
        {
            InitializeComponent();

            BindingContext = _chatRoomPageViewModel = AppContainer.Container.Resolve<ChatRoomPageViewModel>();
        }

        protected override void OnAppearing()
        {
            _chatRoomPageViewModel.LoadMessages();
            _chatRoomPageViewModel.Connect();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _chatRoomPageViewModel.Disconnect();
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}