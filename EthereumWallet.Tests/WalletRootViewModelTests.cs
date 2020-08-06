using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.WalletRoot;
using System.Threading.Tasks;
using Xunit;

namespace EthereumWallet.Tests
{
    public class WalletRootViewModelTests
    {
        private const string privateKey = "7355167d89a10238a9d31f5640e09e993de122e1775cdd5c3460ede9ee33b2e7";

        [Fact]
        public async Task Constructor_initializes_page_title_correctly()
        {
            var web3Service = new Web3Service();
            await web3Service.TrySetAccountPrivateKey(privateKey);
            var model = new WalletRootViewModel(web3Service);

            Assert.True(!string.IsNullOrEmpty(model.RootNavigationBarTitle) 
                        && model.RootNavigationBarTitle.Length > 0);
        }
    }
}