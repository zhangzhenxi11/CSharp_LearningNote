using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            c.PrintXto1(10);

            int result = c.SumForm1ToX(100);
            Console.WriteLine(result);
        }
    }
    class Calculator
    {
        //public void PrintXto1(int x)    //打印10到1
        //{
        //    for (int i = x; i > 0; i--)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}

        public void PrintXto1(int x)    //打印10到1
        {
            if (x == 1)
            {
                Console.WriteLine(x);
            }
            else
            {
                Console.WriteLine(x);
                PrintXto1(x - 1);
            }

        }

        //public int SumForm1ToX(int x) //for循环，1~100相加，占用内存较少
        //{
        //    int result = 0;
        //    for (int i = 1; i < x + 1; i++)
        //    {
        //        result = result + i;
        //    }
        //    return result;
        //}
        //public int SumForm1ToX(int x) //递归，占用内存较多
        //{
        //    if (x == 1)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        int result = x + SumForm1ToX(x - 1);
        //        return result;
        //    }
        //}
        public int SumForm1ToX(int x)   //梯形面积，最快，算法复杂度低
        {
            return (1 + x) * x / 2;
        }
    }
}
