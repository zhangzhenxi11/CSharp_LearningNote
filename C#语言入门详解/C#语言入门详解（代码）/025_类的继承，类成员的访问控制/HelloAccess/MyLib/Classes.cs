using System;


namespace MyLib {
    public class Vehicle2 {
        public string Owner { get; set; }
        private string Owner2 { get; set; } //父类的类成员访问级别为private时，访问范围限制在类体里，子类无法访问 //面向对象第一个原则——封装encapsulation，封装后隐藏conceal
        string Owner3 { get; set; } //默认访问级别为private，但不建议这样写
        //private int _rpm;   //C++和JAVA的写法，变量名签名有下划线，实例字段、私有字段
        protected int _rpm;
        private int _fuel;
        public void Refuel() {
            _fuel = 100;
        }
        //public void Burn() {//烧油不属于司机的工作
        protected void Burn(int fuel) {//protected不被外部调用，可以被子类调用，可以跨程序集调用
            _fuel -= _fuel;
        }
        public void Accelerate() {
            Burn(1);
            _rpm += 1000;   //省略写法
            //this._rpm += 1000;    //完整写法
        }
        public int Speed { get { return _rpm / 100; } }
    }
    public class Car2 : Vehicle2 {
        public void ShowOwner() {
            Console.WriteLine(base.Owner);
            //Console.WriteLine(base.Owner2);   //报错
        }
        public void TurboAccelerate() {
            Burn(2);
            _rpm += 3000;
        }
    }
}
