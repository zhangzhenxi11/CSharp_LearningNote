using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; //多线程

namespace MulticastDelegateExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu2 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu3 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            Action action1 = new Action(stu1.DoHomework);
            Action action2 = new Action(stu2.DoHomework);
            Action action3 = new Action(stu3.DoHomework);

            action1.Invoke();
            action2.Invoke();
            action3.Invoke();
            Console.WriteLine("=====");


            action1 += action2; //Action+=多播委托
            action1 += action3;
            action1.Invoke();   //Action.Invoke直接同步调用
            Console.WriteLine("==========");


            stu1.DoHomework();  //直接同步调用
            stu2.DoHomework();
            stu3.DoHomework();

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.", i);
                Thread.Sleep(50);
            }
            Console.WriteLine("=====");


            Student stu4 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu5 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu6 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            Action action4 = new Action(stu4.DoHomework);
            Action action5 = new Action(stu5.DoHomework);
            Action action6 = new Action(stu6.DoHomework);

            action4.Invoke();   //使用委托 Action.Invoke间接同步调用
            action5.Invoke();
            action6.Invoke();

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.", i);
                Thread.Sleep(50);
            }
            Console.WriteLine("=====");


            Student stu7 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu8 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu9 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            Action action7 = new Action(stu7.DoHomework);
            Action action8 = new Action(stu8.DoHomework);
            Action action9 = new Action(stu9.DoHomework);

            action7 += action8;   //多播委托 Action直接同步调用
            action7 += action9;
            action7.Invoke();

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.", i);
                Thread.Sleep(50);
            }
            Console.WriteLine("=========");


            Student stu10 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu11 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu12 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            Action action10 = new Action(stu10.DoHomework);
            Action action11 = new Action(stu11.DoHomework);
            Action action12 = new Action(stu12.DoHomework);

            action10.BeginInvoke(null, null);   //委托 BeginInvoke隐式异步调用
            action11.BeginInvoke(null, null);
            action12.BeginInvoke(null, null);

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.", i);
                Thread.Sleep(50);
            }
            Console.WriteLine("=====");


            Student stu13 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu14 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu15 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            Thread thread1 = new Thread(new ThreadStart(stu13.DoHomework));
            Thread thread2 = new Thread(new ThreadStart(stu14.DoHomework));
            Thread thread3 = new Thread(new ThreadStart(stu15.DoHomework));

            thread1.Start();    //委托 thread显示异步调用
            thread2.Start();
            thread3.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.", i);
                Thread.Sleep(50);
            }
            Console.WriteLine("=====");


            Student stu16 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu17 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu18 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            Task task1 = new Task(new Action(stu16.DoHomework));
            Task task2 = new Task(new Action(stu17.DoHomework));
            Task task3 = new Task(new Action(stu18.DoHomework));

            task1.Start();    //委托 task显示异步调用
            task2.Start();
            task3.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.", i);
                Thread.Sleep(50);
            }
            Console.WriteLine("==========");
        }
    }
    class Student
    {
        public int ID { get; set; }
        public ConsoleColor PenColor { get; set; }
        public void DoHomework()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine("Student {0} doing homework {1} hour(s).", this.ID, i);
                Thread.Sleep(50); //1000ms=1s
            }
        }
    }
}
