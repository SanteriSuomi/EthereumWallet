using SQLite;
using System;

namespace EthereumWallet.Common.Database
{
    public static class DatabaseConstants
    {
        public const string Filename = "Movies.db3";

        public const SQLiteOpenFlags Flags =
                     SQLiteOpenFlags.ReadWrite |
                     SQLiteOpenFlags.Create |
                     SQLiteOpenFlags.SharedCache;

        public static string Path
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return System.IO.Path.Combine(basePath, Filename);
            }
        }
    }
}