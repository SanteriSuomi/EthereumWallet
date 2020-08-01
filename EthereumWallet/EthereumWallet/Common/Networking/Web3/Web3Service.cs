using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Data;
using EthereumWallet.Common.Settings;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Networking.WebThree
{
    public class Web3Service : IWeb3Service
    {
        public Web3 Client { get; set; }
        public Account Account { get; private set; }

        public void UpdateClient()
        {
            switch (App.Settings.Endpoint)
            {
                case Endpoint.Mainnet:
                    Client = new Web3(ApiConstants.EthplorerMainnetApiUrl);
                    break;
                case Endpoint.Kovan:
                    Client = new Web3(ApiConstants.EthplorerKovanApiUrl);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Attempt to set account using private key.
        /// </summary>
        /// <returns>Whether the creation was succesful or not.</returns>
        public async Task<bool> TrySetAccountPrivateKey(string privateKey)
        {
            return await Task.Run(() =>
            {
                var newAccount = new Account(privateKey);
                if (HasValidAddress(newAccount))
                {
                    Account = newAccount;
                    return true;
                }

                return false;
            });
        }

        /// <summary>
        /// Attempt to set account using keystore file and password.
        /// </summary>
        /// <returns>Whether the creation was succesful or not.</returns>
        public async Task<bool> TrySetAccountKeystore(string json, string password)
        {
            return await Task.Run(() =>
            {
                if (json != null)
                {
                    var keystore = JsonConvert.DeserializeObject<KeyStoreRoot>(json);
                    if (keystore != null)
                    {
                        var newAccount = Account.LoadFromKeyStore(json, password);
                        if (HasValidAddress(newAccount))
                        {
                            Account = newAccount;
                            return true;
                        }
                    }
                }

                return false;
            });
        }

        private static bool HasValidAddress(Account account)
        {
            return AddressUtil.Current.IsValidAddressLength(account?.Address)
                    && AddressUtil.Current.IsChecksumAddress(account?.Address);
        }
    }
}