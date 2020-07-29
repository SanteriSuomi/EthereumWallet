﻿using EthereumWallet.Modules.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EthereumWallet.Common.Navigation
{
    public interface INavigationService
    {
        Task<bool> PushAsync<TViewModel>(object parameter = null, bool animated = true) where TViewModel : BaseViewModel;
        Task PopAsync();
        IReadOnlyList<Page> NavigationStack { get; }
    }
}