using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic myVar = 100;    //动态类型dynamic
            Console.WriteLine(myVar);
            myVar = "Mr.Okay!";
            Console.WriteLine(myVar);
        }
    }
}
