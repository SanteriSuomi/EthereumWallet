using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthereumWallet.Modules.Dev.DevLog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevLogView : ContentPage
    {
        public DevLogView(DevLogViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}