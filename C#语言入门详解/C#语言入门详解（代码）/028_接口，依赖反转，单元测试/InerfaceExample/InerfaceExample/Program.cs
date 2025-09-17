using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InerfaceExample {
    public class Program {
        static void Main(string[] args) {
            int[] nums1 = new int[] { 1, 2, 3, 4, 5 };  //Array数组
            Console.WriteLine(nums1.GetType().BaseType);    //Array   //Array类实现了IEnumerable接口，int[]数组nums1可被迭代
            ArrayList nums2 = new ArrayList { 1, 2, 3, 4, 5 }; //ArrayList动态数组，是非泛型，属于System.Collections    //ArrayList类实现了IEnumerable接口，ArrayList动态数组nums2可被迭代
            Console.WriteLine(Sum(nums1));
            Console.WriteLine(Avg(nums1));
            Console.WriteLine(Sum(nums2));
            Console.WriteLine(Avg(nums2));
            Console.WriteLine("=====");

            Console.WriteLine(Sum2(nums1));
            Console.WriteLine(Avg2(nums1));
            Console.WriteLine(Sum2(nums2));
            Console.WriteLine(Avg2(nums2));
            Console.WriteLine("=====");

            var engine = new Engine();
            var car = new Car(engine);
            car.Run(3);
            Console.WriteLine(car.Speed);
            Console.WriteLine("=====");

            var user = new PhoneUser(new NokiaPhone());
            user.UsePhone();
            user = new PhoneUser(new EricessonPhone());
            user.UsePhone();
            Console.WriteLine("=====");

            var fan = new DeskFan(new PowerSupply());
            Console.WriteLine(fan.Work());

        }


        //对一组整数进行求和、求平均值，这组整数有可能存放在int[]整型数组nums1，也有可能存放在ArrayList数组nums2
        //对int[]整型数组nums1和ArrayList数组nums2进行求和、求平均值，不使用接口，需要写4个函数
        //C#为强类型语言，用于操作整线数组的函数，不能用来操作ArrayList数组
        static int Sum(int[] nums) {
            int sum = 0;
            //foreach(var n in nums) {  //用foreach对集合进行遍历
            //    sum += n;
            //}
            foreach (var n in nums) sum += n;   //牺牲可读性，缩短代码
            return sum;
        }
        static double Avg(int[] nums) {
            int sum = 0; double count = 0;
            foreach (var n in nums) { sum += n; count++; }
            return sum / count;
        }
        static int Sum(ArrayList nums) {
            int sum = 0;
            foreach (var n in nums) sum += (int)n;  //(int)n强制类型转换
            return sum;
        }
        static double Avg(ArrayList nums) {
            int sum = 0; double count = 0;
            foreach (var n in nums) { sum += (int)n; count++; }
            return sum / count;
        }


        //使用接口，提供方为整线数组sums1、ArrayList实例sums2，需求方为Sum、Avg函数，需求是传进来的值能被迭代
        static int Sum2(IEnumerable nums) {//使用接口IEnumerable
            int sum = 0;
            foreach (var n in nums) sum += (int)n;  //(int)n强制类型转换
            return sum;
        }
        static double Avg2(IEnumerable nums) {
            int sum = 0; double count = 0;
            foreach (var n in nums) { sum += (int)n; count++; }
            return sum / count;
        }


        //依赖与耦合
        class Engine {
            public int RPM { get; private set; }
            public void Work(int gas) {
                this.RPM = 1000 * gas;
                this.RPM = 0;   //被依赖的类出现问题
            }
        }
        class Car {//紧耦合，Car类完全依赖在Engine类上，基础类Engine出现问题Car也会出错，不好排查调试、影响团队工作
            private Engine _engine;//创建Engine类的实例，依赖
            public Car(Engine engine) {//函数构造器
                _engine = engine;
            }
            public int Speed { get; private set; }
            public void Run(int gas) {
                _engine.Work(gas);
                this.Speed = _engine.RPM / 100;
            }
        }


        //引入接口，松耦合
        class PhoneUser {//人与手机之间解耦
            private IPhone _phone;
            public PhoneUser(IPhone phone) {
                _phone = phone;
            }
            public void UsePhone() {
                _phone.Dial();
                _phone.PickUp();
                _phone.Send();
                _phone.Receive();
            }
        }
        interface IPhone {//IPhone的I是指interface的I
            void Dial();
            void PickUp();
            void Send();
            void Receive();
        }
        class NokiaPhone : IPhone {
            public void Dial() {
                Console.WriteLine("Nokia calling...");
            }

            public void PickUp() {
                Console.WriteLine("Hello!This is Tim!");
            }

            public void Receive() {
                Console.WriteLine("Nokia message ring...");
            }

            public void Send() {
                Console.WriteLine("Hello!");
            }
        }
        class EricessonPhone : IPhone {
            public void Dial() {
                Console.WriteLine("Ericesson calling...");
            }

            public void PickUp() {
                Console.WriteLine("Hi!This is Tim!");
            }

            public void Receive() {
                Console.WriteLine("Ericesson message ring...");
            }

            public void Send() {
                Console.WriteLine("Hello!");
            }
        }


        //接口、解耦在单元测试中的应用
        //风扇电源、电流、电流保护

        //紧耦合
        class PowerSupply {
            public int GetPower() {
                return 100;
            }
        }
        class DeskFan {
            private PowerSupply _powerSupply;
            public DeskFan(PowerSupply powerSupply) {
                _powerSupply = powerSupply;
            }
            public string Work() {
                int power = _powerSupply.GetPower();
                if (power <= 0) {
                    return "Won't work.";
                }
                else if (power < 100) {
                    return "Slow";
                }
                else if (power < 200) {
                    return "Work fine";
                }
                else {
                    return "Warning!";
                }
            }
        }

        //使用接口，自底向上（重构）
        public interface IPowerSupply {
            int GetPower();
        }
        public class PowerSupply2 : IPowerSupply {
            public int GetPower() {
                return 100;
            }
        }
        public class DeskFan2 {
            private IPowerSupply _powerSupply;
            public DeskFan2(IPowerSupply powerSupply) {
                _powerSupply = powerSupply;
            }
            public string Work() {
                int power = _powerSupply.GetPower();
                if (power <= 0) {
                    return "Won't work.";
                }
                else if (power < 100) {
                    return "Slow";
                }
                else if (power < 200) {
                    return "Work fine";
                }
                else {
                    //return "Explode!";
                    return "Warning!";
                }
            }
        }
    }
}

