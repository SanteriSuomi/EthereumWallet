using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Dialogs;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Common.Settings;
using EthereumWallet.Modules.Base;
using EthereumWallet.Modules.Dev.DevLog;
using EthereumWallet.Modules.WalletRoot;
using Plugin.FilePicker;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EthereumWallet.Modules.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(IWeb3Service web3Service, INavigationService navigationService, IDialogService dialogService)
        {
            _web3Service = web3Service;
            _navigationService = navigationService;
            _dialogService = dialogService;
            PrivateKeyReturnCommand = new Command<string>((s) => OnPrivateKeyReturn(s).SafeFireAndForget());
            PrivateKeyTextChangedCommand = new Command<TextChangedEventArgs>((args) => OnPrivateKeyTextChanged(args).SafeFireAndForget());
            KeystoreCommand = new Command(() => OnKeystoreClicked().SafeFireAndForget());
            ChooseEndpointPressed = new Command(() => OnChooseEndpointPressed().SafeFireAndForget());
            DevLogButtonPressed = new Command(() => OnDevLogButtonPressed().SafeFireAndForget());
            MessagingCenter.Subscribe<LoginView>(this, "OnAppearing", (l) => OnViewAppearing().SafeFireAndForget());
            App.SettingsChangedEvent += OnSettingsChanged;
        }

        public ICommand PrivateKeyReturnCommand { get; set; }
        public ICommand PrivateKeyTextChangedCommand { get; set; }
        public ICommand KeystoreCommand { get; set; }
        public ICommand ChooseEndpointPressed { get; set; }
        public ICommand DevLogButtonPressed { get; set; }

        private bool _loadingIndicator;
        public bool LoadingIndicator
        {
            get => _loadingIndicator;
            set
            {
                _loadingIndicator = value;
                OnPropertyChanged();
            }
        }

        private bool _contentPageTouchEnabled = true;
        public bool ContentPageTouchEnabled
        {
            get => _contentPageTouchEnabled;
            set
            {
                _contentPageTouchEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _privateKeyInfoLabelEnabled;
        public bool PrivateKeyInfoLabelEnabled
        {
            get => _privateKeyInfoLabelEnabled;
            set
            {
                _privateKeyInfoLabelEnabled = value;
                OnPropertyChanged();
            }
        }

        private string _privateKeyInfoLabelText;
        public string PrivateKeyInfoLabelText
        {
            get => _privateKeyInfoLabelText;
            set
            {
                _privateKeyInfoLabelText = value;
                OnPropertyChanged();
            }
        }
        
        private string _privateKeyText;
        public string PrivateKeyText
        {
            get => _privateKeyText;
            set
            {
                _privateKeyText = value;
                OnPropertyChanged();
            }
        }

        private string _endpointText;
        public string EndpointText
        {
            get => _endpointText;
            set
            {
                _endpointText = value;
                OnPropertyChanged();
            }
        }

        private readonly IWeb3Service _web3Service;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private async Task OnPrivateKeyReturn(string text)
        {
            await TrySetPrivateKey(text);
        }

        private async Task OnPrivateKeyTextChanged(TextChangedEventArgs args)
        {
            await TrySetPrivateKey(args.NewTextValue);
        }

        private async Task TrySetPrivateKey(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                PrivateKeyInfoLabelEnabled = false;
                return;
            }

            if (text.Length < 64)
            {
                PrivateKeyInfoLabelText = "Private key is too short.";
                PrivateKeyInfoLabelEnabled = true;
            }
            else if (text.Length == 64)
            {
                var result = await _web3Service.TrySetAccountPrivateKey(text);
                if (result)
                {
                    PrivateKeyText = string.Empty;
                    await _navigationService.PushAsync<WalletRootViewModel>(null, true);
                }
            }
        }

        private async Task OnKeystoreClicked()
        {
            var fileData = await CrossFilePicker.Current.PickFile();
            if (fileData != null)
            {
                LoadingIndicator = true;
                ContentPageTouchEnabled = false;

                var fileContents = await Task.Run(() => Encoding.UTF8.GetString(fileData.DataArray));
                var userInput = await _dialogService.DisplayPrompt("Password", "Please enter keystore password.");
                var result = await _web3Service.TrySetAccountKeystore(fileContents, userInput);
                if (result)
                {
                    LoadingIndicator = false;
                    ContentPageTouchEnabled = true;

                    await _navigationService.PushAsync<WalletRootViewModel>(null, true);
                }
            }

            LoadingIndicator = false;
            ContentPageTouchEnabled = true;
        }

        private async Task OnChooseEndpointPressed()
        {
            var input = await _dialogService.DisplayActionSheet("Choose Endpoint", null, null, "Mainnet", "Kovan");
            if (string.IsNullOrEmpty(input)) return;

            switch (input)
            {
                case "Mainnet":
                    App.Settings.Endpoint = Endpoint.Mainnet;
                    break;
                case "Kovan":
                    App.Settings.Endpoint = Endpoint.Kovan;
                    break;
            }

            _web3Service.UpdateClient(App.Settings.Endpoint);
            await App.SettingsRepository.SaveAsync(App.Settings);
        }

        private async Task OnDevLogButtonPressed()
        {
            await _navigationService.PushAsync<DevLogViewModel>();
        }

        private async Task OnViewAppearing()
        {
            if (string.IsNullOrEmpty(EndpointText))
            {
                EndpointText = App.Settings.Endpoint.ToString();
            }
            
            await _navigationService.PopToRootAsync();
        }

        private void OnSettingsChanged(object sender, EventArgs _)
        {
            var settings = sender as Settings;
            EndpointText = settings.Endpoint.ToString();
            _web3Service.UpdateClient(settings.Endpoint);
        }
    }
}