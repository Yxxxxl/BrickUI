using BrickUI.ViewModels;
using System.Windows.Controls;

namespace BrickUI.Views
{
    public partial class SideMenuDemoView : UserControl
    {
        public SideMenuDemoView()
        {
            InitializeComponent();
            DataContext = new SideMenuDemoViewModel();
        }
    }
}
