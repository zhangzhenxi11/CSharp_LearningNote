using BabyStroller.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Lib2 {
    /*
    public class Cow {
        public void Voice(int times) {
            for (int i = 0; i < times; i++) {
                Console.WriteLine("Moo!");
            }
        }
    }
    */

    [Unfinished]    //未修改完成的标注Unfinished
    public class Cow:IAnimal {
        public void Voice(int times) {
            for (int i = 0; i < times; i++) {
                Console.WriteLine("Moo!");
            }
        }
    }
}
