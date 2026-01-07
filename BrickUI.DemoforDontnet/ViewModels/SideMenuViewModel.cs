using BrickUI.Controls.SideMenu;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;

namespace BrickUI.DemoforDontnet.ViewModels
{
    public class SideMenuViewModel : ObservableObject
    {
        private SideMenuItem _selectedItem;
        private string _statusText;

        public SideMenuViewModel()
        {
            StatusText = "Ready";
            ItemCommand = new RelayCommand<SideMenuItem>(OnItemInvoked);

            MenuItems = new ObservableCollection<SideMenuItem>
            {
                new SideMenuItem("Dashboard", new PackIconMaterial { Kind = PackIconMaterialKind.ViewDashboard, Width = 18, Height = 18 }, ItemCommand),
                new SideMenuItem("Devices", new PackIconMaterial { Kind = PackIconMaterialKind.Devices, Width = 18, Height = 18 }, ItemCommand),
                new SideMenuItem("Reports", new PackIconMaterial { Kind = PackIconMaterialKind.ChartLine, Width = 18, Height = 18 }, ItemCommand),
                new SideMenuItem("Settings", new PackIconMaterial { Kind = PackIconMaterialKind.CogOutline, Width = 18, Height = 18 }, ItemCommand),
            };

            SelectedItem = MenuItems[0];
        }

        public ObservableCollection<SideMenuItem> MenuItems { get; }

        public RelayCommand<SideMenuItem> ItemCommand { get; }

        public SideMenuItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value) && value != null)
                {
                    StatusText = "Selected: " + value.Text;
                }
            }
        }

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        private void OnItemInvoked(SideMenuItem item)
        {
            if (item == null)
            {
                return;
            }

            StatusText = "Command: " + item.Text;
        }
    }
}
