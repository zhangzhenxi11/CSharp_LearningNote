using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateOperator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            int y = 4;
            int z = x / y;  //算式中除号/与类型int相关联
            Console.WriteLine(z);

            double a = 5.0;
            double b = 4.0;
            double c = a / b;   //算式中除号/与类型double相关联
            Console.WriteLine(c);

            Person persion1 = new Person();
            Person persion2 = new Person();
            persion1.Name = "Deer";
            persion2.Name = "Deer's wife";
            List<Person> nation = Person.GetMarry(persion1, persion2);
            foreach (var p in nation)
            {
                Console.WriteLine(p.Name);
            }

            Console.WriteLine("==========");
            List<Person> nation2 = persion1 + persion2;
            foreach (var p in nation)
            {
                Console.WriteLine(p.Name);
            }
        }
        class Person
        {
            public string Name;
            public static List<Person> GetMarry(Person p1, Person p2)
            {
                List<Person> people = new List<Person>();
                people.Add(p1);
                people.Add(p2);
                for (int i = 0; i < 11; i++)
                {
                    Person child = new Person();
                    child.Name = p1.Name + "&" + p2.Name + "s child";
                    people.Add(child);
                }
                return people;
            }
            public static List<Person> operator +(Person p1, Person p2) //自定义操作符
            {
                List<Person> people = new List<Person>();
                people.Add(p1);
                people.Add(p2);
                for (int i = 0; i < 11; i++)
                {
                    Person child = new Person();
                    child.Name = p1.Name + "&" + p2.Name + "s child";
                    people.Add(child);
                }
                return people;
            }
        }
    }
}
