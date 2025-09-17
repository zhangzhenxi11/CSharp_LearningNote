using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFun
{
    /*
    double Add(double a, double b)  //命名空间不能直接包含字段、方法或语句之类的成员
    {
        return a + b;
    }
    */
    internal class Program  //类Program
    {
        static void Main(string[] args)
        {
            /*
            double Add(double a, double b)  //函数声明不能放到函数体里
            {
                return a + b;
            }
            */
        }

        double Add(double a, double b)  //类的成员，只有作为类（结构体）的成员时才被称为方法
        {
            return a + b;
        }
    }
}
