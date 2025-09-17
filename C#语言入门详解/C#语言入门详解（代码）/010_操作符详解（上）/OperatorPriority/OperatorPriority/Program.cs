using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorPriority
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x1;
            x1 = 3 + 4 + 5;  //3+4，7+5，12

            int x = 100;
            int y = 200;
            x = x + y;
            x += y;
            Console.WriteLine(x);
            Console.WriteLine("==========");

            int x2 = 100;
            int y2 = 200;
            int z2 = 300;
            x2 += y2 += z2;
            Console.WriteLine(x2);
            Console.WriteLine(y2);
            Console.WriteLine(z2);

            //System.IO.File.Create("D:\\HelloWorld.txt");    //访问外层名称空间的子集名称空间System.IO，访问名称空间的类成员System.IO.File，访问类的静态成员System.IO.File.Create("")

            Form myForm = new Form();
            myForm.Text = "Hello, World!";  //访问myForm引用的对象的实例成员myForm.Text
            myForm.ShowDialog();    //访问myForm的方法
        }
    }
}
