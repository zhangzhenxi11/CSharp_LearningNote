using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Type myType = typeof(Form);
            Console.WriteLine(myType.Name); //打印Form这个类的类型
            Console.WriteLine(myType.FullName); //打印Form这个类的全名
            Console.WriteLine(myType.BaseType.FullName);    //打印Form这个类的父类的全名
            Console.WriteLine(myType.BaseType.BaseType.FullName);   //打印Form这个类的父类的父类的全名

            PropertyInfo[] pInfos = myType.GetProperties(); 
            foreach(var p in pInfos)
            {
                Console.WriteLine(p.Name);
            }

            Console.WriteLine("==========");

            MethodInfo[] mInfos = myType.GetMethods();
            foreach (var p in mInfos)
            {
                Console.WriteLine(p.Name);
            }

            double result = 3.0 / 4.0;
            Console.WriteLine(result);
            result = 3 / 4;
            Console.WriteLine(result);
        }
    }
}
