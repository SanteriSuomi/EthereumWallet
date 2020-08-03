using System.Diagnostics;
using Xamarin.Forms;

namespace EthereumWallet.Common.Behaviours
{
    public class OnlyInDebugButtonBehaviour : Behavior<Button>
    {
        protected override void OnAttachedTo(Button bindable)
        {
            base.OnAttachedTo(bindable);
            if (!Debugger.IsAttached)
            {
                bindable.IsVisible = false;
            }
        }
    }
}