using BrickUI.Controls;
using BrickUI.Controls;
using BrickUI.DemoforDontnet.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.IconPacks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace BrickUI.DemoforDontnet.ViewModels
{
    public partial class SideMenuViewModel : ObservableObject
    {
        #region Constructor
        public SideMenuViewModel()
        {
            ItemCommand = new RelayCommand<SideMenuItem>(OnItemInvoked);

            menuItems = new ObservableCollection<SideMenuItem>
            {
                new SideMenuItem("Home", new PackIconMaterial { Kind = PackIconMaterialKind.ViewDashboard, Width = 18, Height = 18 }, ItemCommand),
                new SideMenuItem("LogConsole", new PackIconMaterial { Kind = PackIconMaterialKind.Devices, Width = 18, Height = 18 }, ItemCommand),
                new SideMenuItem("Reports", new PackIconMaterial { Kind = PackIconMaterialKind.ChartLine, Width = 18, Height = 18 }, ItemCommand),
                new SideMenuItem("Settings", new PackIconMaterial { Kind = PackIconMaterialKind.CogOutline, Width = 18, Height = 18 }, ItemCommand),
            };

            selectedItem = menuItems[0];
        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        [ObservableProperty]
        private object showView;

        [ObservableProperty]
        private SideMenuItem selectedItem;

        [ObservableProperty]
        private ObservableCollection<SideMenuItem> menuItems;

        #endregion

        #region Commnads

        #endregion

        #region Methods

        #endregion

        #region EventCallBack

        #endregion

        public RelayCommand<SideMenuItem> ItemCommand { get; }

        private void OnItemInvoked(SideMenuItem item)
        {
            if (item == null)
            {
                return;
            }

            if (item.Text == "Home")
            {
                ShowView = null;
            }
            else if (item.Text == "LogConsole")
            {
                ShowView = App.Services.GetRequiredService<LogConsoleView>();
            }
        }
    }
}
