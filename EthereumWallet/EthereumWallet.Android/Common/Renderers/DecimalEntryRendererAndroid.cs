using Android.Content;
//using Android.Text;
//using Android.Text.Method;
using EthereumWallet.Common.Renderers;
using EthereumWallet.Droid.Common.Renderers;
//using Java.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DecimalEditor), typeof(DecimalEntryRendererAndroid))]
namespace EthereumWallet.Droid.Common.Renderers
{
    public class DecimalEntryRendererAndroid : EditorRenderer
    {
        public DecimalEntryRendererAndroid(Context context) : base(context) { }

        //protected override NumberKeyListener GetDigitsKeyListener(InputTypes inputTypes)
        //{
        //    return DigitsKeyListener.GetInstance(Locale.Default, false, true);
        //}
    }
}