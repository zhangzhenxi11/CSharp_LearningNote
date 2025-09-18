namespace DelegateExample
{
    public delegate double Calc(double x,double y); //自定义委托 Calc
    internal class Program
    {

        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            Calc calc1 = new Calc(calculator.Add);
            Calc calc2 = new Calc(calculator.Sub);
            Calc calc3 = new Calc(calculator.Mul);
            Calc calc4 = new Calc(calculator.Div);

            double a = 100;
            double b = 200;
            double c = 0;

            c = calc1(a,b);
            Console.WriteLine(c);
            c = calc2(a, b);
            Console.WriteLine(c);
            c = calc3(a, b);
            Console.WriteLine(c);
            c = calc4(a, b);
            Console.WriteLine(c);


        }
    }

    class Calculator
    {
        public double Add(double x, double y)
        { 
            return x + y;   
        }
        public double Sub(double x, double y)
        {
            return x - y;
        }

        public double Mul(double x, double y)
        {
            return x * y;
        }

        public double Div(double x, double y)
        {
            return x / y;
        }
    }
}
/*委托与所封装的方法必须类型兼容*/