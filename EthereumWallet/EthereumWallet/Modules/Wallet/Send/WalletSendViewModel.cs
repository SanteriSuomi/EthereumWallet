﻿using EthereumWallet.Common.Dialogs;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
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
        public WalletSendViewModel(IWeb3Service web3Service, IDialogService dialogService)
        {
            _web3Service = web3Service;
            _dialogService = dialogService;
            SendTransactionPressed = new Command(() => OnSendTransactionPressed().SafeFireAndForget());
        }

        public ICommand SendTransactionPressed { get; set; }

        public string AddressEditorText { get; set; }
        public string AmountEditorText { get; set; }

        private bool _isSendingTransaction;
        public bool IsSendingTransaction
        {
            get => _isSendingTransaction;
            set
            {
                _isSendingTransaction = value;
                MessagingCenter.Send(this, "OnSendingTransactionChanged", value);
            }
        }

        private const float receiptRequestDelay = 0.5f;

        private readonly IWeb3Service _web3Service;
        private readonly IDialogService _dialogService;

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

                    }
                    else
                    {
                        IsSendingTransaction = false;
                    }
                }
                else
                {
                    IsSendingTransaction = false;
                }
            }
        }

        private async Task<(bool sent, TransactionReceipt receipt)> AttemptToSendTransaction(decimal decimalAmount, string amountWithCommasReplaced, string receivingAddress)
        {
            var localAddress = _web3Service.Account.Address;
            var privateKey = _web3Service.Account.PrivateKey;

            var transactionCount = await _web3Service.Client.Eth.Transactions.GetTransactionCount.SendRequestAsync(localAddress);
            var gasPrice = await _web3Service.Client.Eth.GasPrice.SendRequestAsync(localAddress);
            var gasLimit = gasPrice;

            var amount = Web3.Convert.ToWei(decimalAmount, UnitConversion.EthUnit.Ether);

            var (verified, encoded) = TrySignAndVerifyTransaction(receivingAddress, privateKey, amount, transactionCount.Value, gasPrice, gasLimit);
            if (verified)
            {
                bool confirmation = await DisplayConfirmation(receivingAddress, amountWithCommasReplaced);
                if (confirmation)
                {
                    var receipt = await SendTransaction(encoded);
                    return (true, receipt);
                }

                return (false, null);
            }

            return (false, null);
        }

        private (bool verified, string encoded) TrySignAndVerifyTransaction(string receivingAddress, string privateKey,
                BigInteger amount, BigInteger nonce, BigInteger gasPrice, BigInteger gasLimit)
        {
            var encodedTransaction = Web3.OfflineTransactionSigner.SignTransaction(privateKey, receivingAddress, amount, nonce/*, gasPrice, gasLimit*/);
            return (Web3.OfflineTransactionSigner.VerifyTransaction(encodedTransaction), encodedTransaction);
        }

        private async Task<bool> DisplayConfirmation(string toAddress, string amount)
        {
            var message = $"Are you sure you want to send {amount} ether to {toAddress}?";
            var confirmation = await _dialogService.DisplayAlert("Transaction Confirmation", message, "Yes", "No");
            return confirmation;
        }

        private async Task<TransactionReceipt> SendTransaction(string encoded)
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