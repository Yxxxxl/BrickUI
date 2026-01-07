using BrickUI.DemoforDontnet.ViewModels;
using BrickUI.DemoforDontnet.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BrickUI.DemoforDontnet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Views
            services.AddSingleton(sp =>
            {
                return new MainWindow(sp.GetRequiredService<MainWindowViewModel>());
            });

            services.AddSingleton<SideMenuView>();

            services.AddSingleton<LogConsoleView>();

            // ViewModels
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<SideMenuViewModel>();

            services.AddSingleton<LogConsoleViewModel>();




            Services = services.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

    }

}
