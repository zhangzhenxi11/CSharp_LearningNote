using System.Windows;
using WpfApp_bing.Views;

namespace WpfApp_bing
{
    /// <summary>
    /// 启动选择窗口，让用户选择查看传统WPF还是Prism版本
    /// </summary>
    public partial class StartupWindow : Window
    {
        // 存储窗体实例的引用
        private MainWindow _traditionalWindow;
        private MainWindowPrism _prismWindow;

        public StartupWindow()
        {
            InitializeComponent();
        }

        private void ShowTraditionalWpf_Click(object sender, RoutedEventArgs e)
        {
            // 显示传统WPF版本
            if (_traditionalWindow == null || !_traditionalWindow.IsLoaded)
            {
                _traditionalWindow = new MainWindow();
                _traditionalWindow.Closed += (s, args) => _traditionalWindow = null; // 关闭后重置引用
                _traditionalWindow.Show();
            }
            else
            {
                _traditionalWindow.Activate(); // 激活现有窗口
            }
            //var traditionalWindow = new MainWindow();
            //traditionalWindow.Show();
        }

        private void ShowPrismVersion_Click(object sender, RoutedEventArgs e)
        {
            // 显示Prism版本
            if (_prismWindow == null || !_prismWindow.IsLoaded)
            {
                _prismWindow = new MainWindowPrism();
                _prismWindow.Closed += (s, args) => _prismWindow = null;
                _prismWindow.Show();
            }
            else
            {
                _prismWindow.Activate();
            }
            //var prismWindow = new MainWindowPrism();
            //prismWindow.Show();
        }

        private void ShowBothVersions_Click(object sender, RoutedEventArgs e)
        {
            // 处理传统WPF窗口
            if (_traditionalWindow == null || !_traditionalWindow.IsLoaded)
            {
                _traditionalWindow = new MainWindow();
                _traditionalWindow.Closed += (s, args) => _traditionalWindow = null;
                _traditionalWindow.Left = 100;
                _traditionalWindow.Top = 100;
                _traditionalWindow.Show();
            }
            else
            {
                _traditionalWindow.Activate();
            }

            // 处理Prism窗口
            if (_prismWindow == null || !_prismWindow.IsLoaded)
            {
                _prismWindow = new MainWindowPrism();
                _prismWindow.Closed += (s, args) => _prismWindow = null;
                _prismWindow.Left = _traditionalWindow.Left + _traditionalWindow.Width + 20;
                _prismWindow.Top = 100;
                _prismWindow.Show();
            }
            else
            {
                _prismWindow.Activate();
            }

            // 同时显示两个版本进行对比

            //var traditionalWindow = new MainWindow();
            //var prismWindow = new MainWindowPrism();

            //traditionalWindow.Show();
            //prismWindow.Show();

            //// 调整窗口位置，方便对比
            //traditionalWindow.Left = 100;
            //traditionalWindow.Top = 100;

            //prismWindow.Left = traditionalWindow.Left + traditionalWindow.Width + 20;
            //prismWindow.Top = 100;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}