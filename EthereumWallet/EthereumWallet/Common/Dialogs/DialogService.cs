using System.Threading.Tasks;
using Xamarin.Forms;

namespace EthereumWallet.Common.Dialogs
{
    public class DialogService : IDialogService
    {
        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
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