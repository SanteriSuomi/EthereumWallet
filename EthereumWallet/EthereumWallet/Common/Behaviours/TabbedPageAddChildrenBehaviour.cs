using Autofac;
using EthereumWallet.ApplicationBase;
using EthereumWallet.Modules.Wallet.Info;
using EthereumWallet.Modules.Wallet.Send;
using Xamarin.Forms;

namespace EthereumWallet.Common.Behaviours
{
    public class TabbedPageAddChildrenBehaviour : Behavior<TabbedPage>
    {
        protected override void OnAttachedTo(TabbedPage bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Children.Add(App.Container.Resolve<WalletInfoView>());
            bindable.Children.Add(App.Container.Resolve<WalletSendView>());
        }
    }
}