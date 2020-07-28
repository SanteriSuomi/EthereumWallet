namespace EthereumWallet.Common.Networking
{
    public static class ApiConstants
    {
        public const string ProjectId = "314787135cc04966af0f398fb32fb4c0";
        public const string ProjectSecret = "07c10d4c3a034f6e87f812c8e533793d";

        /// <summary>
        /// API Url for interacting with the Ethereum mainnet using Web3.
        /// </summary>
        public static string InfuraMainnetApiUrl = $"https://:{ProjectSecret}@mainnet.infura.io/v3/{ProjectId}";

        /// <summary>
        /// API Url for interacting with the Ethereum testnet Ropsten using Web3.
        /// </summary>
        public static string InfuraRopstenApiUrl = $"https://:{ProjectSecret}@ropsten.infura.io/v3/{ProjectId}";
    }
}