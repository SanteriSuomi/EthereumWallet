using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Wallet.Info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletInfoView : ContentPage
    {
        public WalletInfoView(WalletInfoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}