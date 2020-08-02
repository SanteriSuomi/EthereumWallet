using System.Threading.Tasks;
using Xamarin.Forms;

namespace EthereumWallet.Common.Dialogs
{
    public class DialogService : IDialogService
    {
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public Task<string> DisplayPrompt(string title, string message)
        {
            return Application.Current.MainPage.DisplayPromptAsync(title, message);
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }
    }
}