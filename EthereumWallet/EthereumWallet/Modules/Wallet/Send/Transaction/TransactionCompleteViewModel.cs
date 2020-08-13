using EthereumWallet.Modules.Base;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;

namespace EthereumWallet.Modules.Wallet.Send.Transaction
{
    public class TransactionCompleteViewModel : BaseViewModel
    {
        public override Task InitializeAsync(object parameter)
        {
            Receipt = parameter as TransactionReceipt;
            OnPropertyChanged(nameof(Receipt));
            return base.InitializeAsync(parameter);
        }

        public TransactionReceipt Receipt { get; set; }
        public bool ReceiptHasErrors => string.IsNullOrEmpty(Receipt.TransactionHash);
    }
}