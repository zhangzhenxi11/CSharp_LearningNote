using System;
using System.Collections.Generic;
namespace list_tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //WorkingWithString();


           var fibonacciNumbers =  FibonacciNumberFuc();

            foreach (var item in fibonacciNumbers)
                Console.WriteLine(item);
        }

        static List<int> FibonacciNumberFuc()
        {
            int current = 3;
            var fibonacciNumbers = new List<int> { 1, 1 };
            while (current <= 20)
            {
                var previous = fibonacciNumbers[fibonacciNumbers.Count - 1];
                var previous2 = fibonacciNumbers[fibonacciNumbers.Count - 2];
                fibonacciNumbers.Add(previous + previous2);
                current++;
  
            }

            return fibonacciNumbers;
        }

        static void WorkingWithString()
        {
            var names = new List<string> { "zhangzhenxi", "Ana", "Felipe" };
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}!");
            }

            Console.WriteLine();
            names.Add("Maria");
            names.Add("Bill");
            names.Add("Ana");
            foreach (var name in names)
            {
                Console.WriteLine($"{name.ToUpper()}!");
            }

            Console.WriteLine($"My name is {names[0]}");

            Console.WriteLine($"I've added {names[2]} and {names[3]} to the list");

            Console.WriteLine($"The list has {names.Count} people in it");

            //搜索列表并进行排序
            var index = names.IndexOf("Felipe");
            if (index == -1)
            {
                Console.WriteLine($"When an item is not found, IndexOf returns {index}");
            }
            else
            {
                Console.WriteLine($"The name {names[index]} is at index {index}");
            }
            index = names.IndexOf("Not Found");
            if (index == -1)
            {
                Console.WriteLine($"When an item is not found, IndexOf returns {index}");
            }
            else
            {
                Console.WriteLine($"The name {names[index]} is at index {index}");
            }

            //字符串则按字母顺序
            names.Sort();
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}!");
            }


        }
    
    
    
    
    }
}
