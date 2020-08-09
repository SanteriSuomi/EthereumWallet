using EthereumWallet.Common.Dialogs;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
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
        public WalletSendViewModel(IWeb3Service web3Service, IDialogService dialogService)
        {
            _web3Service = web3Service;
            _dialogService = dialogService;
            SendTransactionPressed = new Command(() => OnSendTransactionPressed().SafeFireAndForget());
        }

        public ICommand SendTransactionPressed { get; set; }

        public string AddressEditorText { get; set; }
        public string AmountEditorText { get; set; }

        private readonly IWeb3Service _web3Service;
        private readonly IDialogService _dialogService;

        private async Task OnSendTransactionPressed()
        {
            if (AddressEditorText.IsValidAddress())
            {
                var address = AddressEditorText;
                var amountWithCommasReplaced = AmountEditorText.Replace(',', '.');
                if (decimal.TryParse(amountWithCommasReplaced, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
                {
                    var transactionResult = await AttemptToSendTransaction(amount, address);
                }
            }
        }

        private async Task<(bool sent, TransactionReceipt receipt)> AttemptToSendTransaction(decimal decimalAmount, string toAddress)
        {
            var publicAddress = _web3Service.Account.Address;
            var privateKey = _web3Service.Account.PrivateKey;
            //TODO: fix nonce (transaction count) since GetTransactionCount seems not to work (use transaction count from info view/ethplorer api?)
            var transactionCount = await _web3Service.Client.Eth.Transactions.GetTransactionCount.SendRequestAsync(publicAddress);

            var amount = Web3.Convert.ToWei(decimalAmount);
            var nonce = new BigInteger(transactionCount);

            var (verified, encoded) = TrySignAndVerifyTransaction(toAddress, privateKey, amount, nonce);
            if (verified)
            {
                bool confirmation = await DisplayConfirmation(toAddress, amount);
                if (confirmation)
                {
                    var receipt = await SendTransaction(encoded);
                    return (true, receipt);
                }

                return (false, null);
            }
            
            return (false, null);
        }

        private static (bool verified, string encoded) TrySignAndVerifyTransaction(string toAddress, string privateKey, BigInteger amount, BigInteger nonce)
        {
            var signer = new TransactionSigner();
            var encodedTransaction = signer.SignTransaction(privateKey, toAddress, amount, nonce);
            return (signer.VerifyTransaction(encodedTransaction), encodedTransaction);
        }

        private async Task<bool> DisplayConfirmation(string toAddress, BigInteger amount)
        {
            var message = $"Are you sure you want to send {amount} ether to {toAddress}?";
            var confirmation = await _dialogService.DisplayAlert("Transaction Confirmation", message, "Yes", "No");
            return confirmation;
        }

        private async Task<TransactionReceipt> SendTransaction(string encoded)
        {
            var transactionData = $"0x{encoded}";
            var transactionId = await _web3Service.Client.Eth.Transactions.SendRawTransaction.SendRequestAsync(transactionData);
            return await _web3Service.Client.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionId);
        }
    }
}