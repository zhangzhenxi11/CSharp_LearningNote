namespace Delegate_asynchronous
{
    class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu2 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu3 = new Student() { ID = 3, PenColor = ConsoleColor.Blue };


            //异步:1.隐式调用
            //Action action1 = new Action(stu1.DoHomeWork);
            //Action action2 = new Action(stu2.DoHomeWork);
            //Action action3 = new Action(stu3.DoHomeWork);

            //这种写法平台不再支持,-->Task.Run
            //action1.BeginInvoke(null,null);
            //action2.BeginInvoke(null, null);
            //action3.BeginInvoke(null, null);

            //Task.Run(() => {action1.Invoke();action2.Invoke();action3.Invoke();});
            //Task.Run(action1.Invoke);
            //Task.Run(action2.Invoke);
            //Task.Run(action3.Invoke);

            //异步:2.显示调用
            //Thread thread1 = new Thread(new ThreadStart(stu1.DoHomeWork));
            //Thread thread2 = new Thread(new ThreadStart(stu2.DoHomeWork));
            //Thread thread3 = new Thread(new ThreadStart(stu3.DoHomeWork));

            //thread1.Start();
            //thread2.Start();
            //thread3.Start();

            //异步:Task
            Task task1 = new Task(new Action(stu1.DoHomeWork));
            Task task2 = new Task(new Action(stu2.DoHomeWork));
            Task task3 = new Task(new Action(stu3.DoHomeWork));
            task1.Start();
            task2.Start();
            task3.Start();


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
