using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.button1.Click += ButtonClick;  //常用方法
            this.button2.Click += new RoutedEventHandler(this.ButtonClick2); //传统的编写方法，委托方式
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.textBox1.Text = "Hello, WASPEC!";
        }
        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            this.textBox1.Text = "Hello, world!";
        }
    }
}