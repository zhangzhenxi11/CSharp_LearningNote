using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaticSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!"); //类Console的静态方法WriteLine，静态成员
            Form form = new Form(); //类的引用变量form
            form.Text = "Hello";    //变量form的实例属性Text，非静态成员(实例)
            form.ShowDialog();  //变量form的实例方法ShowDialog
        }
    }
}
