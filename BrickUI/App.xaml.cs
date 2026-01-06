using BrickUI.ViewModels;
using BrickUI.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BrickUI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton<SideMenuView>();

            // ViewModels
            services.AddSingleton<MainWindowViewModel>();
        
            services.AddSingleton<SideMenuViewModel>();

            // Views
            services.AddSingleton(sp => 
            { 
                return new MainWindow(sp.GetRequiredService<MainWindowViewModel>());
            });


            Services = services.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

    }
}
