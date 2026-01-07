using BrickUI.DemoforDontnet.ViewModels;
using System.Windows.Controls;

namespace BrickUI.DemoforDontnet.Views
{
    public partial class SideMenuView : UserControl
    {
        public SideMenuView(SideMenuViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }
    }
}
