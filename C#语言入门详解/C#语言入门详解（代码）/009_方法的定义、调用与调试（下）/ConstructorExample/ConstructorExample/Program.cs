using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student stu = new Student();    //圆括号()调用Student类的构造器
            Console.WriteLine(stu.ID);
            Console.WriteLine(stu.Name);
            Console.WriteLine(stu.Name == null);

            Console.WriteLine("==========");

            StudentTwo stu2 = new StudentTwo();    //圆括号()调用Student类的构造器
            Console.WriteLine(stu2.ID);
            Console.WriteLine(stu2.Name);

            Console.WriteLine("==========");

            StudentTwo stu3 = new StudentTwo(2, "Mr.Okay");
            Console.WriteLine(stu3.ID);
            Console.WriteLine(stu3.Name);
        }
        class Student
        {
            public int ID;  //没有赋值，默认构造器进行初始化赋值
            public string Name;
        }
        class StudentTwo
        {
            public StudentTwo()    //创建一个不带参数的自定义构造器
            {
                this.ID = 1;
                this.Name = "No name";
            }

            public StudentTwo(int intId, string intName)   //创建一个带参数的自定义构造器 //构造器的重载
            {
                this.ID = intId;
                this.Name = intName;
            }

            public int ID;  //没有赋值，默认构造器进行初始化赋值
            public string Name;
        }
    }
}
