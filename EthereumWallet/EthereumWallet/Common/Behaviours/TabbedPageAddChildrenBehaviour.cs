using Autofac;
using EthereumWallet.ApplicationBase;
using EthereumWallet.Modules.Base;
using EthereumWallet.Modules.Wallet.Info;
using EthereumWallet.Modules.Wallet.Send;
using Serilog;
using System;
using Xamarin.Forms;

namespace EthereumWallet.Common.Behaviours
{
    public class TabbedPageAddChildrenBehaviour : Behavior<TabbedPage>
    {
        protected override async void OnAttachedTo(TabbedPage bindable)
        {
            base.OnAttachedTo(bindable);

            Page view = null;
            BaseViewModel model = null;
            try
            {
                view = App.Container.Resolve<WalletInfoView>();
                model = view.BindingContext as BaseViewModel;
                bindable.Children.Add(view);
                await model.InitializeAsync(null);

                view = App.Container.Resolve<WalletSendView>();
                model = view.BindingContext as BaseViewModel;
                bindable.Children.Add(view);
                await model.InitializeAsync(null);
            }
            catch (NullReferenceException e) when (view is null)
            {
                Log.Error(e, "view was null");
            }
            catch (NullReferenceException e) when (model is null)
            {
                Log.Error(e, "model was null");
            }
        }
    }
}