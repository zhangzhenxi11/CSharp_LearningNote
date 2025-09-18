namespace Delegate_Synchronous
{
    class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu2 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu3 = new Student() { ID = 3, PenColor = ConsoleColor.Blue };

            //1.直接调用方法
            //stu1.DoHomeWork();
            //stu2.DoHomeWork();
            //stu3.DoHomeWork();

            //for (int i = 0; i < 10; i++)
            //{ 
            //    Console.ForegroundColor = ConsoleColor.Red; 
            //    Console.WriteLine("Main thread{0}",i);
            //    Thread.Sleep(1000); 
            //}

            //2.委托:间接调用
            Action action1 = new Action(stu1.DoHomeWork);
            Action action2 = new Action(stu2.DoHomeWork);
            Action action3 = new Action(stu3.DoHomeWork);

            action1 += action2;
            action1 += action3;
            action1.Invoke();

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Main thread{0}", i);
                Thread.Sleep(1000);
            }
        }
    }

    class Student
    {
        public int ID { get; set; }
        public ConsoleColor PenColor { get; set; }

        public void DoHomeWork()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine("Stduent{0}doning homeWork {1} hours(s)", this.ID, i);
                Thread.Sleep(1000);
            }
        }

    }
}
