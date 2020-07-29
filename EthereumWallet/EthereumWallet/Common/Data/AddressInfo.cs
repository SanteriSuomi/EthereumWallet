using System.Collections.Generic;

namespace EthereumWallet.Common.Data
{
    #pragma warning disable // Objects converted from JSON as per api requirements
    public class Price
    {
        public double rate { get; set; }
        public double diff { get; set; }
        public double diff7d { get; set; }
        public int ts { get; set; }
        public double marketCapUsd { get; set; }
        public double availableSupply { get; set; }
        public double volume24h { get; set; }
        public double diff30d { get; set; }
    }

    public class ETH
    {
        public double balance { get; set; }
        public Price price { get; set; }
    }

    public class TokenInfo
    {
        public string address { get; set; }
        public string name { get; set; }
        public string decimals { get; set; }
        public string symbol { get; set; }
        public string totalSupply { get; set; }
        public string owner { get; set; }
        public object lastUpdated { get; set; }
        public int issuancesCount { get; set; }
        public int holdersCount { get; set; }
        public int ethTransfersCount { get; set; }
        public object price { get; set; }
        public string alert { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string reddit { get; set; }
    }

    public class Token
    {
        public TokenInfo tokenInfo { get; set; }
        public double balance { get; set; }
        public int totalIn { get; set; }
        public int totalOut { get; set; }
    }

    public class AddressInfo
    {
        public string address { get; set; }
        public ETH ETH { get; set; }
        public int countTxs { get; set; }
        public IList<Token> tokens { get; set; }
    }
}