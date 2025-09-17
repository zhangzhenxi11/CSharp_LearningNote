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
using System.Collections.ObjectModel;

namespace WpfApp_集合对象作为列表控件的ItemSource
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        // 在类中定义集合
        private ObservableCollection<Student> stuList; //集合的变化（添加、删除项）能自动反映到 UI 上，
        public MainWindow()
        {
            InitializeComponent();

            /*
             集合初始化器语法：
             List<T> list = new List<T>()
             {
                 item1,
                 item2,
                 item3
             };
             */
            //数据源
            stuList = new ObservableCollection<Student>()
            {
                new Student() { Id = 0, Name = "Tim", Age = 29 },
                new Student() { Id = 1, Name = "Tom", Age = 30 },
                new Student() { Id = 2, Name = "kyle", Age = 28 },
                new Student() { Id = 3, Name = "Tony", Age = 29 },
                new Student() { Id = 4, Name = "Vina", Age = 27 },
                new Student() { Id = 5, Name = "Mike", Age = 18 },
            };

            //为listBox设置Binding
            this.listBoxStudents.ItemsSource = stuList;  //ItemsControl类属性ItemsSource
           // this.listBoxStudents.DisplayMemberPath = "Name";

            //TextBOX 设置Binding
            Binding binding = new Binding("SelectedItem.Id") { Source = this.listBoxStudents };
            this.textBoxld.SetBinding(TextBox.TextProperty,binding);

        }
    }
}