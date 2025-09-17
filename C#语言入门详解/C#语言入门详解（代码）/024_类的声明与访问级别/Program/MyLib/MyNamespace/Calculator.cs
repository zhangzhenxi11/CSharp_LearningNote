namespace MyLib.MyNamespace {//大名称空间MyLib，子名称空间MyNamespace
    public class Calculator {//类访问级别修饰符public和internal
        public double Add(double a, double b) {
            return a + b;
        }
    }
    internal class Calculator2 {//internal class Calculator等同于class Calculator  //项目级别的访问成为Assembly装配集(装配件)，编译后exe或dll
        public double Add(double a, double b) {
            return a + b;
        }
    }
}
