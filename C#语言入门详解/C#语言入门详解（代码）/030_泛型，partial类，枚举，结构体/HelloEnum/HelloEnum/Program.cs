using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloEnum {
    internal class Program {
        static void Main(string[] args) {
            Person person = new Person();
            person.Level = "Boss";
            person.Level = "BigBoss";
            person.Level = "boss";  //无法约束程序员规范输入
            person.Level = "bigboss";
            person.Level = "admin";
            Console.WriteLine("=====");


            Person2 person2 = new Person2();
            person2.Level = Level.Employee;    //使用枚举限定输入范围
            Person2 boss2 = new Person2();
            boss2.Level = Level.Boss;
            Console.WriteLine(boss2.Level > person2.Level);
            Console.WriteLine("=====");

            Console.WriteLine(Level.Employee);
            Console.WriteLine(Level.Manager);
            Console.WriteLine(Level.Boss);
            Console.WriteLine(Level.BigBoss);
            Console.WriteLine("=====");

            Console.WriteLine((int)Level.Employee);
            Console.WriteLine((int)Level.Manager);
            Console.WriteLine((int)Level.Boss);
            Console.WriteLine((int)Level.BigBoss);
            Console.WriteLine("=====");


            Person3 person3 = new Person3();
            person3.Level = Level.Employee;
            person3.Name = "Timothy";
            person3.Skill = Skill.Teach;    //多技能时    //List是比较重的数据结构
            person3.Skill = Skill.Drive | Skill.Cook | Skill.Program | Skill.Teach; //枚举类型的比特位用法
            Console.WriteLine((int)person3.Skill);
            Console.WriteLine((person3.Skill & Skill.Cook) > 0);
            Console.WriteLine((person3.Skill & Skill.Cook) == Skill.Cook);

        }
    }
    enum Level {//枚举enum的本质是限制取值范围的整数
        Employee = 100,   //手动指定值
        Manager,    //不赋值，前枚举值+1
        Boss = 300,
        BigBoss,
    }
    enum Skill {//枚举类型的比特位用法
        Drive =1,
        Cook=2,
        Program=4,
        Teach=8,
    }
    class Person {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
    }
    class Person2 {
        public int ID { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
    }
    class Person3 {
        public int ID { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public Skill Skill { get; set; }
    }
}
