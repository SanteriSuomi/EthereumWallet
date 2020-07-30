using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using System.Linq;

namespace EthereumWallet.Modules.WalletRoot
{
    public class WalletRootViewModel : BaseViewModel
    {
        private const int titleAddressLength = 20;

        public WalletRootViewModel(IWeb3Service web3Service)
        {
            _web3Service = web3Service;
            var addressString = new string(_web3Service.Account?.Address.Take(titleAddressLength).ToArray()) + "...";
            RootNavigationBarTitle = $"Wallet: {addressString}";
        }

        private readonly IWeb3Service _web3Service;

        public string RootNavigationBarTitle { get; set; }
    }
}