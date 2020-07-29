﻿using EthereumWallet.Common.Dialogs;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using EthereumWallet.Modules.Wallet;
using Plugin.FilePicker;
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
        }

        public ICommand PrivateKeyReturnCommand { get; set; }
        public ICommand PrivateKeyTextChangedCommand { get; set; }

        public ICommand KeystoreCommand { get; set; }

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
                    await _navigationService.PushAsync<LoginViewModel>(null, true);
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

                    await _navigationService.PushAsync<WalletViewModel>(null, true);
                }
            }

            LoadingIndicator = false;
            ContentPageTouchEnabled = true;
        }
    }
}