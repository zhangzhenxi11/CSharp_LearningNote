using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //(1星)事件拥有者 和 事件响应者 是不同的对象
            Form form = new Form();
            Controller controller = new Controller(form);   //事件响应者，实例controller
            form.ShowDialog();

            //(2星)对象用自己的方法订阅和处理自己的事件
            //Form form1 = new Form();
            //form.Click += form.Action; //不能这样用，Form没有Action
            MyForm form1 = new MyForm();    //事件拥有者form1
            form1.Click += form1.FormCliked;
            form1.ShowDialog();

            //(3星)事件的拥有者 是 事件的响应者 的字段成员，事件的拥有者 用自己的方法 订阅 自己某个成员的事件
            MyForm2 form2 = new MyForm2();
            form2.ShowDialog();


        }

        private static void FormCliked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    class Controller
    {
        private Form form;  //Form类型字段form

        public Controller(Form form)
        {
            if (form != null)
            { 
                this.form = form; //this.form获得form对象引用，使用this区分字段和传进来的参数
                this.form.Click += FormClicked; //事件form.Click，事件处理器FormClicked，事件订阅+=
            }
        }

        private void FormClicked(object sender, EventArgs e)
        {
            Console.WriteLine(sender);  //System.Windows.Forms.Form, Text
            Console.WriteLine(e);   //System.Windows.Forms.MouseEventArgs

            this.form.Text = DateTime.Now.ToString();   //Controller实例的方法   //this.form.Text被赋值  
        }
    }

    class MyForm : Form //派生，在原有类的基础上扩展功能，基类Form
    {
        internal void FormCliked(object sender, EventArgs e) //事件处理器实例form1的方法FormClicked
        {
            Console.WriteLine(sender);
            Console.WriteLine(e);
            this.Text = DateTime.Now.ToString();
        }


    }
    class MyForm2 : Form
    {
        private TextBox textBox;
        private Button button;  //事件拥有者button，是Form的字段成员

        public MyForm2()
        {
            this.textBox = new TextBox();
            this.button = new Button();
            this.Controls.Add(this.button);
            this.Controls.Add(this.textBox);
            this.button.Click += ButtonClicked;//事件this.button.Click，事件处理器this.ButtonClicked
            this.button.Text = "Say Hello";
            this.button.Top = 100;  //非可视化编程
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            this.textBox.Text = "Hello,World!!!!!!!!!!!!!!";
        }
    }

}
