using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateMemberExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Student stu1 = new Student();
            stu1.Age = 40;
            stu1.Score = 90;

            Student stu2 = new Student();
            stu2.Age = 24;
            stu2.Score = 60;

            Student.ReportAmount();
            Console.WriteLine("=====");
            */


            List<Student> stuList = new List<Student>();
            for (int i = 0; i < 100; i++)
            {
                Student stu = new Student(i);
                stu.Age = 24;
                stu.Score = i;
                stuList.Add(stu);
            }
            //Student.ReportAmount();

            int totalAge = 0;
            int totalScore = 0;
            foreach (var stu in stuList)
            {
                totalAge += stu.Age;
                totalScore += stu.Score;
            }
            Student.AverageAge = totalAge / Student.Amount;
            Student.AverageScore = totalScore / Student.Amount;

            Student.ReportAmount();
            Student.ReportAverageAge();
            Student.ReportAverageScore();
            Console.WriteLine("=====");


            Student stu2 = new Student(1);
            Console.WriteLine(stu2.ID);
            //stu2.ID = 2;  //赋值报错，只读字段
            Console.WriteLine("==========");


            Console.WriteLine(Brush.DefaultColor.Red);
            Console.WriteLine(Brush.DefaultColor.Green);
            Console.WriteLine(Brush.DefaultColor.Blue);
            Console.WriteLine("==========");

        }
    }
    class Student
    {
        public readonly int ID;  //实例只读字段
        public int Age = 0; //实例字段，实例字段初始化的时机——对象创建时
        public int Score;   //无显式初始化时，int默认0，字段获得其类型的默认值，所以字段“永远都不会未被初始化”

        public static int AverageAge;   //静态字段，静态字段初始化的时机——类型被加载(load)时，只执行一次
        public static int AverageScore;
        public static int Amount = 0;

        public Student(int id)
        {
            this.ID = id;   //实例只读字段
            Student.Amount++;
        }

        static Student()   //static静态构造器
        {
            Student.AverageScore = 0;   //静态只读字段，此处与public static int AverageScore = 0;实现效果一致
        }
        public static void ReportAmount()
        {
            Console.WriteLine(Student.Amount);
        }
        public static void ReportAverageAge()
        {
            Console.WriteLine(Student.AverageAge);
        }
        public static void ReportAverageScore()
        {
            Console.WriteLine(Student.AverageScore);
        }
    }
    struct Color
    {
        public int Red;
        public int Green;
        public int Blue;
    }
    class Brush
    {
        public static readonly Color DefaultColor = new Color() { Red = 0, Green = 0, Blue = 0 };   //类的静态只读字段
    }
    class Brush2
    {
        public static readonly Color DefaultColor;  //静态只读字段
        static Brush2() //静态构造函数，加载时，只执行一次
        {
            Brush2.DefaultColor = new Color() { Red = 0, Green = 0, Blue = 0 };     //静态只读字段，静态构造器初始化，与class Brush效果一致
        }
    }
}
