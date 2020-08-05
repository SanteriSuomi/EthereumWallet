using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking.HTTP;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Common.Settings;
using EthereumWallet.Modules.Wallet.Info;
using Moq;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;

namespace EthereumWallet.Tests
{
    public class WalletInfoViewModelTests
    {
        public WalletInfoViewModelTests()
        {
            MockForms.Init();
            Application.Current = new App(isTest: true);
        }

        private const string privateKey = "7355167d89a10238a9d31f5640e09e993de122e1775cdd5c3460ede9ee33b2e7";

        [Fact]
        public async Task InitializeAsync_working_correctly()
        {
            App.Settings.Endpoint = Endpoint.Mainnet;
            var web3Service = new Web3Service();
            await web3Service.TrySetAccountPrivateKey(privateKey);
            var model = new WalletInfoViewModel(web3Service, new NetworkService(), new Mock<INavigationService>().Object);

            await model.InitializeAsync(null);

            Assert.True(model.Tokens.Count == 0);
        }

        [Fact]
        public async Task TokenListItemPressed_executing_correctly()
        {
            var navigationPage = new NavigationPage();
            Application.Current.MainPage = navigationPage;
            var lazy = new Lazy<INavigation>(navigationPage.Navigation);
            var model = new WalletInfoViewModel(new Mock<IWeb3Service>().Object, new Mock<INetworkService>().Object, new NavigationService(lazy));

            var oldCount = navigationPage.Navigation.NavigationStack.Count;
            model.TokenListItemPressed.Execute(null);
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            var newCount = navigationPage.Navigation.NavigationStack.Count;

            Assert.True(newCount == oldCount + 1);
        }

        [Fact]
        public async Task RefreshPressed_executing_correctly()
        {
            App.Settings.Endpoint = Endpoint.Mainnet;
            var web3Service = new Web3Service();
            await web3Service.TrySetAccountPrivateKey(privateKey);
            var model = new WalletInfoViewModel(web3Service, new NetworkService(), new Mock<INavigationService>().Object);

            model.RefreshPressed.Execute(null);
            await Task.Delay(TimeSpan.FromSeconds(0.5));

            Assert.True(model.Tokens.Count == 0);
        }
    }
}