﻿using EthereumWallet.Common.Settings;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Networking.WebThree
{
    public interface IWeb3Service
    {
        Web3 Client { get; }
        Account Account { get; }
        void UpdateClient(Endpoint endpoint);
        Task<bool> TrySetAccountPrivateKey(string privateKey);
        Task<bool> TrySetAccountKeystore(string json, string password);
    }
}