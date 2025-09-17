using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloStruct {
    internal class Program {
        static void Main(string[] args) {
            Student student = new Student() { ID = 101, Name = "Timothy" }; //局部变量student分配在Main函数的函数栈上，student所关联的栈上内存存储的是Student类型实例
            object obj = student;   //装箱，将Student类型实例复制到内存堆里，obj变量引用的是堆内存里的Student类型实例
            Student student2 = (Student)obj;    //拆箱，强制类型转换
            Console.WriteLine($"#{student2.ID} Name:{student2.Name}");
            Console.WriteLine("=====");


            //经典面试题
            Student stu1 = new Student() { ID = 101, Name = "Timothy" };
            Student stu2 = stu1;    //值类型赋值时，复制的是一个完整的对象    //引用类型变量之间赋值时，复制的是同一个变量的引用
            stu2.ID = 1001;
            stu2.Name = "Michael";
            Console.WriteLine($"#{stu1.ID} Name:{stu1.Name}");  //stu1和stu2是两个独立的对象
            Console.WriteLine($"#{stu2.ID} Name:{stu2.Name}");
            Console.WriteLine("=====");


            //结构体可以实现接口
            stu2.Speak();
            Console.WriteLine("=====");

            //结构体不能有显式无参构造器
            Student stu3 = new Student(1, "Timothy");
            stu3.Speak();
            Console.WriteLine("=====");
        }
    }
    interface ISpeak {
        void Speak();
    }
    struct Student : ISpeak {//结构体类型是值类型，值类型的特点是与值类型变量相关联的内存里存储的是值类型的实例 //结构体可以实现接口
        /*
        public Student(){//报错，不能有显式无参构造器
        }
        */
        public Student(int id,string name)  //结构体可以有显式有参数构造体
        {
            this.ID = id;
            this.Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public void Speak() {
            Console.WriteLine($"I'm #{this.ID} student {this.Name}");
        }

        //结构体没有基类和基结构体
        /*
        struct SuperStudent : Student {//报错，不能派生自类/结构体
        }
        */
    }
}
