using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //关键字using

namespace Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int myVariable = 100;   //驼峰法
            Console.WriteLine(myVariable); //Pascal

            Form @Form = new Form();    //标识符@Form
            @Form.ShowDialog();

            Form _formNo_1 = new Form();    //标定符号;、()
            _formNo_1.ShowDialog();

            int x = 2;  //操作符=
            long y = 3L;
            double z = 4.0D;
            char c = 'a';
            bool b = true;
            bool b2 = false;
            string str = null;

            Form f = null;
            f.ShowDialog();

            //注释
            /*注释*/

            Form        e = new Form(); //Ctrl+K、Ctrl+D，格式化文档格式
        }
    }
}
