using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatementsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string input = Console.ReadLine();
            string input = "65";
            try
            {
                double score = double.Parse(input);
                if (score >= 60)
                {
                    Console.WriteLine("Pass!");
                }
                else
                {
                    Console.WriteLine("Failed");
                }
            }
            catch
            {
                Console.WriteLine("Not a number!");
            }
            Console.WriteLine("==========");


            if (5 > 3)
            {
                Console.WriteLine("Hello, World!");
            }

            bool result = 5 > 3;
            if (result)
            {
                Console.WriteLine("Hello, World!"); //嵌入式语句
            }
            Console.WriteLine("==========");


            int score1 = 90;
            if (score1 >= 60)
                if (score1 >= 85)
                    Console.WriteLine("Best!");
                else
                    Console.WriteLine("Good!");
            else
                Console.WriteLine("Failed!");
            Console.WriteLine("==========");


            var x = 100;
            Console.WriteLine(x.GetType().FullName);
            Console.WriteLine("==========");


            int x1 = 100;   //声明后调用初始化器
            int y1; //声明后没有初始化
            y1 = 10;    //赋值

            int x2, y2, z2;
            x2 = 100;
            y2 = 100;
            z2 = x2 + y2;

            int x3 = 100, y3 = 200, z3 = 300;

            int x4 = 3 + 5;

            int[] myArray = { 1, 2, 3 };
            Console.WriteLine(myArray[1]);
            Console.WriteLine("==========");


            const int x5 = 100; //声明常量


            Console.WriteLine("Hello, World!"); //方法调用invocation表达式


            new Form(); //对象object创建creation表达式


            int x6;
            x6 = 100;   //赋值语句assignment 
            x6++;   //后置post/前置pre自增increment/自减decrement表达式
            x6--;
            x6--;
            ++x6;
            --x6;


            int x7;
            x7 = 100;    //表达式有计算出来的值会被丢弃
            x7++;
            Add(3.0, 4.0);
            Add2(3.0, 4.0);

            int x8 = 100;
            int y8 = 200;
            //x8 + y8;  //C#语言中编译报错
            //x8;
            //y8;
            //100;

        }
        static double Add(double a,double b)
        {
            return a + b;
        }
        static double Add2(double a, double b)
        {
            double result = a + b;  //一个方法尽量只做一个事情
            Console.Write("Result is {0}.", result);
            return result;
        }
    }
}
