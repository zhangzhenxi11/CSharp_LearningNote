using System;
using System.Collections;
using System.Collections.Generic;   //泛型的集合、基类、基接口都集中在System.Collections.Generic名称空间里
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloGeneric {
    internal class Program {
        public static void Main(string[] args) {
            Apple apple = new Apple() { Color = "Red" };
            //Box box = new Box() { Cargo = apple };
            AppleBox box = new AppleBox() { Cargo = apple };
            Console.WriteLine(box.Cargo.Color);

            Book book = new Book() { Name = "New Book" };   //类型膨胀
            BookBox bookbox = new BookBox() { Cargo = book };
            Console.WriteLine(bookbox.Cargo.Name);
            Console.WriteLine("=====");


            Apple2 apple2 = new Apple2() { Color = "Red" }; //成员膨胀
            Book2 book2 = new Book2() { Name = "New Book" };
            Box2 box1 = new Box2() { Apple = apple };
            Box2 box2 = new Box2() { Book = book };
            Console.WriteLine(box1.Apple.Color);
            Console.WriteLine(box2.Book.Name);
            Console.WriteLine("=====");


            Apple3 apple3 = new Apple3() { Color = "Red" }; //成员膨胀
            Book3 book3 = new Book3() { Name = "New Book" };
            Box3 box3 = new Box3() { Cargo = apple };
            Box3 box4 = new Box3() { Cargo = book };
            //Console.WriteLine(box3.Cargo.);   无法访问成员
            Console.WriteLine((box3.Cargo as Apple)?.Color);    //强制类型转换，?判断OK后执行后段
            Console.WriteLine((box4.Cargo as Apple)?.Color);    //打印结果null出错
            Console.WriteLine((box4.Cargo as Book)?.Name);
            Console.WriteLine("=====");


            Apple4 apple4 = new Apple4() { Color = "Red" }; //使用泛型，不会产生类型膨胀和成员膨胀
            Book4 book4 = new Book4() { Name = "New Book" };
            Box4<Apple4> box5 = new Box4<Apple4>() { Cargo = apple4 };
            Box4<Book4> box6 = new Box4<Book4>() { Cargo = book4 };
            Console.WriteLine(box5.Cargo.Color);
            Console.WriteLine(box6.Cargo.Name);
            Console.WriteLine("==========");


            Student<int> stu = new Student<int>();  //泛型接口，泛型类
            stu.ID = 101;
            stu.Name = "Timothy";

            Student<ulong> stu2 = new Student<ulong>();
            stu2.ID = 100000000000001;
            stu2.Name = "Timothy";

            Student stu3 = new Student();
            stu3.ID = 100000000000001;
            stu3.Name = "Timothy";
            //==========


            //最常用的泛型接口IList<T>
            IList<int> list = new List<int>();  //IList<int>泛型接口，List<int>泛型类   //C#里的List相当于JAVA里的ArrayList动态数组    //public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>    //ICollection<T>集合，IEnumerable<T>可被迭代
            for (int i = 0; i < 100; i++) {
                list.Add(i);
            }
            foreach (var item in list) {
                Console.WriteLine(item);
            }
            Console.WriteLine("==========");


            //不止一个参数的泛型接口IDictionary<TKey, TValue>和泛型类Dictionary<TKey, TValue>
            IDictionary<int, string> dict = new Dictionary<int, string>();  //IDictionary<TKey, TValue>泛型接口，Dictionary<int, string>泛型类 //多态    //不止一个参数的泛型接口和泛型类
            dict[0] = "Timothy";
            dict[1] = "Michael";
            Console.WriteLine($"Student #1 is {dict[0]}");
            Console.WriteLine($"Student #2 is {dict[1]}");
            Console.WriteLine("==========");


            //泛型算法  //两个数组合并
            int[] a1 = { 1, 2, 3, 4, 5 };
            int[] a2 = { 1, 2, 3, 4, 5, 6 };
            double[] a3 = { 1.1, 2.2, 3.3, 4.4, 5.5 };
            double[] a4 = { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6 };
            var result = Zip(a1, a2);   //自动推断的隐式Zip(a1, a2)，显式Zip<int>(a1, a2)，显式Zip<double>(a3, a3)   //泛型方法的调用时候类型参数的自动推断
            Console.WriteLine(string.Join(",", result));
            var result2 = Zip(a3, a4);
            Console.WriteLine(string.Join(",", result2));
            //var result3 = Zip(a1, a4);    //两个参数类型不一样会报错
            Console.WriteLine("==========");


            //泛型委托里的Action委托（只能引用没有返回值的方法）
            Action<string> a5 = Say;    //用Action委托引用Say方法（间接调用），参数为String
            a5.Invoke("Timothy");
            a5("Timothy");  //委托也可以像方法一样调用，委托本身是可调用类型
            Action<int> a6 = Mul;
            a6(1);
            Console.WriteLine("=====");


            //泛型委托里的Function委托（引用有返回值的方法）
            Func<int, int, int> func1 = Add;   //Func<in T1, in T2, out TResult>
            var result3 = func1(100, 200);
            Console.WriteLine(result3);
            Func<double, double, double> func2 = Add;
            var result4 = func2(100.1, 200.2);
            Console.WriteLine(result4);
            Console.WriteLine("=====");


            //泛型委托里的Lambda表达式   //泛型委托与Lambda表达式组成LINQ查询
            Func<double, double, double> func3 = (double a, double b) => { return a + b; };   //Lambda表达式(参数1，参数2)=>{return 表达式;}   //对于逻辑简单的表达式，在调用的地方匿名声明，不用单独声明，
            func3 = (a, b) => { return a + b; };   //进一步简化
            func3 = (a, b) => a + b;   //进一步简化
            var result5 = func3(100.1, 200.2);
            Console.WriteLine(result5);
            Func<int, int> func4 = a => a * a;   //单个参数时更加简化
            Console.WriteLine("==========");
        }

        /*
        static int[] Zip(int[] a, int[] b) {//方法成员膨胀
            int[] zipped = new int[a.Length + b.Length];
            int ai = 0, bi = 0, zi = 0;
            do {
                if (ai < a.Length) zipped[zi++] = a[ai++];
                if (bi < b.Length) zipped[zi++] = b[bi++];
            } while (ai < a.Length || bi < b.Length);
            return zipped;
        }
        static double[] Zip(double[] a, double[] b) {
            double[] zipped = new double[a.Length + b.Length];
            int ai = 0, bi = 0, zi = 0;
            do {
                if (ai < a.Length) zipped[zi++] = a[ai++];
                if (bi < b.Length) zipped[zi++] = b[bi++];
            } while (ai < a.Length || bi < b.Length);
            return zipped;
        }
        */
        static T[] Zip<T>(T[] a, T[] b) {//泛型方法
            T[] zipped = new T[a.Length + b.Length];
            int ai = 0, bi = 0, zi = 0;
            do {
                if (ai < a.Length) zipped[zi++] = a[ai++];
                if (bi < b.Length) zipped[zi++] = b[bi++];
            } while (ai < a.Length || bi < b.Length);
            return zipped;
        }

        static void Say(string str) {
            Console.WriteLine($"Hello, {str}!");
        }
        static void Mul(int x) {
            Console.WriteLine(x * 100);
        }
        static int Add(int a, int b) {
            return a + b;
        }
        static double Add(double a, double b) {
            return a + b;
        }

    }

    //类型膨胀  //一个商品装一个盒子
    class Apple {//苹果
        public string Color { get; set; }
    }
    class Book {//书
        public string Name { get; set; }
    }
    //class Box {//装箱
    class AppleBox {
        public Apple Cargo { get; set; }
    }
    class BookBox {
        public Book Cargo { get; set; }
    }


    //成员膨胀  //一种盒子
    class Apple2 {//苹果
        public string Color { get; set; }
    }
    class Book2 {//书
        public string Name { get; set; }
    }
    //class Box {//装箱
    class Box2 {
        public Apple Apple { get; set; }
        public Book Book { get; set; }
        //...
    }


    //使用object
    class Apple3 {//苹果
        public string Color { get; set; }
    }
    class Book3 {//书
        public string Name { get; set; }
    }
    class Box3 {//装箱
        public Object Cargo { get; set; }
    }


    //使用泛型
    class Apple4 {//苹果
        public string Color { get; set; }
    }
    class Book4 {//书
        public string Name { get; set; }
    }
    class Box4<TCargo> {//装箱    //<>里的是类型参数
        public TCargo Cargo { get; set; }
    }


    //泛型接口  //保证接口的唯一性
    interface IUnique<TId> {
        TId ID { get; set; }
    }
    class Student<TId> : IUnique<TId> {//泛型类Student<TId>派生自泛型接口IUnique<TId> //泛型接口第一步
        public TId ID { get; set; }
        public string Name { get; set; }
    }
    class Student : IUnique<ulong> {//Student类派生自特化的泛型接口IUnique<ulong>，泛型接口特化  //泛型接口第二步
        public ulong ID { get; set; }
        public string Name { get; set; }
    }
}
