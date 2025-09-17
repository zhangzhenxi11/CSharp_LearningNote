using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace StatementExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int score = 0;
            bool canContinue = true;
            while (canContinue) //while循环
            {
                Console.WriteLine("Please input first number:");
                string str1 = Console.ReadLine();
                int x = int.Parse(str1);

                Console.WriteLine("Please input second number:");
                string str2 = Console.ReadLine();
                int y = int.Parse(str2);

                int sum = x + y;
                if (sum == 100)
                {
                    score++;
                    Console.WriteLine("Correct!{0}+{1}={2}", x, y, sum);
                }
                else
                {
                    Console.WriteLine("Error!{0}+{1}!={2}", x, y, 100);
                    canContinue = false;
                }
            }
            Console.WriteLine("Your score is {0}.", score);
            Console.WriteLine("Game Over!");
            Console.WriteLine("=====");


            int score1 = 0;
            int sum1 = -1;
            int x1;
            int y1;
            do //do while循环
            {
                Console.WriteLine("Please input first number:");
                string str1 = Console.ReadLine();
                if (str1.ToString().ToLower() == "end")
                {
                    break;
                }
                //int x1 = int.Parse(str1);
                try
                {
                    x1 = int.Parse(str1);
                }
                catch
                {
                    Console.WriteLine("First number has problem!Restart.");
                    continue;
                }

                Console.WriteLine("Please input second number:");
                string str2 = Console.ReadLine();
                if (str2.ToString().ToLower() == "end")
                {
                    break;
                }
                //int y1 = int.Parse(str2);
                try
                {
                    y1 = int.Parse(str2);
                }
                catch
                {
                    Console.WriteLine("Second number has problem!Restart.");
                    continue;
                }

                sum1 = x1 + y1;
                if (sum1 == 100)
                {
                    score++;
                    Console.WriteLine("Correct!{0}+{1}={2}", x1, y1, sum1);
                }
                else
                {
                    Console.WriteLine("Error!{0}+{1}!={2}", x1, y1, 100);
                }
            }
            while (sum1 == 100 || sum1 == -1);

            Console.WriteLine("Your score is {0}.", score);
            Console.WriteLine("Game Over!");
            Console.WriteLine("==========");


            int counter = 0;
            while (counter < 10)
            {
                Console.WriteLine("Hello, World!");
                counter++;
            }
            Console.WriteLine("=====");

            int counter2 = 0;
            do
            {
                Console.WriteLine("Hello, World!");
                counter2++;
            } while (counter2 < 10);
            Console.WriteLine("=====");


            for (int counter3 = 0; counter3 < 10; counter3++)
            {
                Console.WriteLine("Hello, World!");
            }
            Console.WriteLine("=====");

            int counter4 = 0;   //不会这么写，降低可读性
            for (counter4 = 0; counter4 < 10; counter4++)
            {
                Console.WriteLine("Hello, World!");
            }
            Console.WriteLine(counter);
            Console.WriteLine("=====");

            /*
			for(; ; )   //无限循环
			{
				Console.WriteLine("Hello, World!");
			}
 
            for(int counter5 = 0; ; counter5++) //无限循环
            {
            Console.WriteLine("Hello, World!");
            }
 
            for (int counter6 = 0; counter6 < 10;) //无限循环
            {
                Console.WriteLine("Hello, World!");
            }
            */


            for (int a = 1; a <= 9; a++)    //九九乘法表
            {
                for (int b = 1; b <= a; b++)
                {
                    Console.Write("{0}*{1}={2}\t", a, b, a * b);  //Console.Write()不换行
                }
                Console.WriteLine();    //换行
            }
            Console.WriteLine("=====");


            for (int a = 1; a <= 9; a++)    //打印*号，直角三角形
            {
                for (int b = 1; b <= a; b++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.WriteLine("=====");


            int i, j;
            int Line = 10;
            for (i = 0; i < Line; i++)    //打印*号，等边三角形
            {
                for (j = 0; j < (Line - i); j++)
                {
                    Console.Write(" ");
                }
                for (j = 0; j < (2 * i + 1); j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.WriteLine("=====");


            int i2, j2;
            int Line2 = 10;
            for (i2 = 0; i2 < Line; i2++)    //打印*号，菱形上部分
            {
                for (j2 = 0; j2 < (Line2 - i2); j2++)
                {
                    Console.Write(" ");
                }
                for (j2 = 0; j2 < (2 * i2 + 1); j2++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            int i3, j3;
            int Line3 = 10;
            for (i3 = (Line3 - 2); i3 >= 0; i3--)    //打印*号，菱形下部分
            {
                for (j3 = 0; j3 < (Line3 - i3); j3++)
                {
                    Console.Write(" ");
                }
                for (j3 = 0; j3 < (2 * i3 + 1); j3++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.WriteLine("==========");


            int[] intArray = new int[] { 1, 2, 3, 4, 5, 6 };    //C#中所有数组类型都是Array这个类作为基类
            Console.WriteLine(intArray.GetType().FullName); //Array类的基接口IEnumerable（大写I开头）
            Console.WriteLine(intArray is Array);

            IEnumerator enumerator = intArray.GetEnumerator();  //指月
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);  //使用迭代器对集合进行遍历
            }
            enumerator.Reset(); //重置迭代器
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            Console.WriteLine("=====");


            List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6 };
            IEnumerator enumerator2 = intList.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                Console.WriteLine(enumerator2.Current);
            }
            Console.WriteLine("=====");


            List<int> intList2 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            foreach (var current in intList2)   //用foreach对集合进行遍历，比用while遍历集合更加方便
            {
                Console.WriteLine(current);
            }
            Console.WriteLine("==========");


            Greeting("Mr.Okay");
            Console.WriteLine("=====");

            var result = WhoIsWho("Mr.Hello");
            Console.WriteLine(result);
            Console.WriteLine("==========");
        }
        static void Greeting(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return; //尽早return，简化
            }
            Console.WriteLine("Hello, {0}!", name);
        }
        static string WhoIsWho(string alias)
        {
            if(alias == "Mr.Okay")
            {
                return "Tim";   //每个选择分支都有return
            }
            else
            {
                return "I don't know!";
            }
        }
    }
}
