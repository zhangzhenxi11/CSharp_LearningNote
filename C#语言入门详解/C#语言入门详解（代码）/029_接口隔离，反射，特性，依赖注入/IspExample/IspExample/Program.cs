using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace IspExample {
    internal class Program {
        static void Main(string[] args) {
            var driver = new Driver(new Car()); //接口隔离例子1，胖接口分解成小接口
            driver.Drive();
            var driver2 = new Driver2(new HeavyTank());    //违反接口隔离原则
            driver2.Drive();
            Console.WriteLine("=====");

            var driver3 = new Driver(new Truck());  //一个接口继承多个接口
            driver3.Drive();
            var driver4 = new Driver(new MediumTank());
            driver4.Drive();
            Console.WriteLine("==========");


            int[] nums1 = { 1, 2, 3, 4, 5 }; //接口隔离例子2，调用者绝不多要
            ArrayList nums2 = new ArrayList { 1, 2, 3, 4.5 };
            Console.WriteLine(Sum(nums1));
            Console.WriteLine(Sum(nums1));

            var roc = new ReadOnlyCollection(nums1);
            foreach (var n in roc) {
                Console.WriteLine(n);
            }

            var nums3 = new ReadOnlyCollection(nums1);
            Console.WriteLine(Sum(nums3));  //调用者多要，无法转换
            Console.WriteLine("==========");


            var wk = new WarmKiller();  //接口隔离例子3，接口的显示实现
            //wk.Kill();  //Kill方法不应该轻易被看到
            IKiller killer = wk;
            killer.Kill();

            IKiller killer2 = new WarmKiller();
            killer2.Kill(); //只有调用IKiller接口时才显示Kill方法

            var wk2 = killer2 as WarmKiller;    //强制类型转换
            wk2.Love();
            var wk3 = (WarmKiller)killer2;
            wk3.Love();
            var wk4 = (IGentleman)killer2;
            wk4.Love();
            Console.WriteLine("==========");


            //反射reflect机制（避免影响程序运行性能）
            ITank tank = new HeavyTank();   //使用静态类型
            //=====华丽的分割线=====
            var t = tank.GetType(); //使用object类型，所有类型的根 //GetType获得对像在内存当中运行时的与其关联的动态类型的描述信息TypeInfo
            object o = Activator.CreateInstance(t);
            MethodInfo fireMi = t.GetMethod("Fire");    //使用反射机制调用方法    //没有封装的反射
            MethodInfo runMi = t.GetMethod("Run");
            fireMi.Invoke(o, null); //在o的对象上Invoke调用方法，null无参数传入
            runMi.Invoke(o, null);
            Console.WriteLine("==========");


            //反射的第一个用途：依赖注入 //以下是依赖注入的基本用法
            var sc = new ServiceCollection();   //一次性（程序启动时注册）依赖注入，ServiceCollection容器  //安装Microsoft.Extensions.DependencyInjection扩展
            sc.AddScoped(typeof(ITank), typeof(HeavyTank)); //AddScoped(接口，实现接口的类)，用注册的类型创建的实例注入到创建的构造器里  //只需要修改这里，改变程序其它地方调用
            sc.AddScoped(typeof(IVehicle), typeof(Car));
            sc.AddScoped(typeof(IVehicle), typeof(LightTank));
            sc.AddScoped(typeof(IVehicle), typeof(MediumTank));
            sc.AddScoped(typeof(IVehicle), typeof(HeavyTank));  //以最后的调用为准
            sc.AddScoped<Driver>(); //Driver成员
            var sp = sc.BuildServiceProvider(); //BuildServiceProvider，创建ServiceProvider
            //=====华丽的分割线=====
            ITank tank2 = sp.GetService<ITank>();    //在能看到ServiceProvider的地方，从container容器里调用对象，不需要用new操作符
            tank2.Fire();
            tank2.Run();
            var driver5 = sp.GetService<Driver>();
            driver5.Drive();
            Console.WriteLine("==========");
        }

        //接口隔离例子2
        /*
        static int Sum(ICollection nums) {//ICollection引用IEnumerator接口，调用者多要
            int sum = 0;
            foreach (var n in nums) {
                sum += (int)n;
            }
            return sum;
        }
        */
        static int Sum(IEnumerable nums) {//ICollection改为IEnumerable，调用者绝不多要，只用到迭代功能
            int sum = 0;
            foreach (var n in nums) {
                sum += (int)n;
            }
            return sum;
        }

        //创建一个只实现ICollection、不实现IEnumerable的类
        //ICollection定义所有非泛型集合的大小、枚举数和同步方法
        //IEnumerable公开枚举数，该枚举数支持在非泛型集合上进行简单迭代。
        class ReadOnlyCollection : IEnumerable {//只读、只能被迭代、不能删除添加元素的集合
            private int[] _array;
            public ReadOnlyCollection(int[] array) {//函数构造器
                _array = array;
            }
            public IEnumerator GetEnumerator() {//IEnumerator支持对非泛型集合的简单迭代
                return new Enumerator(this);
            }
            public class Enumerator : IEnumerator {//创建ReadOnlyCollection类的类成员Enumerator
                private ReadOnlyCollection _collection;
                private int _head;
                public Enumerator(ReadOnlyCollection collection) {//函数构造器
                    _collection = collection;
                    _head = -1;
                }
                public object Current {
                    get {
                        object o = _collection._array[_head];//ReadOnlyCollection的私有成员_array
                        return o;
                    }
                }

                public bool MoveNext() {
                    if (++_head < _collection._array.Length) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

                public void Reset() {
                    _head = -1;
                }
            }
        }
    }


    //接口隔离例子1
    class Driver {
        private IVehicle _vehicle;
        public Driver(IVehicle vehicle) {
            _vehicle = vehicle;
        }
        public void Drive() {
            _vehicle.Run();
        }
    }
    class Driver2 {
        private ITank _tank;
        public Driver2(ITank tank) {    //违反接口隔离原则
            _tank = tank;
        }
        public void Drive() {
            _tank.Run();
        }
    }
    interface IVehicle {
        void Run();
    }
    class Car : IVehicle {
        public void Run() {
            Console.WriteLine("Car is running...");
        }
    }
    class Truck : IVehicle {
        public void Run() {
            Console.WriteLine("Truck is running...");
        }
    }
    interface IWeapon {//一个接口实现单一功能
        void Fire();
    }
    //interface ITank {    //违反接口隔离原则的情况，传入胖接口    //接口IVehicle和接口ITank的Run方法重复，ITank变成了胖接口
    //    void Fire();
    //    void Run();
    //}
    interface ITank : IVehicle, IWeapon {//C#中拿一个类或一个接口继承其接口时，可以有多个接口   //把胖接口分成本质不同的小接口
    }
    class LightTank : ITank {
        public void Fire() {
            Console.WriteLine("Boom!");
        }

        public void Run() {
            Console.WriteLine("Ka ka ka...");
        }
    }
    class MediumTank : ITank {
        public void Fire() {
            Console.WriteLine("Boom!!");
        }

        public void Run() {
            Console.WriteLine("Ka! ka! ka!...");
        }
    }
    class HeavyTank : ITank {
        public void Fire() {
            Console.WriteLine("Boom!!!");
        }

        public void Run() {
            Console.WriteLine("Ka!! ka!! ka!!...");
        }
    }


    //接口隔离例子3
    interface IGentleman {
        void Love();
    }
    interface IKiller {
        void Kill();
    }
    class WarmKiller : IGentleman, IKiller {
        //public void Kill() {
        //    Console.WriteLine("Let me kill the enemy...");
        //}
        void IKiller.Kill() {
            Console.WriteLine("Let me kill the enemy...");
        }
        public void Love() {
            Console.WriteLine("I will love you forever...");
        }


    }
}
