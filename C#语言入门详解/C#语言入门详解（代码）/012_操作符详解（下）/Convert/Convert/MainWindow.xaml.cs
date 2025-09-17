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

namespace Convert
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            double x = System.Convert.ToDouble(tb1.Text);
            double y = System.Convert.ToDouble(tb2.Text);

            double x2 = double.Parse(this.tb1.Text); //double.Parse的输入需要符合数值格式
            double y2;
            bool r = double.TryParse(tb2.Text, out y2); //double.TryParse尝试转换并输出数值和结果

            double result = x + y;
            //this.tb3.Text = result; //无法直接转换
            //this.tb3.Text = System.Convert.ToString(result);  //方法1，使用Convert类的ToString方法
            this.tb3.Text = result.ToString();  //方法2，直接调用数值数据的实例方法

            object o = 2;
            o.ToString();   //所有数据类型的都由object派生出来
            o.Equals(this);
            o.GetType().ToString();
            o.GetHashCode();
        }
    }
}
