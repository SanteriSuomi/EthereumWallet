using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Wallet.Send
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletSendView : ContentPage
    {
        public WalletSendView(WalletSendViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}