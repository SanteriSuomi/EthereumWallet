using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using Serilog;
using System;
using System.Linq;

namespace EthereumWallet.Modules.WalletRoot
{
    public class WalletRootViewModel : BaseViewModel
    {
        private const int titleAddressLength = 20;

        public WalletRootViewModel(IWeb3Service web3Service)
        {
            _web3Service = web3Service;
            try
            {
                var addressString = new string(_web3Service.Account?.Address?.Take(titleAddressLength).ToArray()) + "...";
                RootNavigationBarTitle = $"Wallet: {addressString}";
            }
            catch (NullReferenceException e) when (_web3Service.Account is null)
            {
                Log.Warning(e, "Account was null.");
            }
            catch (NullReferenceException e) when (_web3Service.Account.Address is null)
            {
                Log.Warning(e, "Account.Address was null.");
            }

        }

        public string RootNavigationBarTitle { get; set; }

        private readonly IWeb3Service _web3Service;
    }
}