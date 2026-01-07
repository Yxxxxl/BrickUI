using BrickUI.DemoforDontnet.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickUI.DemoforDontnet.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        #region Constructor
        public MainWindowViewModel()
        {
            view = App.Services.GetRequiredService<SideMenuView>();
        }
        #endregion

        #region Fields
        [ObservableProperty]
        private object view;
        #endregion

        #region Properties

        #endregion

        #region Commands
        [RelayCommand]
        private void Click() 
        {
            Debug.WriteLine("Button Clicked");
        }
        #endregion

        #region Methods

        #endregion

        #region EventCallBacks

        #endregion
    }
}
