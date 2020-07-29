using EthereumWallet.Common.Data;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Networking.WebThree
{
    public class Web3Service : IWeb3Service
    {
        public Web3 Client { get; }
        public Account Account { get; private set; }

        public Web3Service()
        {
            Client = new Web3(ApiConstants.InfuraMainnetApiUrl);
        }

        /// <summary>
        /// Attempt to set account using private key.
        /// </summary>
        /// <param name="privateKey"></param>
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

        public async Task<bool> TrySetAccountKeystore(string json, string password)
        {
            return await Task.Run(() =>
            {
                if (json != null)
                {
                    var keystore = JsonConvert.DeserializeObject<KeyStoreRoot>(json);
                    if (keystore != null)
                    {
                        var newAccount = Account.LoadFromKeyStore(json, /*password*/"6Sm*39vr44mvHho");
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

        private static bool HasValidAddress(Account newAccount)
        {
            return AddressUtil.Current.IsValidAddressLength(newAccount?.Address)
                    && AddressUtil.Current.IsChecksumAddress(newAccount?.Address);
        }
    }
}