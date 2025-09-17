using System;
using System.Collections.Generic;
using System.Linq;  //Linq
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ParametersExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student stu = new Student();//传值参数->值类型
            int y = 100;
            stu.AddOne(y);  //stu.AddOne(y)值类型的传值参数在方法内的运算不影响外部变量y的值
            Console.WriteLine(y);   //方法外面的值参数没有改变
            Console.WriteLine("=====");

            Student2 stu2 = new Student2() { Name = "Tim" };    //传值参数 -> 引用类型，并且新创建对象
            SomeMethod(stu2);   //Ctrl + R + R 批量重命名变量
            Console.WriteLine(stu2.Name);   //方法外面引用的实例并没有改变
            Console.WriteLine("{0},{1}", stu2.GetHashCode(), stu2.Name);
            Console.WriteLine("=====");

            Student2 stu3 = new Student2() { Name = "Tim" };    //传值参数 -> 引用类型，只操作对象，不创建新对象
            UpdateObject(stu3); //变量stu3和传值参数stu指向的内存地址不一样，但两个内存地址里存储相同的内存地址（实例在堆内存中的地址）
            Console.WriteLine("HashCode={0},Name={1}", stu3.GetHashCode(), stu3.Name);
            Console.WriteLine("==========");


            int y2 = 1; //引用参数->值类型
            IWantSideEffect(ref y2);    //方法外变量y2和方法参数x指向同一个内存地址，方法内改变参数x内存地址的值，读取方法外变量y2的数值是最新的值
            Console.WriteLine(y2);
            Console.WriteLine("=====");

            Student2 outterStu = new Student2() { Name = "Tim" };   //引用参数 -> 引用类型，创建新对象
            Console.WriteLine("HashCode={0},Name={1}", outterStu.GetHashCode(), outterStu.Name);
            Console.WriteLine("-----");
            IWantSideEffect(ref outterStu);
            Console.WriteLine("HashCode={0},Name={1}", outterStu.GetHashCode(), outterStu.Name);
            Console.WriteLine("=====");

            Student2 outterStu2 = new Student2() { Name = "Tim" };  //引用参数 -> 引用类型，不创建新对象只改变对象
            Console.WriteLine("HashCode={0},Name={1}", outterStu2.GetHashCode(), outterStu2.Name);
            Console.WriteLine("-----");
            SomeSideEffect(ref outterStu2); //变量outterStu2和引用参数stu指向同一个内存地址，这个内存地址存储的是对象在堆内存中的地址
            Console.WriteLine("HashCode={0},Name={1}", outterStu2.GetHashCode(), outterStu2.Name);
            Console.WriteLine("==========");


            Console.WriteLine("Please input first number:");
            string arg1 = Console.ReadLine();
            double x3 = 0;
            bool b1 = double.TryParse(arg1, out x3);    //输出参数 ->  值类型
            if (b1 == false)
            {
                Console.WriteLine("Input error!");
                return;
            }

            Console.WriteLine("Please input second number:");
            string arg2 = Console.ReadLine();
            double y3 = 0;
            bool b2 = double.TryParse(arg2, out y3);
            if (b2 == false)
            {
                Console.WriteLine("Input error!");
                return;
            }

            double z3 = x3 + y3;
            Console.WriteLine("{0}+{1}+{2}", x3, y3, z3);
            Console.WriteLine("==========");


            double x4 = 100;
            bool b3 = DoubleParser.TryParse("ABC", out x4);      //输出参数 ->  引用类型
            if (b3 == true)
            {
                Console.WriteLine(x4 + 1);
            }
            else
            {
                Console.WriteLine(x4);
            }
            Console.WriteLine("==========");


            Student3 stu4 = null;
            bool b4 = StudentFactory.Create("Tim", 34, out stu4);   //引用类型的输出参数
            if (b4 == true)
            {
                Console.WriteLine("Student {0}, age is {1}", stu4.Name, stu4.Age);
            }
            Console.WriteLine("==========");


            int[] myIntArray = new int[] { 1, 2, 3 };
            int result = CalculateSum(myIntArray);
            result = CalculateSum(1, 2, 3); //数组参数
            Console.WriteLine(result);
            Console.WriteLine("=====");

            int x5 = 100;
            int y5 = 200;
            int z5 = x5 + y5;
            Console.WriteLine("{0}+{1}={2}", x5, y5, z5);   //public static void WriteLine(string format, params object[] arg)
            Console.WriteLine("=====");

            string str = "Tim;Tom,Amy.Lisa";
            string[] result2 = str.Split(';', ',', '.');
            foreach (var name in result2)    //迭代 数组
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("==========");


            PrintInfo("Tim", 34);
            PrintInfo(name: "Tim", age: 34);    //具名参数，参数的位置不再受约束
            PrintInfo(age: 34, name: "Tim");
            Console.WriteLine("=====");

            PrintInfo2();
            Console.WriteLine("==========");


            double x6 = 3.14159;
            double y6 = Math.Round(x6, 4);
            y6 = x6.Round(4);   //扩展参数，扩展方法Round()
            Console.WriteLine(y6);
            Console.WriteLine("==========");


            List<int> myList = new List<int>() { 11, 12, 13, 14, 15 };
            bool result3 = AllGreaterThanTen(myList);
            result3 = myList.All(i => i > 10);  //Linq 扩展方法All()
            Console.WriteLine(result3);
            Console.WriteLine("==========");
        }
        static void SomeMethod(Student2 stu)    //静态方法SomeMethod，引用类型的传值参数stu
        {
            stu = new Student2() { Name = "Tom" };
            Console.WriteLine(stu.Name);
            Console.WriteLine("{0},{1}", stu.GetHashCode(), stu.Name);
        }
        static void UpdateObject(Student2 stu)
        {
            stu.Name = "Tom";   //通过传进来的参数修改其引用对象的值，修改对象引用的值，某方法的副作用side-effect
            Console.WriteLine("HashCode={0},Name={1}", stu.GetHashCode(), stu.Name);
        }
        static void IWantSideEffect(ref int x)
        {
            x = x + 100;
        }
        static void IWantSideEffect(ref Student2 stu)
        {
            stu = new Student2() { Name = "Tom" };
            Console.WriteLine("HashCode={0},Name={1}", stu.GetHashCode(), stu.Name);
        }
        static void SomeSideEffect(ref Student2 stu)
        {
            stu.Name = "Tom";
            Console.WriteLine("HashCode={0},Name={1}", stu.GetHashCode(), stu.Name);
        }
        static int CalculateSum(params int[] intArray)  //数组参数
        {
            int sum = 0;
            foreach (var item in intArray)
            {
                sum += item;
            }
            return sum;
        }
        static void PrintInfo(string name, int age)
        {
            Console.WriteLine("Hello {0},you are {1}.", name, age);
        }
        static void PrintInfo2(string name = "Tim", int age = 34)   //可选参数
        {
            Console.WriteLine("Hello {0},you are {1}.", name, age);
        }
        static bool AllGreaterThanTen(List<int> intList)
        {
            foreach (var item in intList)
            {
                if (item <= 10)
                {
                    return false;
                }
            }
            return true;
        }
    }
    class Student
    {
        public void AddOne(int x)   //值类型的传值参数x，不带修饰符
        {
            x = x + 1;
            Console.WriteLine(x);
        }
    }
    class Student2  //类是典型的引用类型
    {
        public string Name { get; set; }
    }

    class DoubleParser
    {
        public static bool TryParse(string input, out double result)  //带有输出参数的方法
        {
            try
            {
                result = double.Parse(input);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }
    }
    class Student3
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }
    class StudentFactory
    {
        public static bool Create(string stuName, int stuAge, out Student3 result)
        {
            result = null;
            if (string.IsNullOrEmpty(stuName))
            {
                return false;
            }
            if (stuAge < 20 || stuAge > 80)
            {
                return false;
            }
            result = new Student3() { Name = stuName, Age = stuAge };
            return true;
        }
    }
    static class DoubleExtension    //静态类
    {
        public static double Round(this double input, int digits)    //扩展方法this double input
        {
            double result = Math.Round(input, digits);
            return result;
        }
    }
}


