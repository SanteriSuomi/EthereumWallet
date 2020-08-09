using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Dialogs;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Common.Settings;
using EthereumWallet.Modules.Login;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;

namespace EthereumWallet.Tests
{
    public class LoginViewModelTests
    {
        private readonly Web3Service _web3Service;
        private LoginViewModel _viewModel;

        private const string privateKey = "7355167d89a10238a9d31f5640e09e993de122e1775cdd5c3460ede9ee33b2e7";

        private const string keystorePassword = "6Sm*39vr44mvHho";
        private const string keystore = @"eyJ2ZXJzaW9uIjozLCJpZCI6ImZjY2U3MTUxLWQ4NTQtNDViYi05ZjgwLWYyNWZiYTM3MDQxYiIs
                                          ImFkZHJlc3MiOiIzNjZmMmFkYWZiZDFmMWNmNTNjZGY4ZDUzNzgzMjk4YWJiZjJiNzM5IiwiY3J5
                                          cHRvIjp7ImNpcGhlcnRleHQiOiIyNmY5M2ZkYTk4ZmIzNWNlMTNmMjNjMmIxNzNjZWE1NTc2YjYx
                                          ZDAyZjQ4YjZjOTA2NjMyZDc0ZTI5MTJhNjZhIiwiY2lwaGVycGFyYW1zIjp7Iml2IjoiYzc0OGNm
                                          YjhjYTI2NjMxN2ZjNGQ2MTY3MTNlOTVkNDAifSwiY2lwaGVyIjoiYWVzLTEyOC1jdHIiLCJrZGYi
                                          OiJzY3J5cHQiLCJrZGZwYXJhbXMiOnsiZGtsZW4iOjMyLCJzYWx0IjoiOTgwMjQwNTIwYjY3OGIy
                                          MDcyMjRmMGMyZTJkZGEwMzAwOWZkZDY1OGIwMzNhMTNiNWVmNzJkZTg1YjBjMmU0MyIsIm4iOjEz
                                          MTA3MiwiciI6OCwicCI6MX0sIm1hYyI6IjMzYTMyOWY3OWVhNThiYWY3MzExNTdkMjA5NDc1YTRh
                                          ZTczODI1YTgxOTI1ZmRkM2ExMTAwNTFlYTMzZTAzYjcifX0=";

        public LoginViewModelTests()
        {
            _web3Service = new Web3Service();
        }

        [Fact]
        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task PrivateKeyReturnCommand_is_executed_correctly()
        {
            _viewModel = new LoginViewModel
            {
                PrivateKeyReturnCommand = new Command<string>(async (s) =>
                {
                    var result = await TrySetPrivateKey(s);
                    Assert.True(result);
                })
            };

            _viewModel.PrivateKeyReturnCommand.Execute(privateKey);
        }

        [Fact]
        public void PrivateKeyTextChangedCommand_is_executed_correctly()
        {
            _viewModel = new LoginViewModel
            {
                PrivateKeyTextChangedCommand = new Command<string>(async (s) =>
                {
                    var result = await TrySetPrivateKey(s);
                    Assert.True(result);
                })
            };

            _viewModel.PrivateKeyTextChangedCommand.Execute(privateKey);
        }

        [Fact]
        public void KeystoreCommand_is_executed_correctly()
        {
            _viewModel = new LoginViewModel(new DialogService())
            {
                KeystoreCommand = new Command<string>(async (s) =>
                {
                    var result = await OnKeystoreClicked();
                    Assert.True(result);
                })
            };

            _viewModel.KeystoreCommand.Execute(privateKey);
        }

        [Fact]
        public void ChooseEndpointPressed_is_executed_correctly()
        {
            MockForms.Init();
            Application.Current = new App(isTest: true);
            _viewModel = new LoginViewModel(new DialogService())
            {
                ChooseEndpointPressed = new Command(async () =>
                {
                    var result = await OnChooseEndpointPressed();
                    Assert.True(result);
                })
            };

            _viewModel.ChooseEndpointPressed.Execute(null);
        }

        private async Task<bool> TrySetPrivateKey(string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                return false;
            }

            if (privateKey.Length < 64)
            {
                return false;
            }
            else if (privateKey.Length == 64)
            {
                var result = await _web3Service.TrySetAccountPrivateKey(privateKey);
                return result;
            }

            return false;
        }

        private async Task<bool> OnKeystoreClicked()
        {
            var fileData = Convert.FromBase64String(keystore);
            if (fileData != null)
            {
                var fileContents = await Task.Run(() => Encoding.UTF8.GetString(fileData));
                var result = await _web3Service.TrySetAccountKeystore(fileContents, keystorePassword);
                return result;
            }

            return false;
        }

        private async Task<bool> OnChooseEndpointPressed()
        {
            var input = "Mainnet";
            switch (input)
            {
                case "Mainnet":
                    App.Settings.Endpoint = Endpoint.Mainnet;
                    break;
            }

            return App.Settings.Endpoint == Endpoint.Mainnet;
        }
    }
}