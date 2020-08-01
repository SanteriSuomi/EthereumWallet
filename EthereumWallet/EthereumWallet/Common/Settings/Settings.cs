using EthereumWallet.Common.Database;
using SQLite;

namespace EthereumWallet.Common.Settings
{
    public class Settings : IDatabaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Endpoint Endpoint { get; set; } = Endpoint.Mainnet;
    }
}
