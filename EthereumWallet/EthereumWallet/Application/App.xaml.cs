using Autofac;
using EthereumWallet.Common.Database;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Common.Settings;
using EthereumWallet.Modules.Login;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EthereumWallet.ApplicationBase
{
    public partial class App : Application
    {
        public static IRepository<Settings> SettingsRepository { get; private set; }
        public static Settings Settings { get; private set; }
        public static IContainer Container { get; private set; }

        private readonly NavigationPage _navigationPage;

        public App()
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
            SettingsRepository = Container.Resolve<IRepository<Settings>>();
        }

        protected override async void OnStart()
        {
            await InitializeSettings();
            MainPage = _navigationPage;
            await _navigationPage.PushAsync(Container.Resolve<LoginView>());
        }

        private static async Task InitializeSettings()
        {
            Settings = await SettingsRepository.GetFirstOrDefault();
            if (Settings is null)
            {
                Settings = new Settings();
            }
        }
    }
}