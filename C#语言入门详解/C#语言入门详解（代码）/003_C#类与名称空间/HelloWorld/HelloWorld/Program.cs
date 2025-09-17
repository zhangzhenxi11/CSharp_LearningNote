//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using SuperCalculate;
using System;
using System.Windows.Forms;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //名称空间引用，全称
            Console.WriteLine("Hello, World!");
            System.Console.WriteLine("Hello, World!");
            System.Console.WriteLine("Good morning!");
            Console.WriteLine("Good evening!");

            //引用System.Windows.Forms
            Form form = new Form();
            System.Windows.Window window = new System.Windows.Window();

            //引用PresentationFramework、PresentationCore、WindowsBase，
            //form.ShowDialog();

            //使用NuGet管理器引用EntityFramework

            //黑盒引用：SuperCalculate.dll
            //白盒引用：跨解决方案添加SuperCalculate.csproj并引用SuperCalculate
            //白盒引用：解决方案内新建类库SuperCalculate并引用SuperCalculate
            double result1 = Calculate.Mul(3, 4);
            Console.WriteLine(result1);
            double result2 = Calculate.Div(3, 0);
            Console.WriteLine(result2);

            //输入错误示例
            //Console.WriteLine('Hello, World!");   单引号
            //Console.WriteLine("Hello, World!")；   中文字符；
            //Console.WriteLine("Hello, World!")；   全角英文字符；
            //console.WriteLine(result2);   类名称首字母小写
        }
    }
}
