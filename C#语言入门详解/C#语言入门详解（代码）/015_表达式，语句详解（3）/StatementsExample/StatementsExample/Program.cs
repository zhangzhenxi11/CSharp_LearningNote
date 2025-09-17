using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 100;    //声明语句

            if (x > 100) Console.WriteLine(x);  //嵌入式语句


            { }    //空的块语句  //语句只能存在方法体里

            {
            hello: Console.WriteLine("Hello, World!");
                //goto hello;   //死循环
            }   //编译器把块语句只当一条语句处理
            Console.WriteLine("==========");


            int x1 = 100;
            {
                Console.WriteLine(x1);
                int y1 = 200;
                Console.WriteLine(y1);
            }
            //Console.WriteLine(y1);    //无法调用，离开了变量y1的作用域


            if (5 > 3) Console.WriteLine("Hello");  //if语句
            if (5 > 3)
                Console.WriteLine("Hello");
            Console.WriteLine("==========");

            //if (5 + 3) Console.WriteLine("Hello");    //报错

            int x2 = 200;
            int y2 = 100;
            if (x2 < y2)
                Console.Write("Hello");
            Console.WriteLine("World!");    //if语句没有控制这行
            Console.WriteLine("=====");

            if (x2 > y2)
            {
                Console.Write("Hello");
                Console.WriteLine("World!");
            }
            Console.WriteLine("=====");

            if (x2 > y2)
            {
                Console.WriteLine("Hello");
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("World!");
                Console.WriteLine("No");
            }
            Console.WriteLine("=====");


            int score = 95; //初版
            if (score >= 0 && score <= 100)
            {
                if (score >= 60 && score <= 100)
                {
                    Console.WriteLine("Pass");
                }
                if (score >= 0 && score <= 59)
                {
                    Console.WriteLine("Failed");
                }
            }
            else
            {
                Console.WriteLine("Input Error!");
            }
            Console.WriteLine("=====");


            if (score >= 0 && score <= 100) //优化
            {
                if (score >= 60)
                {
                    Console.WriteLine("Pass");
                }
                else
                {
                    Console.WriteLine("Failed");
                }
            }
            else
            {
                Console.WriteLine("Input Error!");
            }
            Console.WriteLine("=====");


            int score2 = 95;
            if (score2 >= 0 && score <= 100) //新需求，在原版上修改
            {
                if (score2 >= 60)
                {
                    if (score2 >= 80)
                    {
                        Console.WriteLine("A");
                    }
                    else
                    {
                        Console.WriteLine("B");
                    }
                }
                else
                {
                    if (score2 >= 40)
                    {
                        Console.WriteLine("C");
                    }
                    else
                    {
                        Console.WriteLine("D");
                    }
                }
            }
            else
            {
                Console.WriteLine("Input Error!");
            }
            Console.WriteLine("=====");


            int score3 = 95;
            if (score3 >= 80 && score3 <= 100)    //重新设计
            {
                Console.WriteLine("A");
            }
            else
            {
                if (score3 >= 60)
                {
                    Console.WriteLine("B");
                }
                else
                {
                    if (score3 >= 40)
                    {
                        Console.WriteLine("C");
                    }
                    else
                    {
                        if (score3 >= 0)
                        {
                            Console.WriteLine("D");
                        }
                        else
                        {
                            Console.WriteLine("Input Error!");
                        }
                    }
                }
            }
            Console.WriteLine("=====");


            int score4 = 95;
            if (score4 >= 80 && score4 <= 100)    //再优化
            {
                Console.WriteLine("A");
            }
            else if (score4 >= 60)
            {
                Console.WriteLine("B");
            }
            else if (score4 >= 40)
            {
                Console.WriteLine("C");
            }
            else if (score4 >= 0)
            {
                Console.WriteLine("D");
            }
            else
            {
                Console.WriteLine("Input Error!");
            }
            Console.WriteLine("=====");


            //需求 : 80~100 > A; 60~79->B; 40~59 > C; 0~39->D; 其它->Error
            int score5 = 95;
            //double score5 = 95; //双精度浮点数变量score5报错
            int y3 = 10;
            switch (score5 / 10)
            {
                //case score>= 80 && score <= 100:  //case后类型与switch表达式类型不一致
                //case y3;  //变量y3报错，case后只能是常量constant表达式
                case 10:
                    if (score5 == 100)
                    {
                        goto case 9;
                    }
                    else
                    {
                        goto default;
                    }
                case 9: //两个标签做同样事情，只保留最后的break
                case 8:
                    Console.WriteLine("A");
                    break;
                case 7:
                case 6:
                    Console.WriteLine("B");
                    break;
                case 5:
                case 4:
                    Console.WriteLine("C");
                    break;
                case 3:
                case 2:
                case 1:
                case 0:
                    Console.WriteLine("D");
                    break;
                default:
                    Console.WriteLine("Input Error!");
                    break;
            }
            Console.WriteLine("==========");


            //使用enum枚举类型
            Level myLevel = Level.High;
            switch (myLevel)
            {
                case Level.High:
                    Console.WriteLine("High Level!");
                    break;
                case Level.Mid:
                    Console.WriteLine("Mid Level!");
                    break;
                case Level.Low:
                    Console.WriteLine("Low Level!");
                    break;
                default:
                    break;
            }
            Console.WriteLine("==========");


            Calculator c = new Calculator();
            int r = c.Add("100", "200");
            //int r = c.Add("abc", "200");
            //int r = c.Add("9999999999999999999", "200");
            //int r = c.Add(null, "200");

            try
            {
                r = c.Add("999999999999999999999", "200");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine(oe.Message);
            }
            Console.WriteLine(r);
            Console.WriteLine("==========");



        }//方法体
        enum Level
        {
            High,
            Mid,
            Low
        }

        class Calculator
        {
            public int Add(string arg1, string arg2)    //try语句
            {
                int a = 0;
                int b = 0;
                bool hasError = false;

                try
                {
                    a = int.Parse(arg1);
                    b = int.Parse(arg2);
                }
                catch (ArgumentException ane)    //精细捕抓异常
                {
                    hasError = true;
                    Console.WriteLine("Your argument(s) are null!");
                    Console.WriteLine(ane.Message);
                }
                catch (FormatException fe)
                {
                    hasError = true;
                    Console.WriteLine("Your argument(s) are not number!");
                    Console.WriteLine(fe.Message);
                }
                //catch (OverflowException oe)
                catch (OverflowException)
                {
                    hasError = true;
                    throw; //异常抛给调用者
                    //throw oe; 
                    Console.WriteLine("Out of range!");
                    Console.WriteLine(oe.Message);
                }
                catch (Exception e)   //粗放捕抓异常
                {
                    hasError = true;
                    Console.WriteLine("Your argument(s) have error!");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (hasError)
                    {
                        Console.WriteLine("Excecution has error!");
                    }
                    else
                    {
                        Console.WriteLine("Done!");
                    }
                }

                int result = a + b;
                return result;
            }
        }
    }//类体
}//名字空间体
