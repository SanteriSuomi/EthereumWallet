using EthereumWallet.Modules.Dev.DevLog;
using Serilog;
using Serilog.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace EthereumWallet.Tests
{
    public class DevLogViewModelTests
    {
        public DevLogViewModelTests()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _filePath = Path.Combine(documentsPath, "testlog.txt");
            _logger = new LoggerConfiguration()
                      .WriteTo.File(_filePath)
                      .CreateLogger();
        }

        private readonly string _filePath;
        private readonly Logger _logger;

        [Fact]
        public void RefreshLogPressed_refreshes_log_correctly()
        {
            var model = new DevLogViewModel
            {
                RefreshLogPressed = new Command<string>(async (s) =>
                {
                    bool result = await GetAndSetLogText(s);
                    Assert.True(result);
                })
            };

            _logger.Error("Test Error");
            _logger.Error("Test Error");
            _logger.Dispose();
            model.RefreshLogPressed.Execute(_filePath);
        }

        [Fact]
        public void ClearLogPressed_clears_log_correctly()
        {
            var model = new DevLogViewModel
            {
                ClearLogPressed = new Command<string>(async (s) =>
                {
                    bool result = await ClearLogText(s);
                    Assert.True(result);
                })
            };

            _logger.Error("Test Error");
            _logger.Error("Test Error");
            _logger.Dispose();
            model.ClearLogPressed.Execute(_filePath);
        }

        private async Task<bool> GetAndSetLogText(string filePath)
        {
            string logText = string.Empty;
            return await Task.Run(() =>
            {
                if (File.Exists(filePath))
                {
                    logText = File.ReadAllText(filePath);
                }

                return logText.Length >= 1;
            });
        }

        private async Task<bool> ClearLogText(string filePath)
        {
            await Task.Run(() =>
            {
                File.Create(filePath);
            });

            return File.Exists(filePath);
        }
    }
}