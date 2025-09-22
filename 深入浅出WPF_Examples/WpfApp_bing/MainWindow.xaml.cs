﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_bing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student stu;
        public MainWindow()
        {
            InitializeComponent();

            //准备数据源
            stu = new Student();

            //准备Binding

            Binding binding = new Binding();
            binding.Source = stu;
            binding.Path = new PropertyPath("Name");

            //DependencyProperty : 依赖属性
            //public static BindingExpressionBase SetBinding(DependencyObject target, DependencyProperty dp, BindingBase binding);
            //使用Binding连接数据源与Binding目标
            BindingOperations.SetBinding(this.textBoxName,TextBox.TextProperty,binding);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stu.Name += "Name";
        }
    }
}