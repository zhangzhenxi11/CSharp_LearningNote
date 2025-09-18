using Aga.Diagrams;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 半导体控制系统主窗口
    /// 展示WPF数据绑定和MVVM模式的实际应用
    /// </summary>
    public partial class SemiconductorMainWindow : Window
    {
        private SemiconductorProcessController _processController;

        public SemiconductorMainWindow()
        {
            InitializeComponent();
            
            // 设置图表控制器
            _processController = new SemiconductorProcessController(DiagramCanvas);
            DiagramCanvas.Controller = _processController;
            
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("半导体控制系统主窗口已加载");
            
            // 设置窗口标题
            Title = $"半导体设备流程控制系统 - {DateTime.Now:yyyy-MM-dd}";
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var viewModel = DataContext as MainViewModel;
                if (viewModel?.ProcessStatus == ProcessStatus.Running)
                {
                    var result = MessageBox.Show(
                        "流程正在运行中，确定要退出吗？",
                        "确认退出",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                    
                    if (result == MessageBoxResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                _processController?.Dispose();
                Console.WriteLine("半导体控制系统主窗口已关闭");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"关闭窗口时发生错误: {ex.Message}");
            }
        }

        #region 事件处理器

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeviceLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("设备库功能正在开发中...", "功能提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ParametersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("参数设置功能正在开发中...", "功能提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DataMonitorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("数据监控功能正在开发中...", "功能提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string deviceType)
            {
                var viewModel = DataContext as MainViewModel;
                viewModel?.AddDeviceCommand.Execute(deviceType);
            }
        }

        private void DiagramCanvas_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    var deviceType = e.Data.GetData(DataFormats.StringFormat) as string;
                    var position = e.GetPosition(DiagramCanvas);
                    
                    // 在指定位置添加设备
                    var viewModel = DataContext as MainViewModel;
                    viewModel?.AddDeviceCommand.Execute(deviceType);
                    
                    Console.WriteLine($"在位置 ({position.X}, {position.Y}) 添加设备: {deviceType}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"拖放设备时发生错误: {ex.Message}");
                MessageBox.Show($"添加设备失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 显示错误消息
        /// </summary>
        public void ShowError(string message, string title = "错误")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 显示信息消息
        /// </summary>
        public void ShowInfo(string message, string title = "信息")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        public bool ShowConfirm(string message, string title = "确认")
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        #endregion
    }
}