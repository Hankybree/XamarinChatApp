using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ChatApp.Setup;
using ChatApp.ViewModels;
using Xamarin.Forms;

namespace ChatApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = AppContainer.Container.Resolve<MainPageViewModel>();
        }
    }
}