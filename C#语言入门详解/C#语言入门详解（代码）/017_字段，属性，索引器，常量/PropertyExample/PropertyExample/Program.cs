using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student();
            stu1.Age = 20;

            Student stu2 = new Student();
            stu2.Age = 20;

            Student stu3 = new Student();
            stu3.Age = 200; //字段值被污染

            int avgAge = (stu1.Age + stu2.Age + stu3.Age) / 3;
            Console.WriteLine(avgAge);
            Console.WriteLine("=====");


            Student2 stu4 = new Student2();
            stu4.SetAge(20);

            Student2 stu5 = new Student2();
            stu5.SetAge(20);

            Student2 stu6 = new Student2();
            stu6.SetAge(20);

            int avgAge2 = (stu4.GetAge() + stu5.GetAge() + stu6.GetAge()) / 3;
            Console.WriteLine(avgAge2);
            Console.WriteLine("=====");


            try
            {
                Student2 stu7 = new Student2();
                stu7.SetAge(20);

                Student2 stu8 = new Student2();
                stu8.SetAge(20);

                Student2 stu9 = new Student2();
                stu9.SetAge(20);

                int avgAge3 = (stu7.GetAge() + stu8.GetAge() + stu9.GetAge()) / 3;
                Console.WriteLine(avgAge3);
                Console.WriteLine("=====");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                Student3 stu10 = new Student3();
                stu10.Age = 20;

                Student3 stu11 = new Student3();
                stu11.Age = 20;

                Student3 stu12 = new Student3();
                stu12.Age = 21;

                int avgAge4 = (stu10.Age + stu11.Age + stu12.Age) / 3;
                Console.WriteLine(avgAge4);
                Console.WriteLine("=====");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                Student4.Amount = 100;
                Console.WriteLine(Student4.Amount);
                Console.WriteLine("=====");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                Student5 stu13 = new Student5();
                stu13.Age = 12;
                Console.WriteLine(stu13.CanWork);
                Console.WriteLine("=====");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                Student6 stu14 = new Student6();
                stu14.Age = 12;
                Console.WriteLine(stu14.CanWork);
                Console.WriteLine("=====");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    class Student
    {
        public int Age; //容易被污染
    }
    class Student2
    {
        private int Age;
        public int GetAge() //Get Set方法对
        {
            return Age;
        }
        public void SetAge(int value)
        {
            if (value >= 0 && value <= 120)
            {
                this.Age = value;
            }
            else
            {
                throw new Exception("Age value has error.");
            }
        }
    }
    class Student3
    {
        private int age;    //字段    //建议：永远使用属性（而不是字段）来暴露数据，即字段永远都是private或protected的
        public int Age  //属性    //实例属性，反映实例当前状态 //属性大多数情况下是字段的包装器（wrapper）
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value >= 0 && value <= 120)
                {
                    this.age = value;
                }
                else
                {
                    throw new Exception("Age value has error.");
                }
            }
        }
    }
    class Student4
    {
        public int Age2 { get; set; }    //属性的简略写法

        private int age;    //属性的完整写法

        public int Age  //属性的完整写法
        {
            get { return age; }
            set
            {
                if (value >= 0 && value <= 120)
                {
                    age = value;
                }
                else
                {
                    throw new Exception("Age value has error.");
                }
            }
        }

        private int age3;   //快速封装字段，重构Ctrl + R + E
        public int Age3 { get => age3; set => age3 = value; }

        private static int amount;  //静态static属性，通过类型当前某个数据反映类型当前状态

        public static int Amount
        {
            get { return amount; }
            set
            {
                if (value >= 0)
                {
                    amount = value;
                }
                else
                {
                    throw new Exception("Age value has error.");
                }
            }
        }
    }
    class Student5
    {
        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public bool CanWork //只读属性，只有getter没有setter //尽管语法上正确，几乎没有人使用“只写属性”，因为属性的主要目的是通过向外暴露数据而表示对象/类型的状态
        {
            get
            {
                if (this.age >= 16) //访问不频繁时使用
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    class Student6
    {
        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                this.CalculateCanWork();    //每次赋值Age时调用CalculateCanWork()，访问不频繁时使用
            }
        }
        private bool canwork;

        public bool CanWork
        {
            get { return canwork; }
        }
        private void CalculateCanWork()
        {
            if (this.age >= 16)
            {
                this.canwork = true;
            }
            else
            {
                this.canwork = false;
            }
        }
    }
}
