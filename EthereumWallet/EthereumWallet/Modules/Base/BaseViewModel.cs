using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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

        protected void NotifyTypePropertiesChanged(BaseViewModel model)
        {
            List<PropertyInfo> properties = GetProperties(model);
            foreach (var property in properties)
            {
                OnPropertyChanged(property.Name);
            }
        }

        private List<PropertyInfo> GetProperties(BaseViewModel model)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            try
            {
                var modelProperties = model.GetType().GetProperties();
                properties.AddRange(modelProperties);
                foreach (var property in modelProperties)
                {
                    var propertyProperties = property.DeclaringType.GetProperties();
                    properties.AddRange(propertyProperties);
                    foreach (var propertyProperty in propertyProperties)
                    {
                        properties.AddRange(propertyProperty.DeclaringType.GetProperties());
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Log.Warning(e, "Info/Tokens GetType() or GetProperties() was/were null.");
            }

            return properties;
        }
    }
}