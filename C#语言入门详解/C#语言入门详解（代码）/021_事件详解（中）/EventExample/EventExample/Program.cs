using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace EventExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();  //事件拥有者timer，包含事件Elapsed    //类Timer，类型变量timer引用实例new Timer()
            timer.Interval = 1000;  //时间间隔 1000ms=1s
            Boy boy = new Boy(); //事件响应者boy，包含事件处理器Action
            Girl girl = new Girl(); //事件响应者girl
            timer.Elapsed += boy.Action;    //事件Elapsed（带闪电图标），事件订阅操作符+=，事件拥有者.事件+=事件响应者.事件处理器  //使用自动生成Action方法
            timer.Elapsed += girl.Action;   //一个事件有两个事件处理器boy.Action和girl.Action //事件订阅，本质上是一种以委托类型为基础的“约定”（事件处理器与事件匹配）
            timer.Start();  //启动Timer
            Console.ReadLine();
            timer.Stop();
            Console.WriteLine("==========");


            //(1星)事件拥有者 和 事件响应者 是不同的对象
            Form form = new Form(); //事件拥有者form
            Controller controller = new Controller(form);   //事件响应者，实例controller
            form.ShowDialog();


            //(2星)对象用自己的方法订阅和处理自己的事件
            //Form form1 = new Form();
            //form.Click += form.Action; //不能这样用，Form没有Action
            MyForm form1 = new MyForm();    //事件拥有者form1
            form1.Click += form1.FormClicked;   //事件form1.Click（隶属于类MyForm，继承与类Form），事件的响应者类MyForm的实例form1
            form1.ShowDialog();


            //(3星)事件的拥有者 是 事件的响应者 的字段成员，事件的拥有者 用自己的方法 订阅 自己某个成员的事件
            MyForm2 form2 = new MyForm2();
            form2.ShowDialog();
        }
    }
    class Boy
    {
        internal void Action(object sender, ElapsedEventArgs e) //事件处理器Action
        {
            Console.WriteLine(sender);  //System.Timers.Timer
            Console.WriteLine(e);   //System.Timers.ElapsedEventArgs

            Console.WriteLine("Jump!");
        }
    }
    class Girl
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Sing!");
        }
    }
    class Controller
    {
        private Form form;  //Form类型字段form
        public Controller(Form form)
        {
            if (form != null)    //过滤
            {
                this.form = form;   //this.form获得form对象引用，使用this区分字段和传进来的参数
                this.form.Click += this.FormClicked;    //事件form.Click，事件处理器FormClicked，事件订阅+=
            }
        }

        private void FormClicked(object sender, EventArgs e)    //事件处理器FormClicked，注意此处EventArgs，不能拿ElapsedEventArgs事件处理器去响应Click事件，遵循约束不同所以不通用
        {
            Console.WriteLine(sender);  //System.Windows.Forms.Form, Text
            Console.WriteLine(e);   //System.Windows.Forms.MouseEventArgs

            this.form.Text = DateTime.Now.ToString();   //Controller实例的方法   //this.form.Text被赋值
        }
    }
    class MyForm : Form   //派生，在原有类的基础上扩展功能，基类Form
    {
        internal void FormClicked(object sender, EventArgs e)   //事件处理器实例form1的方法FormClicked
        {
            Console.WriteLine(sender);  //EventExample.MyForm, Text
            Console.WriteLine(e);   //System.Windows.Forms.MouseEventArgs
            this.Text = DateTime.Now.ToString();
        }
    }
    class MyForm2 : Form
    {
        private TextBox textBox;
        private Button button;  //事件用于者button，是Form的字段成员
        public MyForm2()    //事件响应者MyForm2
        {
            this.textBox = new TextBox();
            this.button = new Button();
            this.Controls.Add(this.button);
            this.Controls.Add(this.textBox);
            this.button.Click += this.ButtonClicked; //事件this.button.Click，事件处理器this.ButtonClicked
            this.button.Text = "Say Hello";
            this.button.Top = 100;  //非可视化编程
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            this.textBox.Text = "Hello,World!!!!!!!!!!!!!!";
        }
    }
}
