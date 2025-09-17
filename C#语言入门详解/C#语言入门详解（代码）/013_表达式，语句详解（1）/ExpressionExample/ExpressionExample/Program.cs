using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressionExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x;
            x = 100;    //赋值表达式，x整线变量，100整型数值，得到一个数值value

            int y = 100;
            y++;
            ++y;

            new Form(); //new表达式，Form类型，()调用构造函数，得到一个对象(实例)object
            //(new Form()).ShowDialog();

            Action myAction = new Action(Console.WriteLine);    //.成员访问操作符，Console.WriteLine成员访问表达式，得到一个方法method

            System.Windows.Forms.Form myform2 = new Form(); //System.Windows.Forms.Form名称空间namespace访问表达式


            int x2 = 100;   //字面值参与表达式
            string y2 = "Mr.Okay";

            double x3 = Math.Pow(2, 3); //函数调用参与表达式

            int x4 = 2 + 3; //操作符+和操作数2、3参与表达式

            int x5 = 100;
            int y5 = x5;    //变量名参与表达式

            Type myType = typeof(Int64);    //操作符typeof、操作数是Int64类型名参与表达式
            Console.WriteLine(myType.FullName);
            Console.WriteLine("==========");


            //if (true)
            if (3 < 5)
            {
                Console.WriteLine("OK");
            }

            var x6 = 3 < 5; //3 < 5关系运算的结果是布尔类型
            Console.WriteLine(x6);
            Console.WriteLine("==========");


            Student stu = new Student();
            var x7 = stu.ID;
            var y7 = stu.Name;  //成员访问表达式的数据类型，根据成员的数据类型

            var x8 = Math.Pow(2, 3);    //Math.Pow方法的返回值是double类型
            Console.WriteLine(x8.GetType().Name);
            Console.WriteLine("==========");


            List<int> intList = new List<int>() { 1, 2, 3 };
            double[] doubleArray = new double[] { 1.0, 2.0, 3.0 };
            var x9 = intList[1];    //方法调用表达式的数据类型，根据方法返回的数据类型
            Console.WriteLine(x9.GetType());
            var y9 = doubleArray[1];
            Console.WriteLine(y9.GetType());
            Console.WriteLine("==========");


            int x10 = 100;
            Console.WriteLine(x10.GetType());
            Console.WriteLine(x10++);   //这里打印的是x10的值，而不是x10++后的值
            Console.WriteLine(x10--);   //这里打印的是x10++后的值，而不是x10--后的值
            Console.WriteLine(x10); //这里打印的是x10--后的值
            Console.WriteLine("=====");

            Console.WriteLine(++x10);   //这里打印的是++x10后的值
            Console.WriteLine(--x10);   //这里打印的是--x10后的值
            Console.WriteLine(x10); //这里打印的是x10的值
            Console.WriteLine("==========");


            Console.WriteLine((new Form()).GetType().FullName); //new表达式的返回数据类型是类
            Console.WriteLine("==========");


            var x11 = default(Int32);   //(T)x返回数据类型与目标数据类型一致，转换不成功就异常终止
            Console.WriteLine(x11);
            Console.WriteLine(x.GetType().FullName);
            Console.WriteLine("==========");


            var x12 = 4 / 3;
            Console.WriteLine(x.GetType().FullName);
            Console.WriteLine((4.0/3).GetType().FullName);  //没有发生数值提升时返回数据类型和操作数数据类型一致 //发生数值提升后，返回类型是操作数精度最高的数据类型
            Console.WriteLine("==========");

            long x13 = 100;
            Console.WriteLine((x << 2).GetType().FullName); //<<返回数据类型与左边操作数据类型一致
            Console.WriteLine("==========");


            int? x14 = null;    //int?可空
            //Nullable<int> x14 = null;   //等同于上一句
            var y14 = x14 ?? 100;
            Console.WriteLine(y14.GetType().FullName);
            Console.WriteLine("==========");


            var x15 = 5 > 3 ? 2 : 3.0;
            //var x15 = 5 > 3 ? "2.0" : 3.0;    //报错，"2.0"无法直接转换成int
            Console.WriteLine(x15);
            Console.WriteLine(x.GetType().FullName);
            Console.WriteLine("==========");


            int x16 = 100;
            int y16;
            Console.WriteLine((y = x).GetType().FullName);  //=返回数据类型是赋值左边变量得到的值数据类型
            Console.WriteLine("==========");


            bool b = false;
            if (b == 5 > 3)
            //if (b = 5 > 3)    //如果输错结果就会错误
            {
                Console.WriteLine("Hello");
            }
            Console.WriteLine("==========");


            int x17 = 100;
            int y17;
            y17 = x17;  //一个变量


            System.Windows.Forms.Form myForm;   //名称空间System Windows Forms


            var t = typeof(Int32);  //一个类型


            Console.WriteLine("Hello, World");  //Console.WriteLine，这是一组方法，重载决策决定具体调用哪一个
            Console.WriteLine("==========");


            Form myForm2 = null;    //一个空值


            Action a = delegate () { Console.WriteLine("Hello, World!"); }; //一个匿名方法
            a();
            Console.WriteLine("==========");


            Form myForm3 = new Form();
            myForm3.Text = "Hello";
            myForm3.Load += MyForm3_Load;   //事件挂接事件处理器
            myForm3.ShowDialog();
            Console.WriteLine("==========");


            List<int> intList2 = new List<int>() { 1, 2, 4 };
            int x18 = intList2[2];  //访问处理器


            Console.WriteLine("Hello, World");  //对返回值为void的方法的调用
            Console.WriteLine("==========");

        }

        private static void MyForm3_Load(object sender, EventArgs e)
        {
            Form form = sender as Form; //将object类型转换成Form
            if(form == null)
            {
                return;
            }
            form.Text = "New Title";
        }

        class Student
        {
            public int ID;
            public string Name;
        }
    }
}
