


namespace delegate_example
{
    class Program
    {
        static void Main(string[] args)
        {
            //两个c#委托，Action、Func
            Calcuator calcuator = new Calcuator();
            Action action = new Action(calcuator.Report );
            calcuator.Report();
            action.Invoke();
            action();

            Func<int, int, int> func1 = new Func<int, int, int>(calcuator.Add);
            Func<int, int, int> func2 = new Func<int, int, int> (calcuator.Sub);


            int x = 100;
            int y = 100;
            int z = 0;
            z = func1.Invoke(x,y);
            Console.WriteLine(z);   

            z = func2.Invoke(x,y);  
            Console.WriteLine(z);   
        }
    }
    class Calcuator
    {
        public void Report()
        {
            Console.WriteLine("I have 3 methods");
        }

        public int Add(int a, int b)
        { 
            return a + b;
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }


    }

}

//委托是一个类