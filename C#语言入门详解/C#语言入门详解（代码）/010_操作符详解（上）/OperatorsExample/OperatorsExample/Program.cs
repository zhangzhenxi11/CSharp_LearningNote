using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            double x = c.Add(3.0, 5.0); //c.Add的.为成员访问操作符
            Console.WriteLine(x);   //WriteLine(x)的()为方法调用操作符

            Console.Write("==========\n");

            //c.PrintHello;
            c.PrintHello();

            Console.Write("==========\n");

            Action myAction = new Action(c.PrintHello); //委托对象的方法(成员)，void并且无参数，c.PrintHello后不带()，
            myAction(); //调用myAction管理的方法

            int[] myIntArray = new int[10]; //int[10]的[]为元素访问操作符，访问数组类型对象，创建int数组对象，数组长度10
            int[] myIntArray2 = new int[] { 1, 2, 3, 4, 5 }; //创建数组实例，花括号{}初始化器
            //int[] myIntArray3 = new int[4] { 1, 2, 3, 4, 5 }; //报错
            //int[] myIntArray4 = new int[6] { 1, 2, 3, 4, 5 }; //报错
            int[] myIntArray5 = new int[5] { 1, 2, 3, 4, 5 }; //初始化器[]输入数量，对应初始化的int元素数量
            Console.WriteLine(myIntArray5[0]);  //访问数组元素，0为第一个，总数-1为最后一个
            Console.WriteLine(myIntArray5[4]);
            Console.WriteLine(myIntArray5[myIntArray2.Length - 1]);
            //Console.WriteLine(myIntArray5[5]);  //访问数组元素超边界

            Console.Write("==========\n");

            Dictionary<string, Student> stuDic = new Dictionary<string, Student>();   //泛型类<>，字典Dictionary，用Student往字典里灌数据
            for (int i = 1; i <= 100; i++)
            {
                Student stu = new Student();
                stu.Name = "s_" + i.ToString();
                stu.Score = 100 + i;
                stuDic.Add(stu.Name, stu);  //增加索引值stu.Name和对象stu
            }

            Student number6 = stuDic["s_6"];
            Console.WriteLine(number6.Score);   //查询s_6的成绩

            Console.Write("==========\n");

            int x1 = 100;
            x1++;
            //x1 = x1 + 1;
            Console.WriteLine(x1);
            x1--;
            //x1 = x1 - 1;
            Console.WriteLine(x1);
            int y1 = x1++;  //先赋值=再执行++或--
            //y1 = x1; 
            //x1 = x1 + 1;
            Console.WriteLine(x1);
            Console.WriteLine(y1);

            Console.Write("==========\n");

            Type t = typeof(int);   //使用typeof查看Int
            Console.WriteLine(t.Namespace); //Int的名称空间
            Console.WriteLine(t.FullName);  //Int的全名
            Console.WriteLine(t.Name);  //Int的名称（不带名称空间）

            int c1 = t.GetMethods().Length; //获得数组长度
            foreach (var mi in t.GetMethods())
            {
                Console.WriteLine(mi.Name);
            }
            Console.WriteLine(c1);

            Console.Write("==========\n");

            int x2 = default(int);  //使用default查看类型的内部结构，int是结构体类型
            Console.WriteLine(x2);

            double x3 = default(double);
            Console.WriteLine(x3);

            Form myForm = default(Form);    //Form是引用类型
            Console.WriteLine(myForm == null);

            Level level = default(Level);   //查看自定义的枚举类型Level
            Console.WriteLine(level);
        }
    }
    class Calculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }
        public void PrintHello()
        {
            Console.WriteLine("Hello");
        }
    }
    class Student
    {
        public string Name;
        public int Score;
    }
    enum Level  //枚举类型
    {
        /*
        Medium, //无赋值，默认第一个，0开始
        Low,
        High
        */

        Medium = 1,
        Low = 0,
        High = 2

        /*
        Medium = 1, //出错
        Low = 3,
        High = 2
        */
    }
}
