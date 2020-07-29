using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;

namespace EthereumWallet.Modules.Wallet
{
    public class WalletViewModel : BaseViewModel
    {
        public WalletViewModel(IWeb3Service web3Service)
        {
            _web3Service = web3Service;
        }

        private readonly IWeb3Service _web3Service;
    }
}