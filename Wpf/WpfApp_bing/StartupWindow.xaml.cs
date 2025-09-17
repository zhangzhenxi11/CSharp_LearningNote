using System.Windows;
using WpfApp_bing.Views;

namespace WpfApp_bing
{
    /// <summary>
    /// 启动选择窗口，让用户选择查看传统WPF还是Prism版本
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
        }

        private void ShowTraditionalWpf_Click(object sender, RoutedEventArgs e)
        {
            // 显示传统WPF版本
            var traditionalWindow = new MainWindow();
            traditionalWindow.Show();
        }

        private void ShowPrismVersion_Click(object sender, RoutedEventArgs e)
        {
            // 显示Prism版本
            var prismWindow = new MainWindowPrism();
            prismWindow.Show();
        }

        private void ShowBothVersions_Click(object sender, RoutedEventArgs e)
        {
            // 同时显示两个版本进行对比
            var traditionalWindow = new MainWindow();
            var prismWindow = new MainWindowPrism();
            
            traditionalWindow.Show();
            prismWindow.Show();
            
            // 调整窗口位置，方便对比
            traditionalWindow.Left = 100;
            traditionalWindow.Top = 100;
            
            prismWindow.Left = traditionalWindow.Left + traditionalWindow.Width + 20;
            prismWindow.Top = 100;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}