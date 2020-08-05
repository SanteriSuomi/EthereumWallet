using Autofac;
using EthereumWallet.Common.Database;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Common.Settings;
using EthereumWallet.Modules.Login;
using Serilog;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EthereumWallet.ApplicationBase
{
    public partial class App : Application
    {
        public static IRepository<Settings> SettingsRepository { get; private set; }
        public static Settings Settings { get; set; }
        public static EventHandler SettingsChangedEvent { get; set; }

        public static string LogFilePath { get; private set; }

        public static IContainer Container { get; private set; }

        private readonly NavigationPage _navigationPage;

        public App(bool isTest)
        {
            InitializeComponent();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .AsImplementedInterfaces()
                   .AsSelf();

            _navigationPage = new NavigationPage();
            var lazyNavigation = new Lazy<INavigation>(() => _navigationPage.Navigation);
            builder.RegisterType<NavigationService>()
                   .As<INavigationService>()
                   .WithParameter("navigation", lazyNavigation);

            builder.RegisterGeneric(typeof(Repository<>))
                   .As(typeof(IRepository<>))
                   .WithParameter("path", DatabaseConstants.Path)
                   .WithParameter("flags", DatabaseConstants.Flags);

            builder.RegisterType<Web3Service>()
                   .As<IWeb3Service>()
                   .SingleInstance();

            Container = builder.Build();
            
            if (!isTest)
            {
                LogFilePath = Path.Combine(FileSystem.AppDataDirectory, "Logs/ethereumwalletlog.txt");
                Log.Logger = new LoggerConfiguration()
                             .MinimumLevel.Debug()
                             .WriteTo.Debug()
                             .WriteTo.File(LogFilePath)
                             .CreateLogger();
            }

            SettingsRepository = Container.Resolve<IRepository<Settings>>();
            Settings = SettingsRepository.GetFirstOrDefault().Result;
            if (Settings is null)
            {
                Settings = new Settings();
            }
        }

        protected override async void OnStart()
        {
            MainPage = _navigationPage;
            await _navigationPage.PushAsync(Container.Resolve<LoginView>());
        }
    }
}