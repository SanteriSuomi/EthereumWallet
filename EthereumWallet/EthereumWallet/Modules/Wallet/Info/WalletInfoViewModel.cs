using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Data;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using EthereumWallet.Modules.Wallet.Info.TokenInfo;
using Serilog;
using System;
using System.Collections.Generic;
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
        }

        public override async Task InitializeAsync(object parameter)
        {
            await UpdateWalletInfo();
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

        private bool _tokensListEnabled;
        public bool TokenNoDataLabelEnabled
        {
            get => _tokensListEnabled;
            set
            {
                _tokensListEnabled = value;
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
            try
            {
                Info = await _networkService.GetAsync<AddressInfo>(ApiHelpers.GetEthplorerUri(App.Settings.Endpoint, $"getAddressInfo/{_web3Service.Account.Address}"));
                var tokensExist = Info.tokens != null;
                TokenNoDataLabelEnabled = !tokensExist;
                Tokens = new ObservableCollection<Token>(Info.tokens ?? new List<Token>());

                OnPropertyChanged(nameof(Info));
                OnPropertyChanged(nameof(Tokens));
                OnPropertyChanged(nameof(TokenNoDataLabelEnabled));
            }
            catch (NullReferenceException e) when (App.Settings is null)
            {
                Log.Error(e, "App.Settings was null");
            }
            catch (NullReferenceException e) when (_web3Service.Account is null)
            {
                Log.Error(e, "_web3Service.Account was null");
            }
            catch (NotSupportedException e) when (Info.tokens is null)
            {
                Log.Warning(e, "Info.tokens was null");
            }
        }
    }
}