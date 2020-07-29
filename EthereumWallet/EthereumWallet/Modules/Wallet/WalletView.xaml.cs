using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Wallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletView : ContentPage
    {
        public WalletView(WalletViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}