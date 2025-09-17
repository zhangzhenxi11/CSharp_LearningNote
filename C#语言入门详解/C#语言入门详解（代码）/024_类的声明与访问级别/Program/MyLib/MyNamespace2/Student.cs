using MyLib.MyNamespace;
namespace MyLib.MyNamespace2 {
    internal class Student {
        public MyLib.MyNamespace.Calculator2 Calculator2 { get; set; }//internal修饰符，项目级别，在项目内自由访问
    }
}
