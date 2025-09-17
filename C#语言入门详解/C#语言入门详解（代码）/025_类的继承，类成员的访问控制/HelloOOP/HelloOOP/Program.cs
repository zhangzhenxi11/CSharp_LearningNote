using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloOOP {
    internal class Program {
        static void Main(string[] args) {
            Type t = typeof(Car);
            Type tb = t.BaseType;   //Car类的类型
            Console.WriteLine(tb.FullName); //HelloOOP.Vehicle，Car类的类型
            Type tTop = tb.BaseType;
            Console.WriteLine(tTop.FullName);   //System.Object，C#所有的类都是从Object派生出来
            Console.WriteLine(tTop.BaseType == null);   //True，Object类是所有继承类的顶端，Object没有基类
            Console.WriteLine("=====");

            //是一个，is a，一个子类的实例从语义上来讲也是父类的实例，一个派生类的实例从语义上来讲也是基类的实例
            Car car = new Car();
            Console.WriteLine(car is Vehicle);  //True，汽车是交通工具，实例car属于Vehicle类
            Console.WriteLine(car is Object);   //True，实例car属于Object类
            Vehicle vehicle = new Vehicle();
            Console.WriteLine(vehicle is Car);  //False，交通工具不是汽车，实例vehicle不属于Car类
            Console.WriteLine("=====");

            Vehicle vehicle2 = new Car();   //用父类类型的变量引用子类类型的实例，多态，一个汽车是一个交通工具
            Object o1 = new Vehicle();  //一个交通工具是一个实例
            Object o2 = new Car();  //一个汽车是一个实例
        }
    }
    //class Vehicle:Object {//缺省，隐式派生
    //sealed class Vehicle {//sealed封闭，不能从封闭类派生
    class Vehicle {//Vehicle交通工具

    }
    class Toy {

    }
    //class Car:Vehicle,Toy {//错误，一个类最多只允许有一个基类
    class Car : Vehicle {//类:基类，基类Vehicle，Car类从Vehicle类派生出来，Car类继承了Vehicle类

    }

    //internal class Vehicle{
    //pulbic class Car : Vehicle{//错误，子类的访问级别不能超越父类的访问级别
}
