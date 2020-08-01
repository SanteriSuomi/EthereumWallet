using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Wallet.Info.TokenInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenInfoView : ContentPage
    {
        public TokenInfoView(TokenInfoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}