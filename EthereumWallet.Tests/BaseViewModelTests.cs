using Xunit;

namespace EthereumWallet.Tests
{
    public class BaseViewModelTests
    {
        [Fact]
        public void OnPropertyChanged_raises_propertychanged_correctly()
        {
            var model = new TestBaseViewModel();

            model.PropertyChanged += (obj, args) =>
            {
                Assert.Equal(nameof(model.TestProperty), args.PropertyName);
            };

            model.TestProperty = true;
        }
    }
}