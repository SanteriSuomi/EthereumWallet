namespace EthereumWallet.Common.Networking
{
    public static class ApiConstants
    {
        #region Infura
        public const string InfuraProjectId = "314787135cc04966af0f398fb32fb4c0";
        public const string InfuraProjectSecret = "07c10d4c3a034f6e87f812c8e533793d";

        public static string InfuraMainnetApiUrl = $"https://:{InfuraProjectSecret}@mainnet.infura.io/v3/{InfuraProjectId}";
        public static string InfuraKovanApiUrl = $"https://:{InfuraProjectSecret}@kovan.infura.io/v3/{InfuraProjectId}";
        #endregion

        #region Ethplorer
        public const string EthplorerApiKey = "EK-hnVEW-bCpqmdw-fqWdW";

        public static string EthplorerMainnetApiUrl = $"https://api.ethplorer.io/";
        public static string EthplorerKovanApiUrl = $"https://kovan-api.ethplorer.io/";
        #endregion
    }
}