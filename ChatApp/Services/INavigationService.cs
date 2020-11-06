using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp.Services
{
    public interface INavigationService
    {
        Task PushAsync(string page);
        Task<Page> PopAsync();
        Task PushModalAsync(string page);
        Task<Page> PopModalAsync();
    }
}