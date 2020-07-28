using System;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Networking.HTTP
{
    public interface INetworkService
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> GetAsync<TResult>(Uri uri);
    }
}