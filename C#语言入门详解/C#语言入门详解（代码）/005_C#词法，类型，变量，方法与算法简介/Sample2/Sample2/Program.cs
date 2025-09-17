using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sample2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var x = 3;  //整线Int32
            Console.WriteLine(x.GetType().Name);

            var y = 3L; //长整型Int64
            Console.WriteLine(y.GetType().Name);

            var z = 3.0;    //双精度Double
            //var z = 3.0D;
            Console.WriteLine(z.GetType().Name);

            var w = 3.0F;   //单精度Single
            Console.WriteLine(w.GetType().Name);


            int a;  //类型int 变量a
            //a = 100L;   //赋值类型与变量a不符
            a = 100;    //赋值100给变量a

            double b;
            b = 3.0;

            Calculator c = new Calculator();
            int d = c.Add(2, 3);
            string str = c.Today();
            Console.WriteLine(str);
            c.PrintSun(4, 6);
        }
        class Calculator    //类Calculator
        {
            public int Add(int a, int b)   //方法Add，声明public类外面可以访问，声明函数返回结果的类型int，有数据输入、执行运算、返回数据
            {
                int result = a + b;
                return result;  //返回结果
            }
            public string Today()   //没有数据输入、不执行运算、返回数据
            {
                int Day = DateTime.Now.Day;
                return Day.ToString();
            }
            public void PrintSun(int a,int b)   //有数据输入、执行运算、不返回数据void
            {
                int result = a + b;
            }
        }
    }
}
