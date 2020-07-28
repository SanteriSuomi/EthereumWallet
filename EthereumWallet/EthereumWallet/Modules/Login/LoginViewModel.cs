using EthereumWallet.Common.Extensions;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Base;
using Plugin.FilePicker;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EthereumWallet.Modules.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IWeb3Service _web3Service;

        public LoginViewModel(IWeb3Service web3Service)
        {
            _web3Service = web3Service;
            PrivateKeyCommand = new Command<string>((s) => OnPrivateKeyReturn(s).SafeFireAndForget());
            KeystoreCommand = new Command(() => OnKeystoreClicked().SafeFireAndForget());
        }

        public ICommand PrivateKeyCommand { get; set; }
        public ICommand KeystoreCommand { get; set; }

        private async Task OnPrivateKeyReturn(string text)
        {
            if (_web3Service.TrySetAccountKeystore(text))
            {

            }
        }

        private async Task OnKeystoreClicked()
        {
            var fileData = await CrossFilePicker.Current.PickFile();
            if (fileData != null)
            {
                var contents = Encoding.UTF8.GetString(fileData.DataArray);
                if (_web3Service.TrySetAccountKeystore(contents))
                {

                }
            }
        }
    }
}