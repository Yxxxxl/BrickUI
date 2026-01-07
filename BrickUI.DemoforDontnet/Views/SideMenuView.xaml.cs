using BrickUI.ViewModels;
using System.Windows.Controls;

namespace BrickUI.DemoforDontnet.Views
{
    public partial class SideMenuView : Page
    {
        public SideMenuView(SideMenuViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }
    }
}
