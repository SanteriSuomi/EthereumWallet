namespace EthereumWallet.Common.Data
{
    #pragma warning disable // Objects converted from JSON as per api requirements
    public class TokenInfoPrice
    {
        public double rate { get; set; }
        public double diff { get; set; }
        public double diff7d { get; set; }
        public int ts { get; set; }
        public double marketCapUsd { get; set; }
        public double availableSupply { get; set; }
        public double volume24h { get; set; }
        public double diff30d { get; set; }
        public string currency { get; set; }
    }

    public class TokenInfoWithPrice
    {
        public string address { get; set; }
        public string name { get; set; }
        public string decimals { get; set; }
        public string symbol { get; set; }
        public string totalSupply { get; set; }
        public string owner { get; set; }
        public int transfersCount { get; set; }
        public int lastUpdated { get; set; }
        public int issuancesCount { get; set; }
        public int holdersCount { get; set; }
        public int ethTransfersCount { get; set; }
        public TokenInfoPrice price { get; set; }
        public int countOps { get; set; }
    }
}