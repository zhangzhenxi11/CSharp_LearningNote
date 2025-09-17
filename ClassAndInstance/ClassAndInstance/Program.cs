using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassAndInstance
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Form myform; //引用变量
            myform = new Form();
            myform.Text = "My Form";
            myform.ShowDialog();
        }
    }
}
/*
 引用变量和实例的关系：孩子与气球的关系
 
 
 */