using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverrideExample {
    internal class Program {
        static void Main(string[] args) {
            var v = new Vechicle();
            var c = new Car();
            c.Run();
            Console.WriteLine("=====");

            Car2 car2 = new Car2();
            car2.Run(); //
            var v2 = new Vechicle2();
            v2.Run();
            Console.WriteLine("=====");

            Vechicle2 v3 = new Car2();  //is a
            v3.Run();   //调用与实例相关联的版本
            Vechicle2 v4 = new RaseCar2();
            v4.Run();
            Car2 c3 = new RaseCar2();
            c3.Run();
            Vechicle2 c4 = new RaseCar_2(); //hide对override的干扰
            c4.Run();  //RaseCar_2相关的版本，最新的是Car2
            Console.WriteLine("=====");

            Vechicle3 v5 = new Car3();
            v5.Run();   //子类对父类成员的隐藏
            Console.WriteLine("=====");

            Vechicle2 v6 = new Vechicle2();
            v6.Run();
            Console.WriteLine(v6.Speed);
            Vechicle2 v7 = new Car2();
            v7.Run();
            Console.WriteLine(v7.Speed);
            Console.WriteLine("=====");
        }
    }


    class Vechicle {
        public void Run() {
            Console.WriteLine("I'm running!");
        }
    }
    class Car : Vechicle {
        public int Speed { get; set; }  //横向发展（增加成员）
    }


    class Vechicle2 {
        private  int _speed;
        public virtual int Speed {
            get { return _speed; }
            set { _speed = value; }
        }
        public virtual void Run() {//重写前，父类成员标记virtual，virtual名存实亡
            Console.WriteLine("I'm running!");
            _speed = 100;
        }
    }
    class Car2 : Vechicle2 {
        private int _rpm;
        public override int Speed {//重写属性
            get { return _rpm / 100; }
            set { _rpm = value * 100; }
        }
        public override void Run() {//override重写,子类对父类成员的重写，纵向扩展（增加版本）  //方法的重写
            Console.WriteLine("Car is running!");
            _rpm = 5000;
        }
    }
    class RaseCar2 : Car2 {
        public override void Run() {//重写与隐藏的发生条件：函数成员，可见，签名一致
            Console.WriteLine("Race Car is running!");
        }
    }
    class RaseCar_2 : Car2 {
        public void Run() {
            Console.WriteLine("Race Car is running!");
        }
    }


    class Vechicle3 {
        public void Run() {
            Console.WriteLine("I'm running!");
        }
    }
    class Car3 : Vechicle3 {
        public void Run() {//没有virtual和override，子类对父类成员的隐藏
            Console.WriteLine("Car is running!");
        }
    }
}
