using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double result = GetCylinderVolume(10, 100);
            Console.WriteLine(result);
        }
        static double GetCylinderVolume(double r, double h)
        {
            double area = 3.1416 * r * r;
            double volume = area * h;
            return volume;
        }
    }
}
