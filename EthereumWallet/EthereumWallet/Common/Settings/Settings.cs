using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Database;
using SQLite;
using System;

namespace EthereumWallet.Common.Settings
{
    public class Settings : IDatabaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private Endpoint _endpoint;
        public Endpoint Endpoint
        {
            get => _endpoint;
            set
            {
                _endpoint = value;
                App.SettingsChangedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}