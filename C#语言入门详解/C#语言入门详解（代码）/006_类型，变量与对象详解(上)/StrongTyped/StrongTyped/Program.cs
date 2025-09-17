using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrongTyped
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x;
            x = 100;    //整型int，4字节 8bit位
            long y;
            y = 100L;   //长整型long，8字节，64bit位
            x = 100L;   //强类型语言，保护数据安全性

            bool b;
            b = true;
            b = false;
            b = 100;

            if (x = 100)
            {
                Console.WriteLine("It's OK!");
            }
        }
    }
}
