using Autofac;
using EthereumWallet.Common.Database;
using EthereumWallet.Common.Navigation;
using EthereumWallet.Common.Networking.WebThree;
using EthereumWallet.Modules.Login;
using System;
using Xamarin.Forms;

namespace EthereumWallet.ApplicationBase
{
    public partial class App : Application
    {
        public static IContainer Container { get; set; }
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
        }

        protected override async void OnStart()
        {
            MainPage = _navigationPage;
            await _navigationPage.PushAsync(Container.Resolve<LoginView>());
        }

        protected override void OnSleep()
        {
            //
        }

        protected override void OnResume()
        {
            //
        }
    }
}
