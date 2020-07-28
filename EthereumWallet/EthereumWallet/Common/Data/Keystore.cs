namespace EthereumWallet.Common.Data
{
    #pragma warning disable IDE1006 // Naming Styles
    public class Cipherparams
    {
        public string iv { get; set; }
    }

    public class Kdfparams
    {
        public int dklen { get; set; }
        public string salt { get; set; }
        public int n { get; set; }
        public int r { get; set; }
        public int p { get; set; }
    }

    public class Crypto
    {
        public string ciphertext { get; set; }
        public Cipherparams cipherparams { get; set; }
        public string cipher { get; set; }
        public string kdf { get; set; }
        public Kdfparams kdfparams { get; set; }
        public string mac { get; set; }
    }

    public class KeyStoreRoot
    {
        public int version { get; set; }
        public string id { get; set; }
        public string address { get; set; }
        public Crypto crypto { get; set; }
    }
}