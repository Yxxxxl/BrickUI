using BrickUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickUI.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Constructor

        #endregion

        #region Fields
        [ObservableProperty]
        private object view;
        #endregion

        #region Properties

        #endregion

        #region Commands
        [RelayCommand]
        private void Loaded()
        {
            view = App.Services.GetRequiredService<SideMenuView>();
        }
        #endregion

        #region Methods

        #endregion

        #region EventCallBacks

        #endregion
    }
}
