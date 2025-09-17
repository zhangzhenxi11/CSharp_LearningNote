using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example027 {
    internal class Program {
        static void Main(string[] args) {
            //Vehicle v = new Vehicle();  //使用重写时可以正常实例化    //使用抽象时，基类不能实例化
            Vehicle v = new RaceCar();
            v.Run();
        }
    }
}

//为做基类而生的“抽象类”与“开发/关闭原则”
abstract class Student {//类里有抽象成员，该类就变成抽象类，必须加上abstract修饰   //抽象类是指函数成员没有被实现的类，类里至少有一个成员没有被实现，该函数成员只能是public不能是private
    abstract public void Study();   //被abstract修饰的方法Study()没有方法体，没有被实现的方法，抽象方法
}

abstract class Vehicle {
    public void Fill() {
        Console.WriteLine("Pay and fill...");
    }
    //public void Run(string type) {
    //    if (type == "car") {//违反开闭原则
    //        Console.WriteLine("Car is running...");
    //    }
    //    else if (type == "truck") {
    //        Console.WriteLine("Truck is running...");
    //    }
    //    else if (type == "raceCar") {
    //        Console.WriteLine("Race car is running...");
    //    }
    //}
    public virtual void Run() {//基类使用virtual，派生类使用override重写
        Console.WriteLine("Vehicle is running...");
    }
    public abstract void Run2(); //使用abstract抽象，存续
    public void Stop() {
        Console.WriteLine("Stop!");
    }
}
class Car : Vehicle {
    //public void Run() {//违反开闭原则
    //    Console.WriteLine("Car is running...");
    //}
    public override void Run() {//使用virtual虚拟->override重写
        Console.WriteLine("Car is running...");
    }
    public override void Run2() {//使用abstract抽象->重写
        Console.WriteLine("Car is running...");
    }
    //public void Stop() {//违反设计原则，不能copy paste
    //    Console.WriteLine("Stop!");
    //}
}
class Truck : Vehicle {
    //public void Run() {
    //    Console.WriteLine("Truck is running...");
    //}
    public override void Run() {
        Console.WriteLine("Truck is running...");
    }
    public override void Run2() {
        Console.WriteLine("Truck is running...");
    }
    //public void Stop() {
    //    Console.WriteLine("Stop!");
    //}
}
class RaceCar : Vehicle {
    //public void Run() {
    //    Console.WriteLine("Race car is running...");
    //}
    public override void Run() {
        Console.WriteLine("Race car is running...");
    }
    public override void Run2() {
        Console.WriteLine("Race car is running...");
    }
    //public void Stop() {
    //    Console.WriteLine("Stop!");
    //}
}


abstract class VehicleBase {//方法全部都使用抽象abstract
    abstract public void Stop();
    abstract public void Fill();
    abstract public void Run();
}
class Vehicle2 : VehicleBase {
    public override void Fill() {
        Console.WriteLine("Pay and fill...");
    }
    public override void Run() {
        Console.WriteLine("Vehicle is running...");
    }
    public override void Stop() {
        Console.WriteLine("Stop!");
    }
}
class Car2 : Vehicle2 {
    public override void Run() {
        Console.WriteLine("Car is running...");
    }
}
class Truck2 : Vehicle2 {
    public override void Run() {
        Console.WriteLine("Truck is running...");
    }
}
class RaceCar2 : Vehicle2 {
    public override void Run() {
        Console.WriteLine("Race car is running...");
    }
}


interface IVehicleBase3 {//接口interface   //将存续类替换成interface  //接口命名方式I开头
    void Stop();    //需要去掉abstract和public，重复会报错
    void Fill();
    void Run();
}
abstract class Vehicle3 : IVehicleBase3 {//抽象abstract，基类Vehicle3
    public void Fill() {
        Console.WriteLine("Pay and fill...");
    }
    abstract public void Run(); //部分方法使用抽象abstract，使用不完全的实现
    public void Stop() {
        Console.WriteLine("Stop!");
    }
}
class Car3 : Vehicle3 {//具体类，override重写
    public override void Run() {
        Console.WriteLine("Car is running...");
    }
}
class Truck3 : Vehicle3 {
    public override void Run() {
        Console.WriteLine("Truck is running...");
    }
}
class RaceCar3 : Vehicle3 {
    public override void Run() {
        Console.WriteLine("Race car is running...");
    }
}