using Autofac;
using EthereumWallet.ApplicationBase;
using EthereumWallet.Modules.Base;
using EthereumWallet.Modules.Wallet.Info;
using EthereumWallet.Modules.Wallet.Send;
using Xamarin.Forms;

namespace EthereumWallet.Common.Behaviours
{
    public class TabbedPageAddChildrenBehaviour : Behavior<TabbedPage>
    {
        protected override async void OnAttachedTo(TabbedPage bindable)
        {
            base.OnAttachedTo(bindable);
            var infoView = App.Container.Resolve<WalletInfoView>();
            var baseViewModel = infoView.BindingContext as BaseViewModel;
            bindable.Children.Add(infoView);
            await baseViewModel.InitializeAsync(null);

            var sendView = App.Container.Resolve<WalletSendView>();
            baseViewModel = infoView.BindingContext as BaseViewModel;
            bindable.Children.Add(sendView);
            await baseViewModel.InitializeAsync(null);
        }
    }
}