using EthereumWallet.Modules.Base;

namespace EthereumWallet.Tests
{
    public class TestBaseViewModel : BaseViewModel
    {
        private bool _testProperty;
        public bool TestProperty
        {
            get => _testProperty;
            set
            {
                _testProperty = value;
                OnPropertyChanged();
            }
        }
    }
}