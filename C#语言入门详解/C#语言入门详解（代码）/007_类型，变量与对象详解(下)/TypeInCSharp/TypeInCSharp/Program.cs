using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeInCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //类Class
            Type myType = typeof(Form); //From上面右击转至定义，public class Form : ContainerControl
            Console.WriteLine(myType.FullName); //System.Windows.Forms.Form
            Console.WriteLine(myType.IsClass);

            //结构体Structures
            int a;  //int上面右击转至定义，public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
            long b; //long上面右击转至定义，public struct Int64 : IComparable, IFormattable, IConvertible, IComparable<long>, IEquatable<long>

            //枚举
            Form f = new Form();
            f.WindowState = FormWindowState.Normal;  //FormWindowState上面右击转至定义，public enum FormWindowState
            f.ShowDialog();

            object c;
            string d;
            bool e;
            byte g;
            e = true;

            short s = 200;
            long l = 200L;
            int x;  //局部变量x
            x = 100;
            x = s;
            //x = l;    //报错        

            //Student.Amount    //静态变量Amount，静态成员属于类

            Student stu = new Student();    //实例变量stu，非静态成员变量，字段    
            stu.Age = -1;   //此处赋值有问题，后续会讲属性保护值不被随意赋值

            int[] array = new int[100];     //数组元素变量array，array[0]~array[99]
            array[0] = 0;
            array[99] = 1;

            int a1;
            a1 = 100;
            int b1;
            b1 = 200;
            int c1 = a1 + b1;
            Console.WriteLine(c1);

            int h = 100, i = 200;   //写成一行可读性变差

            byte j; //0~255，无符号8位bit整数integer，1字word=2字节byte，1字节byte=8位bit
            j = 100;    //8位01100100

            sbyte sb;   //-128~-127，有符号8位整数
            sb = 100;   //符号位=0，后7位1100100
            sb = -100;  //符号位=1，后面按位取反+1，0011100

            ushort us;  //0~65535，无符号16位整数
            us = 1000;  //高8位00000011，低8位11101000

            short s1;   //-32768~32767，有符号16位整数，符号位在高位最前面，内存编号约往后越大
            s1 = 1000;  //符号位0，高7位00000011，低8位11101000
            s1 = -1000; //符号位1，后面按位取反+1，高7位00000011，低8位11101000
            string str = Convert.ToString(s1, 2);
            Console.WriteLine(str);

            int k;  //-2,147,483,648~2,147,483,647，有符号32位整数
            k = 10000;    //符号位0，高15位0000000 00000000，低16位001000111 00010000
            k = 10000;    //符号位0，后面按位取反+1，高15位0000000 00000000，低16位001000111 00010000

            int m;
            int n = new int();  //不会这么写，值类型没有实例，所谓的“实例”与变量合而为一
            m = 100;
            n = 100;

            StudentTwo stu2;  //分配4个字节，填0
            stu2 = new StudentTwo();    //1个小孩牵着一个气球，类的引用变量和类
            StudentTwo stu3;
            stu3 = stu2;    //2个小孩牵着同一个气球

            StudentTwo stu4;    //局部变量在Stack栈内分配内存
            stu4 = new StudentTwo();    //实例new StudentTwo()在Heap堆内分配内存，局部变量stu4存储的是实例在堆Heap内存中的地址

            StudentThree stu5 = new StudentThree();
            Console.WriteLine(stu5.ID); //成员变量没有赋值，生成后默认值是0
            Console.WriteLine(stu5.Score);

            int o;
            //Console.WriteLine(o);   //局部变量没有赋值，调用后无法通过编译

            const int p = 100;
            Console.WriteLine(p);
            //p = 200;    //常量不能再次赋值 

            int q = 100;
            object obj;
            obj = q; //装箱Boxing，obj是引用类型，在stack栈内存中分配4个字节，存放该对象的地址；object引用的是stack栈内存上的实例q，obj在heap堆内存上新建对象，此实例内容为obj赋值

            int r = (int)obj;   //拆箱Unboxing，指定obj为整型变量，stack栈内存分配4字节，将引用变量obj的值存储到变量r中
            Console.WriteLine(r);

        }
        class Student
        {
            public static int Amount = 0;   //Amount总数，静态成员变量   //有效的修饰符组合public static，类型int，变量名Amount，初始化器=0
            public int Age;
            public string Name;
            public double Add(double a, ref double b, out double c) //值参数变量a，引用参数变量b，输出类型参数变量c
            {
                c = a + b;
                double result = c;  //局部变量result
                return result;
            }
        }

        class StudentTwo  //引用类型 类Class
        {
            uint ID;    //无符号32位整型 //实例字段，结构体变量，值类型变量
            ushort Score;   //无符号16位整数
        }

        class StudentThree
        {
            public uint ID;
            public ushort Score;
        }
    }
}
