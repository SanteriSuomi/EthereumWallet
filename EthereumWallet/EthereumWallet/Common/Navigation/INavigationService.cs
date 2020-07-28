using EthereumWallet.Modules.Base;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Navigation
{
    public interface INavigationService
    {
        Task<bool> PushAsync<TViewModel>(object parameter = null, bool animated = true) where TViewModel : BaseViewModel;
        Task PopAsync();
    }
}