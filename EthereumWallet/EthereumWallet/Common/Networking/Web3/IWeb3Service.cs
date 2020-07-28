using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace EthereumWallet.Common.Networking.WebThree
{
    public interface IWeb3Service
    {
        Web3 Client { get; }
        Account Account { get; }
        bool TrySetAccountPrivateKey(string privateKey);
        bool TrySetAccountKeystore(string content);
    }
}