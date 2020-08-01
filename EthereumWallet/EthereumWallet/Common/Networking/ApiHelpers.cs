using EthereumWallet.ApplicationBase;
using EthereumWallet.Common.Settings;
using System;

namespace EthereumWallet.Common.Networking
{
    public static class ApiHelpers
    {
        /// <summary>
        /// Receive a GET url using api constants and the specific request string.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>GET Url</returns>
        public static string GetEthplorerUrl(string request)
        {
            switch (App.Settings.Endpoint)
            {
                case Endpoint.Mainnet:
                    return $"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}";
                case Endpoint.Kovan:
                    return $"{ApiConstants.EthplorerKovanApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}";
                default:
                    return $"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}";
            }
        }

        public static Uri GetEthplorerUri(string request)
        {
            switch (App.Settings.Endpoint)
            {
                case Endpoint.Mainnet:
                    return new Uri($"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}");
                case Endpoint.Kovan:
                    return new Uri($"{ApiConstants.EthplorerKovanApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}");
                default:
                    return new Uri($"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}");
            }
        }
    }
}