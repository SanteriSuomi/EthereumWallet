using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Extensions;
using EthereumWallet.Modules.Base;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EthereumWallet.Modules.Dev.DevLog
{
    public class DevLogViewModel : BaseViewModel
    {
        public DevLogViewModel()
        {
            RefreshLogPressed = new Command(() => GetAndSetLogText().SafeFireAndForget(true));
            ClearLogPressed = new Command(() => ClearLogText().SafeFireAndForget(true));
            GetAndSetLogText().SafeFireAndForget(true);
        }

        public ICommand RefreshLogPressed { get; set; }
        public ICommand ClearLogPressed { get; set; }

        private string _logText;
        public string LogText
        {
            get => _logText;
            set
            {
                _logText = value;
                OnPropertyChanged();
            }
        }

        private async Task ClearLogText()
        {
            await Task.Run(() =>
            {
                File.Create(App.LogFilePath);
            });

            await GetAndSetLogText();
        }

        private async Task GetAndSetLogText()
        {
            await Task.Run(() =>
            {
                if (File.Exists(App.LogFilePath))
                {
                    LogText = File.ReadAllText(App.LogFilePath);
                }
            });

            if (string.IsNullOrEmpty(LogText))
            {
                LogText = "Log is empty";
            }
        }
    }
}