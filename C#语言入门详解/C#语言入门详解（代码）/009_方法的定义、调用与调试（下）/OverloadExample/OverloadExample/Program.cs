using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            Console.WriteLine(100);
            Console.WriteLine(200L);
            Console.WriteLine(300D);

            Calculator c = new Calculator();
            int x = c.Add(100, 100);
            Console.WriteLine(x);

            double y = c.Add(100D, 200D);
            //double y = c.Add("100", 200D);    //重装报错
            //double y = c.Add(100);    //重装报错
            Console.WriteLine(y);


        }
        class Calculator
        {
            public int Add(int a, int b)
            {
                return a + b;
            }

            /*
            public double Add(int a,int b)  //参数的类型和方法的类型不一致
            {
                return a + b;
            }

            public double Add(int x, int y) //参数的类型和方法的类型不一致
            {
                return x + y;
            }
            */
            public double Add(double x, double y)
            {
                return x + y;
            }

            public int Add(int a, int b, int c) //参数传值
            {
                return a + b + c;
            }
            public int Add<T>(int a, int b) //类型形参T
            {
                T t;
                return a + b;
            }
            public int Add(ref int a, int b) //引用ref
            {
                return a + b;
            }
            public int Add(int a, out int b) //输出out
            {
                b = 100;
                return a + b;
            }
        }
    }
}
