using System;
using MyLib.MyNamespace;

namespace HelloClass {
    internal class Program {
        static void Main(string[] args) {
            MyLib.MyNamespace.Calculator cal = new MyLib.MyNamespace.Calculator();
            double res = cal.Add(1.1, 2.2);
            Console.WriteLine(res);
        }
    }
}
