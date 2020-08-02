using EthereumWallet.Common.Settings;
using System;

namespace EthereumWallet.Common.Networking
{
    public static class ApiHelpers
    {
        public static Uri GetEthplorerUri(Endpoint endpoint, string request)
        {
            switch (endpoint)
            {
                case Endpoint.Mainnet:
                    return new Uri($"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}");
                case Endpoint.Kovan:
                    return new Uri($"{ApiConstants.EthplorerKovanApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}");
            }

            return new Uri($"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}");
        }
    }
}