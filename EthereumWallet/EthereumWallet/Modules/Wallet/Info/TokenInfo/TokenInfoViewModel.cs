using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Data;
using EthereumWallet.Common.Networking;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Modules.Base;
using Serilog;
using System;
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
            try
            {
                Token = parameter as Token;
                Title = $"Token: {Token.tokenInfo.name}";
                HasAlert = !string.IsNullOrEmpty(Token.tokenInfo.alert);
                TokenPrice = new TokenInfoPrice();

                var infoWithPrice = await _networkService.GetAsync<TokenInfoWithPrice>(ApiHelpers.GetEthplorerUri(App.Settings.Endpoint, $"getTokenInfo/{Token.tokenInfo.address}"));
                TokenPriceDataEnabled = infoWithPrice != null;
                TokenPrice = infoWithPrice.price;
            }
            catch (NullReferenceException e) when (Token is null)
            {
                Log.Warning(e, "Parameter is null or not the type of Token. (Token variable is null)");
            }
            catch (NullReferenceException e) when (App.Settings is null)
            {
                Log.Warning(e, "App.Settings was null.");
            }
            catch (NullReferenceException e) when (Token.tokenInfo is null)
            {
                Log.Warning(e, "Token.tokenInfo was null.");
            }
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

        private TokenInfoPrice _tokenPrice;
        public TokenInfoPrice TokenPrice
        {
            get => _tokenPrice;
            set
            {
                _tokenPrice = value;
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

        private bool _tokenPriceDataEnabled;
        public bool TokenPriceDataEnabled
        {
            get => _tokenPriceDataEnabled;
            set
            {
                _tokenPriceDataEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _hasAlert;
        public bool HasAlert
        {
            get => _hasAlert;
            set
            {
                _hasAlert = value;
                OnPropertyChanged();
            }
        }

        private readonly INetworkService _networkService;
    }
}