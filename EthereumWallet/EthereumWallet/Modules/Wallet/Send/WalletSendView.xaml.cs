using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Wallet.Send
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletSendView : ContentPage
    {
        private const double sendingTransactionIndicatorHeight = 85;

        public WalletSendView(WalletSendViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            MessagingCenter.Subscribe<WalletSendViewModel, bool>(this, "OnSendingTransactionChanged", (s, v) =>
            {
                OnSendingTransactionChanged(v);
            });
        }

        private void OnSendingTransactionChanged(bool value)
        {
            if (value)
            {
                sendingTransactionIndicator.HeightRequest = sendingTransactionIndicatorHeight;
            }
            else
            {
                sendingTransactionIndicator.HeightRequest = 0;
            }
        }

        ~WalletSendView()
        {
            MessagingCenter.Unsubscribe<WalletSendViewModel, bool>(this, "OnSendingTransactionChanged");
        }
    }
}