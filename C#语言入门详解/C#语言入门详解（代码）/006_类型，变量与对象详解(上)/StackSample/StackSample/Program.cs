using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackSample
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    BadGuy bg = new BadGuy();
        //    bg.BadMethod();
        //    }
        //}
        //static unsafe Main(string[] args)
        //{
        //    int* p = stackalloc int[9999999];
        //}
        static void Main(string[] args)
        {
            //BadGuy bg = new BadGuy();
            //bg.BadMethod();
            unsafe
            {
                int* p = stackalloc int[9999999];
            }
        }
        class BadGuy
        {
            public void BadMethod()
            {
                int x = 100;
                this.BadMethod();
            }
        }
    }
}
