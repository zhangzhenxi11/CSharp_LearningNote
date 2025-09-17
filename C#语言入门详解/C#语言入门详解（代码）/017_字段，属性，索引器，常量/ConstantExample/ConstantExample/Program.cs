using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = int.MaxValue;   //常量int.MaxValue

            Console.WriteLine(WASPEC.WebsiteURL);
        }
        static double GetArea(double r)
        {
            double a = Math.PI * r * r; //常量PI  //为了提高程序可读性和执行效率
            return a;
        }
    }
    class WASPEC
    {
        public const string WebsiteURL = "http://www.waspec.org";   //只读字段
        //public const Building Location = new Building() { Address = "Some Address" };   //报错  //不能用类类型或结构体类型作为常量类型声明
        //public const Building Location = new Building("Some Address");   //报错
        public static readonly Building Location = new Building("Some Address");    //静态只读字段
    }
    class Building
    {
        public Building(string address)
        {
            this.Address = address;
        }

        public string Address { get; set; }
    }
}
