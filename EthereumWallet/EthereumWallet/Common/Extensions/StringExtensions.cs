using System;

namespace EthereumWallet.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidAddress(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            if (s.Length == 42
                && s[0] == '0'
                && s[1] == 'x')
            {
                for (int i = 2; i < s.Length; i++)
                {
                    var digit = s[i];
                    if (!Uri.IsHexDigit(s[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}