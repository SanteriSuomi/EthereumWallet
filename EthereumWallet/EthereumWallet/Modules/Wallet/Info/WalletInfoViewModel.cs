using EthereumWallet.Common.Data;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using EthereumWallet.Modules.Wallet.Info.TokenInfo;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EthereumWallet.Modules.Wallet.Info
{
    public class WalletInfoViewModel : BaseViewModel
    {
        public WalletInfoViewModel(IWeb3Service web3Service, INetworkService networkService, INavigationService navigationService)
        {
            _web3Service = web3Service;
            _networkService = networkService;
            _navigationService = navigationService;
            TokenListItemPressed = new Command<Token>((t) => OnTokenListItemPressed(t).SafeFireAndForget(true));
            RefreshPressed = new Command(() => OnRefreshPressed().SafeFireAndForget(true));
            UpdateWalletInfo().SafeFireAndForget(true);
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

        public ICommand TokenListItemPressed { get; set; }
        public ICommand RefreshPressed { get; set; }

        private readonly IWeb3Service _web3Service;
        private readonly INetworkService _networkService;
        private readonly INavigationService _navigationService;

        private async Task OnTokenListItemPressed(Token token)
        {
            await _navigationService.PushAsync<TokenInfoViewModel>(token);
        }

        private async Task OnRefreshPressed()
        {
            await UpdateWalletInfo();
        }

        private async Task UpdateWalletInfo()
        {
            Info = await _networkService.GetAsync<AddressInfo>(ApiHelpers.GetEthplorerUri($"getAddressInfo/{_web3Service.Account.Address}"));
            Tokens = new ObservableCollection<Token>(Info.tokens);
        }
    }
}