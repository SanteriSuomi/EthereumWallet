using EthereumWallet.Common.Data;
using EthereumWallet.Common.Networking;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Modules.Base;
using System.Threading.Tasks;

namespace EthereumWallet.Modules.Wallet.Info.TokenInfo
{
    public class TokenInfoViewModel : BaseViewModel
    {
        public TokenInfoViewModel(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public override async Task InitializeAsync(object parameter)
        {
            Token = parameter as Token;
            var info = await _networkService.GetAsync<TokenInfoWithPrice>(ApiHelpers.GetEthplorerUri($"getTokenInfo/{Token.tokenInfo.address}"));
            TokenPrice = info.price;
            Title = $"Token: {Token.tokenInfo.name}";
        }

        private Token _token;
        public Token Token
        {
            get => _token;
            set
            {
                _token = value;
                OnPropertyChanged();
            }
        }

        private TokenInfoPrice _price;
        public TokenInfoPrice TokenPrice
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private readonly INetworkService _networkService;
    }
}