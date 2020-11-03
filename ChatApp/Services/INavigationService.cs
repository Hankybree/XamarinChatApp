using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp.Services
{
    public interface INavigationService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();
    }
}