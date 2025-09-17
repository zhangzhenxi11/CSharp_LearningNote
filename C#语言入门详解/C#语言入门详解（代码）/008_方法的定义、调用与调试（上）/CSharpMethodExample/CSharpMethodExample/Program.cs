using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMethodExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            Console.WriteLine(c.GetCirleArea(10));

            double result = Calculator2.GetCirleArea(100);    //类的静态方法
            Console.WriteLine(result);

            double result2 = Calculator2.GetCylinderVolume(3.0, 4.0);   //调用方法时的argument列表要与定义方法时的parameter列表相匹配
            //double result2 = Calculator2.GetCylinderVolume(3.0);  //数量不匹配
            //double result2 = Calculator2.GetCylinderVolume(3.0, "4.0");  //类型不匹配
            Console.WriteLine(result2);

            double x = 3.0;
            double y = 4.0;
            double result4 = Calculator2.GetCylinderVolume(x, y);
            //double result4 = Calculator2.GetCylinderVolume(double x, double y);   //调用方法的实参格式，不要跟声明变量时搞混
            //double result4 = Calculator2.GetCylinderVolume(double 3.0, double 4.0);   //错误
            //double result4 = Calculator2.GetCylinderVolume(double x = 3.0, double y = 4.0);   //错误
            Console.Write(result4);

            double result5 = Calculator3.GetConeVolume(100, 100);
        }
    }

    class Calculator
    {
        public double GetCirleArea(double r)   //计算圆面积
        {
            //return 3.14 * r * r;
            return Math.PI * r * r;
        }

        /*  //没有复用时，3.14如果要修改，需要修改很多处地方
        public double GetCylinderVolume(double r,double h)  //计算圆柱面积
        {
            return 3.14 * r * r * h;
        }

        public double GetConeVolume(double r,double h)  //计算圆锥面积
        {
            return 3.14 * r * r * h / 3;
        }
        */

        public double GetCylinderVolume(double r, double h) //没有static为非静态方法
        {
            return GetCirleArea(r) * h;
        }

        public double GetConeVolume(double r, double h)
        {
            return GetCylinderVolume(r, h) / 3; //分解，从大算法分解成小算法，自顶向下逐步求精，面向过程的方法
        }
    }
    class Calculator2
    {
        public static double GetCirleArea(double r)   //static静态方法隶属于class类
        {
            //return 3.14 * r * r;
            return Math.PI * r * r;
        }

        public static double GetCylinderVolume(double r, double h)
        {
            return GetCirleArea(r) * h;
        }

        public static double GetConeVolume(double r, double h)
        {
            return GetCylinderVolume(r, h) / 3;
        }
    }

    class Calculator3
    {
        public static double GetCirleArea(double r)   //static静态方法隶属于class类
        {
            return Math.PI * r * r;
        }

        public static double GetCylinderVolume(double r, double h)
        {
            double a = GetCirleArea(r);
            return a * h;
        }

        public static double GetConeVolume(double r, double h)
        {
            double cv = GetCylinderVolume(r, h);
            return cv / 3;
        }
    }

}
