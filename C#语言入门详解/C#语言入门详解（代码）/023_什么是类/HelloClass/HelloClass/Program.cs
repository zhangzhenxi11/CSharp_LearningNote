using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloClass {
    internal class Program {
        static void Main(string[] args) {
            Student stu;    //Student stu声明变量stu    //类是自定义的引用类型
            stu = new Student();    //new Student创建实例，默认构造器()   //使用类创建实例，作为实例的模板
            Console.WriteLine(stu.ID);
            Console.WriteLine(stu.Name);
            Console.WriteLine("=====");

            Student stu2 = new Student() { ID = 1, Name = "Timothy" };   //使用初始化器
            Console.WriteLine(stu2.ID);
            Console.WriteLine(stu2.Name);
            Console.WriteLine("=====");

            Student stu3 = new Student(1, "Timothy");   //使用实例构造器
            Console.WriteLine(stu3.ID);
            Console.WriteLine(stu3.Name);
            stu3.Report();
            Console.WriteLine("=====");

            Type t = typeof(Student);   //不使用new创建实例，使用反射Reflection创建实例 //将Student的类型存储在类型Type类里面
            object o = Activator.CreateInstance(t);
            o = Activator.CreateInstance(t, 1, "Timothy");
            Console.WriteLine(o.GetType().Name);
            Console.WriteLine(o is Student);
            //Student stu4 = (Student)o;
            Student stu4 = o as Student;    //等同于Student stu4 = (Student)o;
            Console.WriteLine(stu4.Name);
            Console.WriteLine("=====");

            Type t2 = typeof(Student);  //使用dynamic动态编程创建实例
            dynamic stu5 = Activator.CreateInstance(t, 1, "Timothy");
            Console.WriteLine(stu5.Name);
            Console.WriteLine("=====");

            Student st6 = new Student(2, "Jacky");
            Console.WriteLine(Student.Amount);
        }
    }
    class Student_ {//类是抽象的结果
    }

    class Student {//代表现实世界中的“种类”，学生们
        public static int Amount { get; set; }  //静态成员Amount总数
        static Student() {//静态构造器
            Amount = 100;
        }
        public Student() {//默认实例构造器
            Amount++;
        }
        public Student(int id, string name) {//自定义实例构造器
            this.ID = id;
            this.Name = name;
            Amount++;
        }
        //~Student() {//析构器，
        //    Console.WriteLine("Bye bye!Release the system resources...");
        //    Amount--;
        //}

        //类是抽象的数据结构
        //类是抽象的数据载体，structure
        public int ID { get; set; }//声明类的属性
        public string Name { get; set; }
        public void Report() {//声明类的方法
            //Console.WriteLine($"I'm #{this.ID} student, my name is {this.Name}.");
            Console.WriteLine($"I'm #{ID} student, my name is {Name}.");
        }
    }
}
