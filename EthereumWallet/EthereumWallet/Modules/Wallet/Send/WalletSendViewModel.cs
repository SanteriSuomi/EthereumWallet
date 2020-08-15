using EthereumWallet.Common.Dialogs;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using EthereumWallet.Modules.Wallet.Send.Transaction;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using System;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EthereumWallet.Modules.Wallet.Send
{
    public class WalletSendViewModel : BaseViewModel
    {
        public WalletSendViewModel(IWeb3Service web3Service, IDialogService dialogService, INavigationService navigationService)
        {
            _web3Service = web3Service;
            _dialogService = dialogService;
            _navigationService = navigationService;
            SendTransactionPressed = new Command(async () =>
            {
                await OnSendTransactionPressed().ConfigureAwait(true);
            });
        }

        private const int minimumTransactionGasPriceInGwei = 1;
        private const int minimumTransactionGasLimitInWei = 21000;
        private const float receiptRequestDelay = 0.5f;

        public ICommand SendTransactionPressed { get; set; }

        public string AddressEditorText { get; set; }
        public string AmountEditorText { get; set; }
        public string GasPriceEditorText { get; set; }
        public string GasLimitEditorText { get; set; }
        public string DataEditorText { get; set; }

        private bool _isSendingTransaction;
        public bool IsSendingTransaction
        {
            get => _isSendingTransaction;
            set
            {
                _isSendingTransaction = value;
                MessagingCenter.Send(this, "OnSendingTransactionChanged", value);
                OnPropertyChanged();
            }
        }

        private readonly IWeb3Service _web3Service;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private async Task OnSendTransactionPressed()
        {
            if (AddressEditorText.IsValidAddress() && !IsSendingTransaction)
            {
                IsSendingTransaction = true;
                var address = AddressEditorText;
                var amountWithCommasReplaced = AmountEditorText.Replace(',', '.');
                if (decimal.TryParse(amountWithCommasReplaced, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
                {
                    var (sent, receipt) = await AttemptToSendTransaction(amount, amountWithCommasReplaced, address);
                    if (sent)
                    {
                        await _navigationService.PushAsync<TransactionCompleteViewModel>(receipt);
                        IsSendingTransaction = false;
                        return;
                    }
                }
            }

            await SendingFailedEvent();
        }

        private async Task SendingFailedEvent()
        {
            IsSendingTransaction = false;
            await _navigationService.PushAsync<TransactionCompleteViewModel>(new TransactionReceipt());
        }

        private async Task<(bool sent, TransactionReceipt receipt)> AttemptToSendTransaction(decimal decimalAmount, string amountWithCommasReplaced, string receivingAddress)
        {
            var localAddress = _web3Service.Account.Address;
            var privateKey = _web3Service.Account.PrivateKey;

            var transactionCount = await _web3Service.Client.Eth.Transactions.GetTransactionCount.SendRequestAsync(localAddress);
            HexBigInteger gasPrice = await GetGasPrice(localAddress);
            HexBigInteger gasLimit = await GetGasLimit();
            var amount = Web3.Convert.ToWei(decimalAmount, UnitConversion.EthUnit.Ether);

            (bool verified, string encoded) data;
            if (gasPrice != null && gasLimit != null && !string.IsNullOrEmpty(DataEditorText))
            {
                data = TrySignAndVerifyTransaction(receivingAddress, privateKey, amount, transactionCount.Value, gasPrice, gasLimit, DataEditorText);
            }
            else
            {
                data = TrySignAndVerifyTransaction(receivingAddress, privateKey, amount, transactionCount.Value);
            }

            if (data.verified)
            {
                bool confirmation = await DisplayConfirmation(receivingAddress, amountWithCommasReplaced);
                if (confirmation)
                {
                    var receipt = await SendTransactionAndGetReceipt(data.encoded);
                    return (true, receipt);
                }

                return (false, null);
            }

            return (false, null);
        }

        private async Task<HexBigInteger> GetGasPrice(string address)
        {
            HexBigInteger gasPrice;
            if (!string.IsNullOrEmpty(GasPriceEditorText)
                && int.TryParse(GasPriceEditorText, NumberStyles.Any, CultureInfo.InvariantCulture, out int price))
            {
                if (price <= minimumTransactionGasPriceInGwei)
                {
                    await _dialogService.DisplayAlert("Gas Price Error", "Gas Price shouldn't be less than 1 gwei. It might take very long to complete.", null, "Ok");
                    return null;
                }

                var weiUnit = Web3.Convert.ToWei(price, UnitConversion.EthUnit.Gwei);
                gasPrice = new HexBigInteger(weiUnit);
            }
            else
            {
                gasPrice = await _web3Service.Client.Eth.GasPrice.SendRequestAsync(address);
            }

            return gasPrice;
        }

        private async Task<HexBigInteger> GetGasLimit()
        {
            HexBigInteger GasLimit;
            if (!string.IsNullOrEmpty(GasLimitEditorText)
                && int.TryParse(GasLimitEditorText, NumberStyles.Any, CultureInfo.InvariantCulture, out int limit))
            {
                var gweiUnit = Web3.Convert.ToWei(limit, UnitConversion.EthUnit.Wei);
                if (gweiUnit <= minimumTransactionGasLimitInWei)
                {
                    await _dialogService.DisplayAlert("Gas Limit Error", "Gas Limit shouldn't be less than the value of gas price.", null, "Ok");
                    return null;
                }

                GasLimit = new HexBigInteger(gweiUnit);
            }
            else
            {
                GasLimit = new HexBigInteger(minimumTransactionGasLimitInWei);
            }

            return GasLimit;
        }

        private (bool verified, string encoded) TrySignAndVerifyTransaction(string receivingAddress, string privateKey,
                BigInteger amount, BigInteger nonce, BigInteger gasPrice, BigInteger gasLimit, string data)
        {
            var encodedTransaction = Web3.OfflineTransactionSigner.SignTransaction(privateKey, receivingAddress, amount, nonce, gasPrice, gasLimit, data);
            return (Web3.OfflineTransactionSigner.VerifyTransaction(encodedTransaction), encodedTransaction);
        }

        private (bool verified, string encoded) TrySignAndVerifyTransaction(string receivingAddress, string privateKey,
                BigInteger amount, BigInteger nonce)
        {
            var encodedTransaction = Web3.OfflineTransactionSigner.SignTransaction(privateKey, receivingAddress, amount, nonce);
            return (Web3.OfflineTransactionSigner.VerifyTransaction(encodedTransaction), encodedTransaction);
        }

        private async Task<bool> DisplayConfirmation(string toAddress, string amount)
        {
            var message = $"Are you sure you want to send {amount} ether to {toAddress}?";
            var confirmation = await _dialogService.DisplayAlert("Transaction Confirmation", message, "Yes", "No");
            return confirmation;
        }

        private async Task<TransactionReceipt> SendTransactionAndGetReceipt(string encoded)
        {
            var transactionData = $"0x{encoded}";
            var transactionId = await _web3Service.Client.Eth.Transactions.SendRawTransaction.SendRequestAsync(transactionData);
            var receipt = await _web3Service.Client.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionId);
            while (receipt is null)
            {
                await Task.Delay(TimeSpan.FromSeconds(receiptRequestDelay));
                receipt = await _web3Service.Client.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionId);
            }

            IsSendingTransaction = false;
            return receipt;
        }
    }
}