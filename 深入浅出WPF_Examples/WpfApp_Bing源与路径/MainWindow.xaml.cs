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

namespace WpfApp_Bing源与路径
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //测试1：
            // <TextBox x:Name="textBox1" Text="{Binding Path=Value,ElementName=slider1}" BorderBrush="Black" Margin="5"/>
            //等价于=> C#代码
            //this.textBox1.SetBinding(TextBox.TextProperty, new Binding("Value") { ElementName = "silder1" });

            //测试2：
            //Binding构造器写法,public Binding(string path)
            //this.textBox2.SetBinding(TextBox.TextProperty, new Binding("Text.Length")
            //{
            //    Source = this.textBox1,
            //    Mode = BindingMode.OneWay
            //});


        }


    }
}