using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi
{
    internal class Program
    {
        public const int max_Value = 30;
        public static int steps = 0;
        static void Main(string[] args)
        {
        Retry:
            steps = 0;
            int levels = 0;
            Console.WriteLine($"输入汉诺塔层数（1~{max_Value})：");  //30层，一共移动了1073741823次。耗时 = 7279.3336ms
            levels = int.Parse(Console.ReadLine());
            DateTime start = DateTime.Now;
            if (levels > 0 && levels <= max_Value)
            {
                Move(levels, 'A', 'B', 'C');
                Console.WriteLine($"一共移动了{Program.steps}次。");
                DateTime end = DateTime.Now;
                TimeSpan ts = end - start;
                Console.WriteLine($"耗时={ts.TotalMilliseconds.ToString()}\n");
                //Console.ReadKey();
                goto Retry;
            }
            Console.WriteLine($"输入范围错误\n");
            //Console.ReadKey();
            goto Retry;
        }
        static void Move(int pile, char src, char tmp, char dst)
        {
            //Console.WriteLine($"{pile}_{src}{tmp}{dst}");
            if (pile == 1)
            {
                steps++;
                Console.WriteLine($"step{steps}，{src} -> {dst}");
                return;
            }
            Move(pile - 1, src, dst, tmp);
            Move(1, src, tmp, dst);
            Move(pile - 1, tmp, src, dst);
        }
    }
}
