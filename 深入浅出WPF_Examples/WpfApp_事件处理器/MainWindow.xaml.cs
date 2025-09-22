using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_事件处理器
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
            //this.button1.Click += Button1_Click;
        }

        //private void Button1_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show(this.button1.Name);
        //}


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.button1.Name);
        }
    }
}
/*
 // <Button x:Name="button1" Content="Click Me!" Width="200" Height="100" Click = "button1_Click" />
        this.button1.Click += Button1_Click;
 事件模型：
 事件拥有者<---------  事件订阅机制 <--------- 事件响应者
 按钮button1对象       点击事件触发           MainWindow窗口
 
*/