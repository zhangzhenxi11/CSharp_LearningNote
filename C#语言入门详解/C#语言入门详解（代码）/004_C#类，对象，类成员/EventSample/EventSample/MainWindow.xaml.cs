using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading; //命名空间System.Windows.Threading

namespace EventSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();  //以事件为侧重点的类，类DispatcherTimer，引用变量timer，实例new DispatcherTimer()
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;   //触发Tick事件时，执行Timer_Tick方法，事件处理器
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.timeTextBox.Text = DateTime.Now.ToString();
        }
    }
}
