using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp
{
    public partial class Form1 : Form   //myButton是Form的字段成员
    {
        public Form1()
        {
            InitializeComponent();

            //(3星)事件的拥有者 是 事件的响应者 的字段成员，事件的拥有者 用自己的方法 订阅 自己某个成员的事件
            this.myButton.Click += Button_Click;    //手动添加  //事件拥有者myButton，事件this.myButton.Click，事件响应器Button_Click
        }

        private void Button_Click(object sender, EventArgs e)   //Form的自定义方法Button_Click
        {
            this.myTextBox.Text = "Hello, World!";
        }

        private void myButton_Click(object sender, EventArgs e)
        {
            //自动生成
            this.myTextBox.Text = "Hello, World!";
        }
    }
}
