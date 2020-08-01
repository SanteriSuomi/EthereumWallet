using EthereumWallet.Common.Data;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Modules.Wallet.Info.TokenInfo;
using System.Threading.Tasks;
using Xunit;

namespace EthereumWallet.Tests
{
    public class TokenInfoViewModelTests
    {
        private const string tokenAddress = "0x0cf0ee63788a0849fe5297f3407f701e122cc023";
        [Fact]
        public async Task InitializeAsync_initializes_view_correctly()
        {
            var model = new TokenInfoViewModel(new NetworkService());

            await model.InitializeAsync(new Token()
            {
                tokenInfo = new TokenInfo()
                {
                    address = tokenAddress
                }
            });

            Assert.Equal("usd", model.TokenPrice.currency.ToLowerInvariant());
        }
    }
}