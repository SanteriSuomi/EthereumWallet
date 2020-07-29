using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.WalletRoot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletRootView : TabbedPage
    {
        public WalletRootView(WalletRootViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}