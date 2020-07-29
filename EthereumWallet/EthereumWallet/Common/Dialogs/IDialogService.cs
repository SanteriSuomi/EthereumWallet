using System.Threading.Tasks;

namespace EthereumWallet.Common.Dialogs
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<string> DisplayPrompt(string title, string message);
        Task<string> DisplayActionSheet(string title, string destruction, params string[] buttons);
    }
}