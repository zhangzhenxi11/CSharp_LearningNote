using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Combine.Models;

namespace Combine {
    internal class Program {
        static void Main(string[] args) {
            //自定义委托
            MyDele dele1 = new MyDele(M1);  //包裹一个与delegate声明时格式一致的方法
            dele1 += M1;    //添加一个方法，多播委托

            Student stu = new Student();
            dele1 += stu.SayHello;  //添加一个类的实例方法
            dele1 += (new Student()).SayHello;  //简化的写法

            dele1.Invoke(); //使用委托间接调用
            dele1();    //简化的写法，拿委托类型的变量像函数一样使用
            Console.WriteLine("=====");


            //MyDele2 dele2 = new MyDele2(M1);  //报错
            MyDele2 dele2 = new MyDele2(Program.Add);
            int res = dele2(100, 200);  //间接调用，有返回值
            Console.WriteLine(res);
            Console.WriteLine("=====");


            //委托加入泛型参数
            MyDele3<int> deleAdd = new MyDele3<int>(Program.Add);
            int res2 = deleAdd(100, 200);
            Console.WriteLine(res2);
            MyDele3<double> deleMul = new MyDele3<double>(Program.Mul);
            double MulRes = deleMul(3.0, 4.0);
            Console.WriteLine(MulRes);
            Console.WriteLine(deleAdd.GetType().IsClass);
            Console.WriteLine("==========");


            //使用C#内置的委托Action，Action无法返回值
            Action action = new Action(M1);
            action();
            Action<string> action1 = new Action<string>(SayHello);  //Action<T>，Action带泛型参数
            action1("Tim");
            Console.WriteLine("=====");
            Action<string, string> action2 = new Action<string, string>(SayHello2);
            action2("Tim", "Tom");
            Console.WriteLine("=====");
            Action<string, int> action3 = new Action<string, int>(SayHello3);
            var action4 = new Action<string, int>(SayHello3);   //简化的写法
            action3("Tim", 3);
            Console.WriteLine("==========");


            //使用C#内置的委托Func，Func有返回值
            Func<int, int, int> func = new Func<int, int, int>(Add);
            int res3 = func(100, 200);
            Console.WriteLine(res3);
            Func<double, double, double> func2 = new Func<double, double, double>(Mul);
            var func3 = new Func<double, double, double>(Mul);   //简化的写法
            double res4 = func2(3.0, 4.0);
            Console.WriteLine(res4);
            Console.WriteLine("==========");


            //Lambda表达式
            int res5 = Add(100, 200);   //Add是调用之前在别处声明的
            Func<int, int, int> func4 = new Func<int, int, int>((int a, int b) => { return a + b; });   //Lambda表达式(int a, int b) => { return a + b; }，使用匿名方法和Inline方法避免过多零碎方法污染程序
            int res6 = func4(100, 200);
            Console.WriteLine(res6);
            func4 = new Func<int, int, int>((int x, int y) => { return x * y; });
            int res7 = func4(3, 4);
            Console.WriteLine(res7);

            Func<int, int, int> func5 = new Func<int, int, int>((a, b) => { return a + b; });   //Lambda表达式的语法糖 //简化格式，省略类型
            Func<int, int, int> func6 = (a, b) => { return a + b; };    //省略委托实例
            Console.WriteLine("=====");

            //泛型委托用Lambda表达式作为参数时，有很多地方可以进行类型推断
            DoSomeCale<int>((int a, int b) => { return a * b; }, 100, 200);
            DoSomeCale((a, b) => { return a * b; }, 100, 200); //简化的写法，泛型委托的类型推断
            Console.WriteLine("==========");


            //LINQ表达式
            //LINQ：.NET Language Integrated Query   //LINQ语言集成查询，Integrated合并，Query查询（数据库/集合）    //SQL查询数据库语言
            //AdventureWorks2014Entities dbContext = new AdventureWorks2014Entities();
            var dbContext = new AdventureWorks2014Entities();
            var allPeople = dbContext.People.ToList();
            foreach (var p in allPeople) {
                Console.WriteLine(p.FirstName);
            }
            Console.WriteLine("=====");
            var allFirstNames = dbContext.People.Select(p => p.FirstName).ToList();
            foreach (var fn in allFirstNames) {
                Console.WriteLine(fn);
            }
            Console.WriteLine("=====");
            var allFullNames = dbContext.People.Select(p => p.FirstName + " " + p.LastName).ToList();
            foreach (var fn in allFullNames) {
                Console.WriteLine(fn);
            }
            Console.WriteLine("=====");
            var allFullNames2 = dbContext.People.Where(p => p.FirstName == "A").Select(p => p.FirstName + " " + p.LastName);    //LINQ最常用的方法Where和Select
            //Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)，Func委托参数、p类型参数、返回类型为bool类型
            //Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)，Func委托参数、p类型参数、返回类型为string类型
            //关注每一个LINQ函数的用法，把什么的Lambda表达式交给函数作为其实参才能满足函数的形参
            foreach (var fn in allFullNames2) {
                Console.WriteLine(fn);
            }
            Console.WriteLine("=====");
            var yesOrNo = dbContext.People.All(p => p.FirstName == "A");
            Console.WriteLine(yesOrNo);
            Console.WriteLine("=====");
            var yesOrNo2 = dbContext.People.Any(p => p.FirstName == "A");
            Console.WriteLine(yesOrNo2);
            Console.WriteLine("=====");
            var groups = dbContext.People.GroupBy(p => p.FirstName).ToList();
            foreach (var g in groups) {
                Console.WriteLine("Name:{0}\tCount:{1}", g.Key, g.Count());
            }
            Console.WriteLine("=====");
            var count = dbContext.People.Count(p => p.FirstName == "A");
            Console.WriteLine(count);

            //在SSMS（Microsoft SQL Server Management Studio）里查询SQL
            //select FirstName, COUNT(*) from Person
            //where FirstName = 'A'
            //group by FirstName

        }
        static void M1() {
            Console.WriteLine("M1 is called!");
        }
        static int Add(int x, int y) {
            return x + y;
        }
        static double Mul(double x, double y) {
            return x * y;
        }
        static void SayHello(string name) {
            Console.WriteLine($"Hello, {name}!");
        }
        static void SayHello2(string name1, string name2) {
            Console.WriteLine($"Hello, {name1} and {name2}!");
        }
        static void SayHello3(string name1, int round) {
            for (int i = 0; i < round; i++) {
                Console.WriteLine($"Hello, {name1}!");
            }
        }

        //泛型方法DoSomeCale<T> + 委托类型参数Func<T, T, T> func + 泛型参数T x, T y
        static void DoSomeCale<T>(Func<T, T, T> func, T x, T y) {   //Lambda表达式作为参数
            T res = func(x, y);
            Console.WriteLine(res);
        }
    }
    class Student {
        public void SayHello() {
            Console.WriteLine("Hello,I'm a student!");
        }
    }
    delegate void MyDele(); //自定义声明委托，无返回、参数为空的MyDele
    delegate int MyDele2(int a, int b);
    delegate T MyDele3<T>(T a, T b);    //自定义声明泛型委托 //使用泛型委托解决类膨胀问题
}
