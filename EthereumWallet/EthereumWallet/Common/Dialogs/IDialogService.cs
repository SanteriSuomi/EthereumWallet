using System.Threading.Tasks;

namespace EthereumWallet.Common.Dialogs
{
    public interface IDialogService
    {
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task<string> DisplayPrompt(string title, string message);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    }
}