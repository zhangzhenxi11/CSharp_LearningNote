using System;
using System.Windows;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 半导体控制系统应用程序入口
    /// 使用简化的WPF应用架构
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new SemiconductorMainWindow();
            var mainViewModel = new MainViewModel();
            
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}