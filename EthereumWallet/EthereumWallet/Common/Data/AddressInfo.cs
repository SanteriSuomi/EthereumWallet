using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

    public class ETH : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private double _balance;
        public double balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }
        public Price price { get; set; }
    }

    public class TokenInfo
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string address { get; set; }
        private string _name;
        public string name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
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

    public class Token : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public TokenInfo tokenInfo { get; set; }
        private double _balance;
        public double balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }
        public int totalIn { get; set; }
        public int totalOut { get; set; }
    }

    public class AddressInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _address;
        public string address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
        public ETH ETH { get; set; }
        public int countTxs { get; set; }
        public IList<Token> tokens { get; set; }
    }
}