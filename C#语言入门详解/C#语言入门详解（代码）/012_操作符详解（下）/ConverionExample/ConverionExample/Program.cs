using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConverionExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string str1 = Console.ReadLine();
            //string str2 = Console.ReadLine();
            string str1 = "1";
            string str2 = "2";
            Console.WriteLine(str1 + str2); //此处是2个字符串相加
            int x = Convert.ToInt32(str1);  //字符串转成整数
            int y = Convert.ToInt32(str2);
            Console.WriteLine(x + y);   //整数相加
            Console.WriteLine("==========");


            int x2 = int.MaxValue;
            long y2 = x2;   //不丢失精度的转换
            Console.WriteLine(y2);
            Console.WriteLine("==========");


            Teacher t = new Teacher();
            Human h = t;    //子类向父类隐式转换
            Animal a = h;
            t.Teach();
            t.Think();
            t.Eat();    //子类能调用父类的方法
            h.Think();
            h.Eat();
            a.Eat();
            Console.WriteLine("==========");


            Console.WriteLine(ushort.MaxValue);
            uint x3 = 65536;
            ushort y3 = (ushort)x3; //显示转换，精度丢失
            Console.WriteLine(y3);
            Console.WriteLine("==========");

            bool x4 = true;
            int y4 = Convert.ToInt32(x4);   //布尔类型转成整型
            Console.WriteLine(y4);
            Console.WriteLine("==========");


            Stone stone = new Stone();
            stone.Age = 5000;
            //Monkey m = (Monkey)stone;   //无法直接转换
            Monkey wukongSun = (Monkey)stone;   //自定义的显式转换操作符(Monkey)stone
            Console.WriteLine(wukongSun.Age);
            Monkey2 wukongSun2 = stone;   //自定义的隐式转换操作符stone
            Console.WriteLine(wukongSun2.Age);
            Console.WriteLine("==========");


            var x5 = 3 * 4;
            Console.WriteLine(x5.GetType().FullName);
            Console.WriteLine(x5);

            var x6 = 3.0 * 4.0;
            Console.WriteLine(x6.GetType().FullName);
            Console.WriteLine(x6);

            var x7 = 3.0 * 4;   //数值提升，此处3.0*4如果int类型不进行数值提升到double类型会导致精度丢失
            Console.WriteLine(x7.GetType().FullName);
            Console.WriteLine(x7);

            int x8 = 100;
            double y8 = x8; //double比int类型精度高，int转double精度不会丢失
            Console.WriteLine(y8);
            Console.WriteLine("==========");


            int x9 = 5;
            int y9 = 4;
            int w9 = 0;
            int z9 = x9 / y9;
            Console.WriteLine(z9);
            //z9 = x9 / w9; //整数除零异常

            double x10 = 5.0;
            double y10 = 4.0;
            double v10 = -5.0;
            double w10 = 0;
            double z10 = x10 / y10;
            Console.WriteLine(z10);
            z10 = x10 / w10;    //正数除零=正无穷大
            Console.WriteLine(z10);
            z10 = v10 / w10;    //负数除零=负无穷大
            Console.WriteLine(z10); //

            double m10 = double.PositiveInfinity;   //取得正无穷大
            double n10 = double.NegativeInfinity;   //取得负无穷大
            double o10 = m10 / n10; //正无穷除以负无穷=NAN
            Console.WriteLine(o10);

            double x11 = (double)5 / 4;
            Console.WriteLine(x11);

            double x12 = (double)(5 / 4);
            Console.WriteLine(x12);
            Console.WriteLine("==========");


            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i % 10);  //整数取余
            }

            double x13 = 3.5;
            double y13 = 3;
            Console.WriteLine(x13 % y13);   //浮点数取余
            Console.WriteLine("==========");


            var x14 = 3.0 + 4;  //浮点数与整数相加时类型提升
            Console.WriteLine(x14);
            Console.WriteLine(x14.GetType().FullName);

            string s1 = "123";
            string s2 = "abc";
            string s3 = s1 + s2;    //+号连接字符串
            Console.WriteLine(s3);
            Console.WriteLine("==========");


            int x15 = 7;
            int y15 = x15 << 1; //左移相当于数据乘2  //超出范围会溢出
            string strX = Convert.ToString(x15, 2).PadLeft(32, '0');
            string strY = Convert.ToString(y15, 2).PadLeft(32, '0');
            Console.WriteLine(strX);
            Console.WriteLine(x15);
            Console.WriteLine(strY);
            Console.WriteLine(y15);
            Console.WriteLine("==========");


            int x16 = 5;
            double y16 = 4.0;
            var result = x16 > y16;
            Console.WriteLine(result.GetType().FullName);
            Console.WriteLine(result);
            var result2 = x16 >= y16;   //注意不是=>Lamda表达式操作符
            Console.WriteLine(result2);
            var result3 = x16 < y16;
            Console.WriteLine(result3);
            var result4 = x16 <= y16;
            Console.WriteLine(result4);
            var result5 = x16 == y16;
            Console.WriteLine(result5);
            var result6 = x16 != y16;
            Console.WriteLine(result6);
            Console.WriteLine("==========");

            char char1 = 'a';   //char类型属于整数类型，在内存中占用2字节byte即16位bit //1字节byte=8位bit，1字=2字节
            char char2 = 'A';
            var result7 = char1 > char2;
            Console.WriteLine(result7);
            ushort u1 = (ushort)char1;
            ushort u2 = (ushort)char2;
            Console.WriteLine(u1);
            Console.WriteLine(u2);
            Console.WriteLine("==========");

            string str3 = "Abc";
            string str4 = "Abc";
            Console.WriteLine(str3 == str4);
            Console.WriteLine(str3.ToLower() == str4.ToLower());    //忽略大小写
            int result8 = string.Compare(str3, str4);   //相等=0时，不想等=-1
            Console.WriteLine("==========");

            Teacher t2 = new Teacher();
            var result9 = t2 is Teacher;    //使用is检验一个对象是不是某个类型的对象
            Console.WriteLine(result9.GetType().FullName);
            Console.WriteLine(result9);
            t2 = null;
            var result10 = t2 is Teacher;   //null不属于Teacher
            Console.WriteLine(result10.GetType().FullName);
            Console.WriteLine(result10);
            t2 = new Teacher();
            var result11 = t2 is Human; //Teacher从Human中派生出来
            Console.WriteLine(result11.GetType().FullName);
            Console.WriteLine(result11);
            var result12 = t2 is Animal;    //Teacher从Human从Animal中派生出来
            Console.WriteLine(result12.GetType().FullName);
            Console.WriteLine(result12);

            Car car = new Car();
            Console.WriteLine(car is Animal);
            Console.WriteLine(car is object);

            Human h2 = new Human();
            var result13 = h2 is Teacher;
            Console.WriteLine(result13);
            Console.WriteLine("==========");

            object o = new Teacher();
            if (o is Teacher)
            {
                Teacher t3 = (Teacher)o;
                t3.Teach();
            }

            Teacher t4 = o as Teacher;  //实现上一段if的功能
            if (t4 != null)
            {
                t4.Teach();
            }
            Console.WriteLine("==========");


            int x17 = 7;
            int y17 = 28;
            int z17 = x17 & y17;    //位与，都是1才是1，否则为0
            string strX17 = Convert.ToString(x17, 2).PadLeft(32, '0');
            string strY17 = Convert.ToString(y17, 2).PadLeft(32, '0');
            string strZ17 = Convert.ToString(z17, 2).PadLeft(32, '0');
            Console.WriteLine(strX17);
            Console.WriteLine(strY17);
            Console.WriteLine(strZ17);
            Console.WriteLine("");

            z17 = x17 | y17;    //位或，都是0才是0，否则为1
            strX17 = Convert.ToString(x17, 2).PadLeft(32, '0');
            strY17 = Convert.ToString(y17, 2).PadLeft(32, '0');
            strZ17 = Convert.ToString(z17, 2).PadLeft(32, '0');
            Console.WriteLine(strX17);
            Console.WriteLine(strY17);
            Console.WriteLine(strZ17);
            Console.WriteLine("");

            z17 = x17 ^ y17;    //位异或，不一样为1，一样为0
            strX17 = Convert.ToString(x17, 2).PadLeft(32, '0');
            strY17 = Convert.ToString(y17, 2).PadLeft(32, '0');
            strZ17 = Convert.ToString(z17, 2).PadLeft(32, '0');
            Console.WriteLine(strX17);
            Console.WriteLine(strY17);
            Console.WriteLine(strZ17);
            Console.WriteLine("==========");


            int x18 = 5;
            int y18 = 4;
            int a2 = 100;
            int b2 = 200;
            if (x18 > y18 && a2 < b2)   //条件与
            {
                Console.WriteLine("Hello");
            }
            if (x18 > y18 || a2 < b2)   //条件或
            {
                Console.WriteLine("Hello");
            }
            Console.WriteLine("==========");


            if (x18 < y18 && a2++ > 100)    //条件与的短路效应，x < y不成立，&&后不执行
            {
                Console.WriteLine("Hello");
            }
            Console.WriteLine(a2);
            Console.WriteLine("=====");

            if (x18 > y18 && a2++ > 100)    //条件与的短路效应，x > y成立，a2++ > 100不成立，但是会执行a2++
            {
                Console.WriteLine("Hello");
            }
            Console.WriteLine(a2);
            Console.WriteLine("=====");

            a2 = 100;
            if (x18 > y18 || a2++ > 100)    //条件或的短路效应，x > y成立，||后不执行
            {
                Console.WriteLine("Hello");
            }
            Console.WriteLine(a2);
            Console.WriteLine("=====");

            a2 = 100;
            if (x18 < y18 || ++a2 > 100)    //条件或的短路效应，x > y成立，++a2先自加1在执行
            {
                Console.WriteLine("Hello");
            }
            Console.WriteLine(a2);
            Console.WriteLine("==========");


            //int x19;
            //x19 = null;   //报错
            Nullable<int> x19 = null;   //Nullable可为空
            Console.WriteLine(x19.HasValue);    //检查x19是否有值
            x19 = 100;
            Console.WriteLine(x19);
            Console.WriteLine(x19.HasValue);    //检查x19是否有值
            Console.WriteLine("=====");

            int? x20 = null;    //int?与Nullable<int>效果一致
            int y20 = x20 ?? 1; //null合并??，如果x20为null，赋值1
            Console.WriteLine(y20);
            Console.WriteLine("==========");


            int x21 = 80;
            string str5 = string.Empty;
            if (x21 >= 60)
            {
                str5 = "Pass";
            }
            else
            {
                str5 = "Falsed";
            }
            Console.WriteLine(str5);

            string str6 = (x21 >= 60) ? "Pass" : "Falsed";    //?:条件操作符，跟上一段if语句结果一致，并且更简洁
            Console.WriteLine(str6);
            Console.WriteLine("==========");


            int x22 = 7;   
            x22 += 1;   //x22 = x22 + 1;
            Console.WriteLine(x22);
            Console.WriteLine("=====");

            x22 = 7;
            x22 <<= 2;  //x22 = x22 << 2;左移2位相当于乘以4
            Console.WriteLine(x22);
            Console.WriteLine("=====");

            int x23 = 5;
            int y23 = 6;
            int z23 = 7;
            int a23 = x23 += y23 *= z23;
            //相当于
            //y23 = y23 * z23;
            //x23 = x23 + y23;
            //a23 = x23;
            Console.WriteLine(a23);
            Console.WriteLine("==========");
        }
        class Animal
        {
            public void Eat()
            {
                Console.WriteLine("Eating...");
            }
        }
        class Human : Animal    //Human派生自Animal，类后加:加基类名，Animal的成员被Human继承
        {
            public void Think()
            {
                Console.WriteLine("Who I am?");
            }
        }
        class Teacher : Human
        {
            public void Teach()
            {
                Console.WriteLine("I teach programming.");
            }
        }
        class Car
        {
            public void Run()
            {
                Console.WriteLine("Running...");
            }
        }
        class Stone
        {
            public int Age;
            public static explicit operator Monkey(Stone stone) //显式类型的操作符是目标类型的构造器，写在类转换的数据类型里
            {
                Monkey m = new Monkey();
                m.Age = stone.Age / 500;
                return m;
            }
            public static implicit operator Monkey2(Stone stone) //隐式类型的操作符

            {
                Monkey2 m = new Monkey2();
                m.Age = stone.Age / 500;
                return m;
            }
        }
        class Monkey
        {
            public int Age;
        }
        class Monkey2
        {
            public int Age;
        }
    }
}
