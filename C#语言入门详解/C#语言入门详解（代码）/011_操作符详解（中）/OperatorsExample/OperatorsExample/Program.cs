using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OperatorsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int r;  //显示类型变量x
            var s = 100;  //隐式类型变量y
            var t = 100L;
            var u = 100D;
            //u = "100";  //报错
            Console.WriteLine(u);

            new Form(); //new Form使用new在内存中创建Form的实例，调用其实例构造器()圆括号，此处没有变量引用
            Form myForm = new Form();
            myForm.Text = "Hello";
            myForm.ShowDialog();

            Form myForm2 = new Form() { Text = "Hello", FormBorderStyle = FormBorderStyle.SizableToolWindow };   //new还可以调用实例的初始化器{}花括号，初始化多个属性
            myForm2.ShowDialog();

            new Form().ShowDialog();    //可以执行，但是窗口关闭后无法再次使用
            Console.WriteLine("==========");


            string name = "Tim"; //String是类类型，没有调用new操作符，这是C#语言的语法糖衣

            char[] str = { 'T', 'i', 'm' };
            String name2 = new String(str); //如果string引用变量非要用new

            int[] myArray = new int[5];
            int[] myArray2 = { 1, 2, 3, 4, 5 }; //直接用初始化器，没有用语法糖衣


            Form myForm3 = new Form() { Text = "Hello" };
            var person = new { Name = "Mr.Okay", Age = 34 };//使用new为匿名类型创建对象(实例)，var的强大功能
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            Console.WriteLine(person.GetType().Name);   //<>f__AnonymousType0`2匿名类型，约定前缀<>f__AnonymousType0，`2表示泛型类需要2个类型
            Console.WriteLine("==========");


            Student stu = new Student();
            stu.Report();
            CsStudent csStudent = new CsStudent();
            csStudent.Report();
            Console.WriteLine("==========");


            uint x = uint.MaxValue; //无符号整线，不能表示负数，0~(2^32)-1，4294967295
            Console.WriteLine(x);
            string binStr = Convert.ToString(x, 2); //转换成二进制，1111,1111,1111,1111,1111,1111,1111,1111
            Console.WriteLine(binStr);
            try
            {
                uint y = checked(x + 1);    //checked检查异常，未经处理的异常:  System.OverflowException: 算术运算导致溢出。
                //uint y = unchecked(x + 1);    //uncheck不检查，C#默认模式
                Console.WriteLine(y);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("There's overflow");
            }

            checked
            {
                try
                {
                    uint y = x + 1;
                    Console.WriteLine(y);
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine("There's overflow");
                }
            }
            Console.WriteLine("==========");


            int a = sizeof(int);    //使用sizeof获得一个对象在内存中占用的字节数
            Console.WriteLine(a);
            int b = sizeof(long);
            Console.WriteLine(b);
            int c = sizeof(ulong);
            Console.WriteLine(c);
            int d = sizeof(double);
            Console.WriteLine(d);
            int e = sizeof(decimal);    //decimal比double更精确
            Console.WriteLine(e);

            unsafe
            {
                int f = sizeof(Student2);   //使用sizeof获取自定义结构体在内存中占用的字节数
                Console.WriteLine(f);
            }
            Console.WriteLine("==========");


            unsafe
            {
                Student3 stu3;
                stu3.ID = 1;
                stu3.Score = 99;
                Student3* pStu3 = &stu3;    //定义pStu3指针变量，&stu3获得stu3地址
                pStu3->Score = 100; //pStu3直接修改Score的数值，->通过指针间接访问
                (*pStu3).Score = 1000;  //*pStu3取得Stu3引用的实例
                Console.WriteLine(stu3.Score);  //stu3.Score中的.是直接访问
            }
            Console.WriteLine("==========");


            int x2 = 100;
            int y2 = +x2;
            Console.WriteLine(y2);
            y2 = -x2;
            Console.WriteLine(y2);
            //y2 = --x2;    错误
            y2 = -(-x2);
            Console.WriteLine(y2);

            Console.WriteLine(int.MaxValue);
            Console.WriteLine(int.MinValue);    //有符号类型，负值比正值多一位

            int x3 = int.MinValue;
            //int y3 = -x3; //负数是正数的按位取反再加1
            int y3 = unchecked(-x3);  //溢出报错
            Console.WriteLine(x3);
            Console.WriteLine(y3);
            string xStr3 = Convert.ToString(x3, 2).PadLeft(32, '0');
            Console.WriteLine(xStr3);
            string yStr3 = Convert.ToString(y3, 2).PadLeft(32, '0');
            Console.WriteLine(yStr3);
            Console.WriteLine("==========");


            int x4 = 12345678;
            int y4 = ~x4;   //~二进制按位取放操作符，求反
            Console.WriteLine(y4);  //-12345679
            string xStr4 = Convert.ToString(x4, 2).PadLeft(32, '0');
            Console.WriteLine(xStr4);
            string yStr4 = Convert.ToString(y4, 2).PadLeft(32, '0');
            Console.WriteLine(yStr4);
            Console.WriteLine("==========");


            bool b1 = false;
            bool b2 = !b1;
            Console.WriteLine(b2);
            Console.WriteLine("==========");


            //Student4 stu4 = new Student4(null);   //Name没有赋值
            Student4 stu4 = new Student4("Tom");
            Console.WriteLine(stu4.Name);
            Console.WriteLine("==========");


            int x5 = 100;
            ++x5;   //无赋值时，前置++或后置++结果一致
            Console.WriteLine(x5);
            x5 = 100;
            x5++;
            Console.WriteLine(x5);
            x5 = 100;
            --x5;
            Console.WriteLine(x5);
            x5 = 100;
            x5--;
            Console.WriteLine(x5);
            Console.WriteLine("==========");


            x5 = 100;
            int y5 = x5++;  //后置++左边为赋值=时，先赋值，再执行自加1
            Console.WriteLine(x5);
            Console.WriteLine(y5);
            x5 = 100;
            y5 = ++x5;  //前置++左边为赋值=时，先执行++自加1
            //x5 = 100 + 1;
            //y5 = x5;
            Console.WriteLine(x5);
            Console.WriteLine(y5);
            x5 = 100;
            y5 = x5--;  //后置--左边为赋值=时，先赋值，再执行自减1
            Console.WriteLine(y5);
            x5 = 100;
            y5 = --x5;  //前置--左边为赋值=时，先执行--自减1
            //x5 = 100 - 1;
            //y5 = x5;
            Console.WriteLine(y5);
            Console.WriteLine("==========");


            x5 = 100;
            y5 = -x5;  //前置-，先做-1，再赋值
            //x5 = 100 - 1;
            //y5 = x5;
            Console.WriteLine(y5);
            Console.WriteLine("==========");
        }
        class Student
        {
            public void Report()
            {
                Console.WriteLine("I'm a student.");
            }
        }
        class CsStudent : Student   //CsStudent从Student派生出来 //类与类之间的关系，继承和派生
        {
            new public void Report()    //子类对父类方法的隐藏，此处new作为修饰符
            {
                Console.WriteLine("I'm CS student.");
            }
        }
        struct Student2
        {
            int ID;
            long score;
        }

        struct Student3
        {
            public int ID;
            public long Score;
        }
        class Student4
        {
            /*
            public Student4(string initName)
            {
                this.Name = initName;
            }
            */
            public Student4(string initName)
            {
                if (!string.IsNullOrEmpty(initName))    //判断是否为null或empty
                {
                    this.Name = initName;
                }
                else
                {
                    throw new ArgumentException("initName cannoe be null or empty.");   //抛异常信息
                }
            }
            public string Name;
        }
    }
}
