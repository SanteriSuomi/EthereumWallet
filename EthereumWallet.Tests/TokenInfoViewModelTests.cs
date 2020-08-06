using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Data;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Modules.Wallet.Info.TokenInfo;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;

namespace EthereumWallet.Tests
{
    public class TokenInfoViewModelTests
    {
        public TokenInfoViewModelTests()
        {
            MockForms.Init();
            Application.Current = new App(isTest: true);
            _tokenInfoModel = new TokenInfoViewModel(new NetworkService());
        }

        private const string tokenAddress = "0x0cf0ee63788a0849fe5297f3407f701e122cc023";

        private readonly TokenInfoViewModel _tokenInfoModel;

        [Fact]
        public async Task InitializeAsync_initializes_token_currency_correctly()
        {
            await _tokenInfoModel.InitializeAsync(new Token()
            {
                tokenInfo = new TokenInfo()
                {
                    address = tokenAddress
                }
            });

            Assert.Equal("usd", _tokenInfoModel.TokenPrice.currency.ToLowerInvariant());
        }

        [Fact]
        public async Task InitializeAsync_initializes_tokenPriceData_correctly()
        {
            await _tokenInfoModel.InitializeAsync(new Token()
            {
                tokenInfo = new TokenInfo()
                {
                    address = tokenAddress
                }
            });

            Assert.True(_tokenInfoModel.TokenPriceDataEnabled);
        }

        [Fact]
        public async Task InitializeAsync_initializes_tokenPriceData_correctly_when_parameter_is_null()
        {
            await _tokenInfoModel.InitializeAsync(null);

            Assert.True(!_tokenInfoModel.TokenPriceDataEnabled);
        }
    }
}