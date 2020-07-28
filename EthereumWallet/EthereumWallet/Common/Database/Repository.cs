using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EthereumWallet.Common.Extensions;

namespace EthereumWallet.Common.Database
{
    public class Repository<T> : IRepository<T> where T : class, IDatabaseItem, new()
    {
        private readonly Lazy<SQLiteAsyncConnection> lazyInitializer;

        private SQLiteAsyncConnection Database => lazyInitializer?.Value;

        public Repository()
        {
            lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
            {
                return new SQLiteAsyncConnection(DatabaseConstants.Path, DatabaseConstants.Flags);
            });

            Initialize().SafeFireAndForget(false, (e) =>
            {
                #if DEBUG
                throw e;
                #endif
            });
        }

        public Repository(string path, SQLiteOpenFlags flags)
        {
            lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
            {
                return new SQLiteAsyncConnection(path, flags);
            });

            Initialize().SafeFireAndForget(false, (e) =>
            {
                #if DEBUG
                throw e;
                #endif
            });
        }

        private async Task Initialize()
        {
            if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
            {
                await Database.CreateTableAsync(typeof(T)).ConfigureAwait(false);
            }
        }

        public Task<T> GetById(int id)
        {
            return Database.Table<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<T> GetByObject(T item)
        {
            return Database.GetAsync<T>(item);
        }

        public Task<int> DeleteAsync(T item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<List<T>> GetAllAsync()
        {
            return Database.Table<T>().ToListAsync();
        }

        public Task<int> SaveAsync(T item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
    }
}
