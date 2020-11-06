using System.Threading.Tasks;
using ChatApp.Services;
using Xamarin.Forms;

namespace ChatApp.Test.Doubles
{
    public class NavigationStub : INavigationService
    {
        public Task PushAsync(string page)
        {
            throw new System.NotImplementedException();
        }

        public Task<Page> PopAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task PushModalAsync(string page)
        {
            return Task.CompletedTask;
        }

        public Task<Page> PopModalAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}