using EthereumWallet.Common.Data;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Networking;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EthereumWallet.Modules.Wallet.Info
{
    public class WalletInfoViewModel : BaseViewModel
    {
        public WalletInfoViewModel(IWeb3Service web3Service, INetworkService networkService)
        {
            _web3Service = web3Service;
            _networkService = networkService;
            GetInfo().SafeFireAndForget(true);
        }

        private async Task GetInfo()
        {
            Info = await _networkService.GetAsync<AddressInfo>(ApiHelpers.GetEthplorerUri($"getAddressInfo/{_web3Service.Account.Address}"));
            Tokens = new ObservableCollection<Token>(Info.tokens);
        }

        private AddressInfo _info;
        public AddressInfo Info
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Token> _tokens;
        public ObservableCollection<Token> Tokens
        {
            get => _tokens;
            set
            {
                _tokens = value;
                OnPropertyChanged();
            }
        }

        private readonly IWeb3Service _web3Service;
        private readonly INetworkService _networkService;
    }
}