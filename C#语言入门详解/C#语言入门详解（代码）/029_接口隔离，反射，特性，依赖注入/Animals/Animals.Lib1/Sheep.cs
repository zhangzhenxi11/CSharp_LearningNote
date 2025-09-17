using BabyStroller.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Lib {
    /*
    public class Sheep {
        public void Voice(int times) {
            for (int i = 0; i < times; i++) {
                Console.WriteLine("Baa...!");
            }
        }
    }
    */

    public class Sheep:IAnimal {
        public void Voice(int times) {
            for (int i = 0; i < times; i++) {
                Console.WriteLine("Baa...!");
            }
        }
    }
}
