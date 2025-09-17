using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexerExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student stu = new Student();

            var mathScore = stu["Math"];
            Console.WriteLine(mathScore == null);
            Console.WriteLine("=====");

            stu["Math"] = 90;
            mathScore = stu["Math"];
            Console.WriteLine(mathScore); 
            Console.WriteLine("=====");

            stu["Math"] = 100;
            mathScore = stu["Math"];
            Console.WriteLine(mathScore);
            Console.WriteLine("=====");
        }
        class Student
        {
            private Dictionary<string, int> scoreDictionary = new Dictionary<string, int>();    //非集合类型索引器indexer
            public int? this[string subject]
            {
                get
                {
                    if (this.scoreDictionary.ContainsKey(subject)) //字典是否包含值
                    {
                        return this.scoreDictionary[subject];   //索引字典返回值
                    }
                    else
                    {
                        return null;
                    }
                }
                set
                {
                    if (value.HasValue == false)    //检测是否有值
                    {
                        throw new Exception("Score cannoe be null.");
                    }
                    if (this.scoreDictionary.ContainsKey(subject))
                    {
                        this.scoreDictionary[subject] = value.Value;    //可空类型的值
                    }
                    else
                    {
                        this.scoreDictionary.Add(subject, value.Value); //加入字典
                    }
                }
            }
        }
    }
}
