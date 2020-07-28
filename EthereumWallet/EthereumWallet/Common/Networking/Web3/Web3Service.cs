using EthereumWallet.Common.Data;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;

namespace EthereumWallet.Common.Networking.WebThree
{
    public class Web3Service : IWeb3Service
    {
        public Web3 Client { get; }
        public Account Account { get; private set; }

        public Web3Service()
        {
            Client = new Web3(ApiConstants.InfuraRopstenApiUrl);
        }

        /// <summary>
        /// Attempt to set account using private key.
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns>Whether the creation was succesful or not.</returns>
        public bool TrySetAccountPrivateKey(string privateKey)
        {
            if (privateKey.Length == 64)
            {
                Account = new Account(privateKey);
                return true;
            }

            return false;
        }

        public bool TrySetAccountKeystore(string content)
        {
            if (content != null)
            {
                var keystore = JsonConvert.DeserializeObject<KeyStoreRoot>(content);
                if (keystore != null)
                {
                    Account = Account.LoadFromKeyStore(content, "password");
                    return true;
                }
            }

            return false;
        }
    }
}