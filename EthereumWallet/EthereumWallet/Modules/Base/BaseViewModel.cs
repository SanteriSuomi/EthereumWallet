using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EthereumWallet.Modules.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}