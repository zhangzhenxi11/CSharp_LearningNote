using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebFormExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button3.Click += this.ButtonClick; //常用的编写方法
            this.button3.Click += new EventHandler(this.ButtonClick);   //传统的编写方法，委托方法
            this.button4.Click += delegate (object sender, EventArgs e) //废弃的编写方法，匿名方法
            {
                this.textBox1.Text = "haha!";
            };
            this.button5.Click += (object sender, EventArgs e) =>   //新的编写方法，Lambda表达式
            {
                this.textBox1.Text = "Hoho!";
            };
        }

        private void ButtonClick(object sender, EventArgs e)    //一个事件处理器可以挂接多个事件   //一个事件可以挂接多个事件处理器
        {
            //this.textBox1.Text = "Hello, World!";
            if (sender == this.button1)
            {
                this.textBox1.Text = "Hello, ";
            }
            if (sender == this.button2)
            {
                this.textBox1.Text = "World!";
            }
            if (sender == this.button3)
            {
                this.textBox1.Text = "Mr.Okay!";
            }
        }
    }
}
