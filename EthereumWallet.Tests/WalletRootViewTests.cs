using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.WalletRoot;
using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Mocks;
using Xunit;

namespace EthereumWallet.Tests
{
    public class WalletRootViewTests
    {
        private const string walletRootViewXamlBase64 = @"PFRhYmJlZFBhZ2UgeG1sbnM9Imh0dHA6Ly94YW1hcmluLmNvbS9zY2hlbWFzLzIwMTQvZm9ybXM
                                                          iDQogICAgICAgICAgICAgeG1sbnM6eD0iaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93aW5meC8yMDA5L3hhb
                                                          WwiDQogICAgICAgICAgICAgeG1sbnM6ZD0iaHR0cDovL3hhbWFyaW4uY29tL3NjaGVtYXMvMjAxNC9mb3Jtcy9kZXNpZ24iDQo
                                                          gICAgICAgICAgICAgeG1sbnM6bWM9Imh0dHA6Ly9zY2hlbWFzLm9wZW54bWxmb3JtYXRzLm9yZy9tYXJrdXAtY29tcGF0aWJpbG
                                                          l0eS8yMDA2Ig0KICAgICAgICAgICAgIHhtbG5zOmJlaGF2aW91cnM9ImNsci1uYW1lc3BhY2U6RXRoZXJldW1XYWxsZXQuQ29tbW9u
                                                          LkJlaGF2aW91cnMiDQogICAgICAgICAgICAgbWM6SWdub3JhYmxlPSJkIg0KICAgICAgICAgICAgIHg6Q2xhc3M9IkV0aGVyZXVtV2
                                                          FsbGV0Lk1vZHVsZXMuV2FsbGV0Um9vdC5XYWxsZXRSb290VmlldyINCiAgICAgICAgICAgICBUaXRsZT0ie0JpbmRpbmcgUm9vdE5hd
                                                          mlnYXRpb25CYXJUaXRsZX0iPg0KICAgIA0KICAgIDxUYWJiZWRQYWdlLkJlaGF2aW9ycz4NCiAgICAgICAgPGJlaGF2aW91cnM6VGFiY
                                                          mVkUGFnZUFkZENoaWxkcmVuQmVoYXZpb3VyPg0KICAgICAgICA8L2JlaGF2aW91cnM6VGFiYmVkUGFnZUFkZENoaWxkcmVuQmVoYXZpb3
                                                          VyPg0KICAgIDwvVGFiYmVkUGFnZS5CZWhhdmlvcnM+DQo8L1RhYmJlZFBhZ2U+";

        [Fact]
        public void XamlConstructor_initializes_pages_correctly()
        {
            MockForms.Init();
            Application.Current = new App(isTest: true);

            var xamlByte = Convert.FromBase64String(walletRootViewXamlBase64);
            var xamlString = Encoding.UTF8.GetString(xamlByte);

            var view = new WalletRootView(new WalletRootViewModel(new Web3Service()));
            view.LoadFromXaml(xamlString);

            Assert.True(view.Children.Count >= 2);
        }
    }
}