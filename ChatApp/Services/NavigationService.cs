using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp.Services
{
    public class NavigationService : INavigationService
    {
        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task<Page> PopAsync()
        {
            return await Application.Current.MainPage.Navigation.PopAsync();
        }
        
        public async Task PushModalAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task<Page> PopModalAsync()
        {
            return await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}