using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateExample {
    public delegate double Calc(double x, double y);    //delegate委托，声明在名字空间里，与class同一级别

    internal class Program {
        public delegate double Calc_In(double x, double y);    //delegate委托，声明在Program类里，是Program类的嵌套类

        static void Main(string[] args) {
            Calculator calculator = new Calculator();   //创建实例后才能调用方法
            calculator.Report();    //直接调用
            Console.WriteLine("=====");


            //C#语言中常用的委托类型Action、Func
            Action action1 = new Action(calculator.Report);  //Action委托 Calculator类的Report()方法，该方法不具有参数和不返回值
            action1.Invoke();    //间接调用
            action1();   //函数指针的写法
            Console.WriteLine("=====");


            Func<int, int, int> func1 = new Func<int, int, int>(calculator.Add);    //Func委托 Calculator类的Add(int a, int b)
            Func<int, int, int> func2 = new Func<int, int, int>(calculator.Sub);

            int x = 100;
            int y = 200;
            int z = 0;

            z = func1.Invoke(x, y);    //函数间接调用
            z = func1(x, y);    //函数指针的写法
            Console.WriteLine(z);
            z = func2.Invoke(x, y);
            z = func2(x, y);
            Console.WriteLine(z);
            Console.WriteLine("==========");


            Calculator2 calculator2 = new Calculator2();

            Calc calc1 = new Calc(calculator2.Add); //delegate委托
            Calc calc2 = new Calc(calculator2.Sub);
            Calc calc3 = new Calc(calculator2.Mul);
            Calc calc4 = new Calc(calculator2.Div);

            double a = 100;
            double b = 200;
            double c = 0;

            c = calc1.Invoke(a, b);
            c = calc1(a, b);
            Console.WriteLine(c);
            c = calc2.Invoke(a, b);
            c = calc2(a, b);
            Console.WriteLine(c);
            c = calc3.Invoke(a, b);
            c = calc3(a, b);
            Console.WriteLine(c);
            c = calc4.Invoke(a, b);
            c = calc4(a, b);
            Console.WriteLine(c);
            Console.WriteLine("=====");


            Program.Calc_In calc5 = new Calc_In(calculator2.Add);   //嵌套委托，声明在Program类里
            Program.Calc_In calc6 = new Calc_In(calculator2.Sub);
            Program.Calc_In calc7 = new Calc_In(calculator2.Mul);
            Program.Calc_In calc8 = new Calc_In(calculator2.Div);

            double a2 = 100;
            double b2 = 200;
            double c2 = 0;

            c2 = calc5.Invoke(a2, b2);
            c2 = calc5(a2, b2);
            Console.WriteLine(c2);
            c2 = calc6.Invoke(a2, b2);
            c2 = calc6(a2, b2);
            Console.WriteLine(c);
            c2 = calc7.Invoke(a2, b2);
            c2 = calc7(a2, b2);
            Console.WriteLine(c2);
            c2 = calc8.Invoke(a2, b2);
            c2 = calc8(a2, b2);
            Console.WriteLine(c2);
            Console.WriteLine("==========");


            ProductFactory productFactory = new ProductFactory();
            WrapFactory wrapFactory = new WrapFactory();

            Func<Product> func3 = new Func<Product>(productFactory.MakePizza);
            Func<Product> func4 = new Func<Product>(productFactory.MakeToyCar);

            Box box3 = wrapFactory.WrapProduct(func3); //委托的模板方法
            Box box4 = wrapFactory.WrapProduct(func4);

            Console.WriteLine(box3.Product.Name);
            Console.WriteLine(box4.Product.Name);
            Console.WriteLine("=====");


            ProductFactory2 productFactory2 = new ProductFactory2();
            WrapFactory2 wrapFactory2 = new WrapFactory2();

            Func<Product2> func5 = new Func<Product2>(productFactory2.MakePizza);
            Func<Product2> func6 = new Func<Product2>(productFactory2.MakeToyCar);

            Logger logger = new Logger();
            Action<Product2> log = new Action<Product2>(logger.Log);

            Box2 box5 = wrapFactory2.WrapProduct(func5, log); //委托的callback方法
            Box2 box6 = wrapFactory2.WrapProduct(func6, log);

            Console.WriteLine(box3.Product.Name);
            Console.WriteLine(box4.Product.Name);
            Console.WriteLine("==========");


            IProductFactory pizzaFactory = new PizzaFactory();  //使用接口代替委托
            IProductFactory toycarFactory = new ToyCarFactory();
            WrapFactory3 wrapFactory3 = new WrapFactory3();

            Box3 box7 = wrapFactory3.WrapProduct(pizzaFactory);
            Box3 box8 = wrapFactory3.WrapProduct(toycarFactory);

            Console.WriteLine(box3.Product.Name);
            Console.WriteLine(box4.Product.Name);
            Console.WriteLine("==========");
        }
    }
    class Calculator {
        public void Report() {
            Console.WriteLine("I have 3 methods.");
        }
        public int Add(int a, int b) {
            int result = a + b;
            return result;
        }
        public int Sub(int a, int b) {
            int result = a - b;
            return result;
        }
    }
    class Calculator2 {
        public double Add(double x, double y) {
            return x + y;
        }
        public double Sub(double x, double y) {
            return x - y;
        }
        public double Mul(double x, double y) {
            return x * y;
        }
        public double Div(double x, double y) {
            return x / y;
        }
    }
    class Product {
        public string Name { get; set; }
    }
    class Box {
        public Product Product { get; set; }
    }
    class WrapFactory {
        public Box WrapProduct(Func<Product> getProduct)    //委托 模板方法
        {
            Box box = new Box();
            Product product = getProduct.Invoke();
            box.Product = product;
            return box;
        }
    }
    class ProductFactory {
        public Product MakePizza() {
            Product product = new Product();
            product.Name = "Pizza";
            return product;
        }
        public Product MakeToyCar() {
            Product product = new Product();
            product.Name = "Toy Car";
            return product;
        }
    }


    class Logger {
        public void Log(Product2 product) {
            //Console.WriteLine("Product '{0}' created at {1}.Price is {2}.", product.Name, DateTime.UtcNow, product.Price); //UtcNow不带时区
            Console.WriteLine("Product '{0}' created at {1}.Price is {2}.", product.Name, DateTime.Now, product.Price);
        }
    }
    class Product2 {
        public string Name { get; set; }
        public double Price { get; set; }

    }
    class Box2 {
        public Product2 Product { get; set; }
    }
    class WrapFactory2 {
        public Box2 WrapProduct(Func<Product2> getProduct, Action<Product2> logCallback)    //委托 callback方法
        {
            Box2 box = new Box2();
            Product2 product = getProduct.Invoke();
            if (product.Price > 50) {
                logCallback(product);
            }
            box.Product = product;
            return box;
        }
    }
    class ProductFactory2 {
        public Product2 MakePizza() {
            Product2 product = new Product2();
            product.Name = "Pizza";
            product.Price = 12;
            return product;
        }
        public Product2 MakeToyCar() {
            Product2 product = new Product2();
            product.Name = "Toy Car";
            product.Price = 100;
            return product;
        }
    }


    interface IProductFactory {
        Product3 Make();
    }
    class PizzaFactory : IProductFactory {
        public Product3 Make() {
            Product3 product = new Product3();
            product.Name = "Pizza";
            return product;
        }
    }
    class ToyCarFactory : IProductFactory {
        public Product3 Make() {
            Product3 product = new Product3();
            product.Name = "Toy Car";
            return product;
        }
    }
    class Product3 {
        public string Name { get; set; }

    }
    class Box3 {
        public Product3 Product { get; set; }
    }
    class WrapFactory3 {
        public Box3 WrapProduct(IProductFactory productFactory)    //接口
        {
            Box3 box = new Box3();
            Product3 product = productFactory.Make();
            box.Product = product;
            return box;
        }
    }
}
