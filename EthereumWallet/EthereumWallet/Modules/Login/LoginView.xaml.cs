using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, "OnAppearing");
        }
    }
}