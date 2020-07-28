using System;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Extensions
{
    #pragma warning disable S3168 // Justification: safe fire and forget task extension
    public static class TaskExtensions
    {
        /// <summary>
        /// Safely fire and forget an async method.
        /// </summary>
        /// <param name="returnContext">Optionally return the context to the caller (true) or don't (false).</param>
        /// <param name="errorHandler">Optional error handler method.</param>
        public static async void SafeFireAndForget(this Task task, bool returnContext = false, Action<Exception> errorHandler = null)
        {
            try
            {
                await task.ConfigureAwait(returnContext);
            }
            catch (Exception e)
            {
                errorHandler?.Invoke(e);
            }
        }

        /// <summary>
        /// Safely fire and forget an async method and return the task result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="returnContext"></param>
        /// <param name="errorHandler"></param>
        /// <returns>Task result.</returns>
        public static async Task<T> SafeFireAndForget<T>(this Task<T> task, bool returnContext = false, Action<Exception> errorHandler = null)
        {
            try
            {
                return await task.ConfigureAwait(returnContext);
            }
            catch (Exception e)
            {
                errorHandler?.Invoke(e);
            }

            return default;
        }
    }
}