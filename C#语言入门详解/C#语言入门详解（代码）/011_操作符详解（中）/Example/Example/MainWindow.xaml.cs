using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Example
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.myButton.Click += MyButton_Click;  //this.myButton.Click事件
            /*
            this.myButton.Click += delegate (object sender, RoutedEventArgs e)   //使用delegate操作符声明匿名方法，过时的使用方式
            {
                this.myTextBox.Text = "Hello, World!";
            };
            */

            this.myButton.Click += (sender, e) =>   //使用Lamda表达式声明匿名方法，新的方式更加简洁
            {
                this.myTextBox.Text = "Hello, World!";
            };
        }
        /*
        private void MyButton_Click(object sender, RoutedEventArgs e)   //MyButton_Click事件处理器
        {
            this.myTextBox.Text = "Hello, World!";
        }
        */
    }
}
