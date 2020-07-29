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
            return $"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}";
        }

        public static Uri GetEthplorerUri(string request)
        {
            return new Uri($"{ApiConstants.EthplorerMainnetApiUrl}{request}?apiKey={ApiConstants.EthplorerApiKey}");
        }
    }
}