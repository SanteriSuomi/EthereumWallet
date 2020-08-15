using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Wallet.Send.Transaction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionCompleteView : PopupPage
    {
        public TransactionCompleteView(TransactionCompleteViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}