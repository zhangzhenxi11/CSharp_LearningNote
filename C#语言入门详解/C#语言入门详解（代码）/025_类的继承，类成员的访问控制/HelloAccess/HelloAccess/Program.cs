using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyLib;

namespace HelloAccess {
    internal class Program {
        static void Main(string[] args) {
            RaceCar raceCar = new RaceCar();
            //raceCar.Owner   //继承于Vehicle类
            //raceCar.GetType   //继承于Object类
            Car car = new Car();
            Console.WriteLine(car.Owner);
            Console.WriteLine("=====");

            car = new Car("Timothy");
            Console.WriteLine(car.Owner);
            Console.WriteLine("=====");

            Vehicle2 vehicle2 = new Vehicle2(); //类库MyLib，Vehicle2类为internal类型时，外部无法访问
            vehicle2.Owner = "Timothy"; //Owner属性为internal类型时，外部无法访问
            Console.WriteLine(vehicle2.Owner);

            Car2 car2 = new Car2();
            car2.Accelerate();
            car2.Accelerate();
            Console.WriteLine(car2.Speed);  //private属性外部无法访问，但是内部可以调用
            Console.WriteLine("=====");

            Car2 car3 = new Car2();
            car3.Refuel();
            car3.TurboAccelerate();
            Console.WriteLine(car3.Speed);
            Console.WriteLine("=====");

            Bus2 bus2 = new Bus2();
            bus2.SlowAccelerate();
            Console.WriteLine(bus2.Speed);

        }
    }
    class Vehicle {
        public Vehicle() {//快捷键ctor，添加构造器
            this.Owner = "N/A";
        }
        public Vehicle(string owner) {
            this.Owner = owner;
        }
        public string Owner { get; set; }
    }
    class Car : Vehicle {
        //public Car() {
        //    this.Owner = "Car Owner";
        //}
        public Car() : base("N/A") {

        }
        public Car(string owner) : base(owner) {//父类的实例构造器不能被子类继承

        }
        public void ShowOwner() {
            Console.WriteLine(this.Owner);  //this.访问子类对象
            Console.WriteLine(Owner);  //简化去掉this.访问子类对象
            Console.WriteLine(base.Owner);  //base.访问上一级父类(基类)对象
        }

    }
    class RaceCar : Car {
    }
    class Bus2 : Vehicle2 {
        public void SlowAccelerate() {
            Burn(1);
            _rpm += 500;
        }
    }
}
