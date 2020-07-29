using System.Windows.Input;
using Xamarin.Forms;

namespace EthereumWallet.Common.Behaviours
{
    public class EntryTextChangedCommand : Behavior<Entry>
    {
        public static readonly BindableProperty TextChangedCommandProperty =
            BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(EntryTextChangedCommand), null);

        public ICommand TextChangedCommand
        {
            get  => (ICommand)GetValue(TextChangedCommandProperty);
            set  => SetValue(TextChangedCommandProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (TextChangedCommand.CanExecute(args))
            {
                TextChangedCommand.Execute(args);
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }
    }
}