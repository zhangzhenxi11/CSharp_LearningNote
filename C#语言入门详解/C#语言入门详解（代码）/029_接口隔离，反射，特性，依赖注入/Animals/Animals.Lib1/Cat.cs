using BabyStroller.SDK;

namespace Animals {
    /*
    public class Cat {//第三方开发者创建类库，Animals\Animals.Lib1\bin\Debug\net8.0\Animals.Lib1.dll
        public void Voice(int times) {//使用纯反射很容易出错，如果Voice写成voice这个类就会被忽略
            for (int i = 0; i < times; i++) {
                Console.WriteLine("Meow!");
            }
        }
    }
    */

    [Unfinished]    //未修改完成的标注Unfinished    //Attribute的用处：使用反射时，通过反射拿到一个方法或者类，看它有没有被Attribute修饰，再决定放弃还是保留
    public class Cat:IAnimal {//使用主体程序提供的SDK
        public void Voice(int times) {
            for (int i = 0; i < times; i++) {
                Console.WriteLine("Meow!");
            }
        }

    }

}
