
namespace MulticastDelegateExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student() {ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu2 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu3 = new Student() {  ID = 3, PenColor = ConsoleColor.Blue };

            Action action1 = new Action(stu1.DoHomeWork);
            Action action2 = new Action(stu2.DoHomeWork);
            Action action3 = new Action(stu3.DoHomeWork);

            //action1.Invoke();
            //action2.Invoke();
            //action3.Invoke();
            //多播委托:一个委托封装多个方法
            action1 += action2;
            action1+= action3;
            action1.Invoke();

        }
    }

    class Student
    { 
        public int ID { get; set; }
        public ConsoleColor PenColor { get; set; }

        public void DoHomeWork()
        {
            for (int i = 0; i < 5; i++) {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine("Stduent{0}doning homeWork {1} hours(s)", this.ID, i);
                Thread.Sleep(1000);
            }
        }

    }

}
/*
Stduent1doning homeWork 0 hours(s)
Stduent1doning homeWork 1 hours(s)
Stduent1doning homeWork 2 hours(s)
Stduent1doning homeWork 3 hours(s)
Stduent1doning homeWork 4 hours(s)
Stduent2doning homeWork 0 hours(s)
Stduent2doning homeWork 1 hours(s)
Stduent2doning homeWork 2 hours(s)
Stduent2doning homeWork 3 hours(s)
Stduent2doning homeWork 4 hours(s)
Stduent3doning homeWork 0 hours(s)
Stduent3doning homeWork 1 hours(s)
Stduent3doning homeWork 2 hours(s)
Stduent3doning homeWork 3 hours(s)
Stduent3doning homeWork 4 hours(s)

 
 
 
 
*/