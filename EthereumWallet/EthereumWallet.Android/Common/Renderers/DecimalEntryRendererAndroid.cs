using Android.Content;
using EthereumWallet.Common.Renderers;
using EthereumWallet.Droid.Common.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DecimalEditor), typeof(DecimalEntryRendererAndroid))]
namespace EthereumWallet.Droid.Common.Renderers
{
    public class DecimalEntryRendererAndroid : EditorRenderer
    {
        public DecimalEntryRendererAndroid(Context context) : base(context) { }
    }
}