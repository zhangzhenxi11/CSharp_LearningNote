using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassAndInstance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //(new Form()).ShowDialog();

            //(new Form()).Text = "My Form";
            //(new Form()).ShowDialog();

            //Form myform;    //类:Form，引用变量：myform
            //myform = new Form();    //实例：new Form()
            //myform.Text = "My Form!";
            //myform.ShowDialog();

            new Form(); //实例没有被引用，气球没有被小孩牵着
            Form myform1;
            Form myform2;
            myform1 = new Form();
            myform2 = myform1;  //myform1和myform2访问同一个实例，2个小孩牵着一个气球
            //myform1.Text = "My Form!";
            //myform2.ShowDialog();

            myform2.Text = "I Change It!";  
            myform1.ShowDialog();
        }
    }
}
