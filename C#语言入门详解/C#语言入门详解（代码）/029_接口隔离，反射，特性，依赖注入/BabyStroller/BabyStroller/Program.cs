using BabyStroller.SDK;
using System.Runtime.Loader;

namespace BabyStroller {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine(Environment.CurrentDirectory);    //查看程序运行目录  //BabyStroller\BabyStroller\bin\Debug\net8.0    在这目录下创建文件夹Animals


            //例子BabyStroller主体程序，使用反射实现更松的耦合
            var folder = Path.Combine(Environment.CurrentDirectory, "Animals"); //得到Animals文件夹
            var files = Directory.GetFiles(folder); //获得文件夹中的所有dll  //未使用API，容易出错
            var animalTypes = new List<Type>();
            foreach (var file in files) {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                var types = assembly.GetTypes();
                foreach (var t in types) {
                    /*
                    if (t.GetMethod("Voice") != null) {//使用纯反射（很容易犯错）
                        animalTypes.Add(t); //加载所有animalTypes到内存
                    }
                    if (t.GetInterfaces().Contains(typeof(IAnimal)) 
                        && !t.GetCustomAttributes(false).Contains(typeof(UnfinishedAttribute))) {//过滤失败
                        animalTypes.Add(t);
                    }
                    */
                    if (t.GetInterfaces().Contains(typeof(IAnimal))) {//第一方程序和第三方插件通过SDK组合到一起
                        var isUnfinished = t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(UnfinishedAttribute));
                        if (isUnfinished) continue;
                        animalTypes.Add(t);
                    }
                }
            }

            while (true) {//死循环，应用广泛
                for (int i = 0; i < animalTypes.Count; i++) {
                    Console.WriteLine($"{i + 1}. {animalTypes[i].Name}");    //打印animalTypes的数量
                }
                Console.WriteLine("=====");

                Console.WriteLine("Please choose animal:"); //选择动物
                int index = int.Parse(Console.ReadLine());
                if (index > animalTypes.Count || index < 1) {
                    Console.WriteLine("No such an animal.Try agaion!");
                    continue;
                }

                int times = int.Parse(Console.ReadLine());  //选择次数
                var t = animalTypes[index - 1]; //程序里0开始，用户1开始
                var m = t.GetMethod("Voice");
                var o = Activator.CreateInstance(t);
                //m.Invoke(o, new object[] { times });    //动物叫   //Invoke方法弱类型调用（很容易犯错）
                var a = o as IAnimal;   //强制类型转换
                a.Voice(times); //强类型直接调用
            }
        }
    }
}
