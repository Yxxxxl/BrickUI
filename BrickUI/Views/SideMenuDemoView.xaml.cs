using BrickUI.ViewModels;
using System.Windows.Controls;

namespace BrickUI.Views
{
    public partial class SideMenuDemoView : Page
    {
        public SideMenuDemoView(SideMenuDemoViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }
    }
}
