using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//1.关键字 await 提供一种非阻止方式来启动任务，然后在任务完成时继续执行
//2.同时启动任务
//3.支持任务组合
//异步操作后跟同步操作的这种组合是一个异步操作。 换句话说，如果操作的任何部分是异步的，则整个操作都是异步的。例如：
//制作吐司的过程是异步操作（烤面包）与同步操作（在吐司上抹黄油和果酱）的组合

//await 方法，以推迟处理工作，直到结果准备就绪
namespace async_await
{
    // These classes are intentionally empty for the purpose of this example. They are simply marker classes for the purpose of demonstration, contain no properties, and serve no other purpose.
    internal class HashBrown { }  //internal 它所修饰的类只能在同一个程序集中被访问
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }

    class Program_1
    {
        static async Task Main(string[] args)
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            var eggsTask = FryEggsAsync(2);
            var hashBrownTask = FryHashBrownsAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var breakfastTasks = new List<Task> { eggsTask, hashBrownTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                //WhenAny 该方法返回一个当其任一参数完成时就完成的 Task<Task> 对象
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == hashBrownTask)
                {
                    Console.WriteLine("hash browns are ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                await finishedTask;
                breakfastTasks.Remove(finishedTask);
            }

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }
        //用黄油和果酱做吐司
        static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static async Task<HashBrown> FryHashBrownsAsync(int patties)
        {
            Console.WriteLine($"putting {patties} hash brown patties in the pan");
            Console.WriteLine("cooking first side of hash browns...");
            await Task.Delay(3000);
            for (int patty = 0; patty < patties; patty++)
            {
                Console.WriteLine("flipping a hash brown patty");
            }
            Console.WriteLine("cooking the second side of hash browns...");
            await Task.Delay(3000);
            Console.WriteLine("Put hash browns on plate");

            return new HashBrown();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }
}

/*
Pouring coffee   倒咖啡
coffee is ready   咖啡好了
Warming the egg pan...   加热煎蛋锅…
putting 3 hash brown patties in the pan   在锅里放3个土豆饼
cooking first side of hash browns...      煎第一面土豆饼…
Putting a slice of bread in the toaster   把一片面包放进烤面包机
Putting a slice of bread in the toaster   把一片面包放进烤面包机
Start toasting...   开始烤土司…
cracking 2 eggs   打2个鸡蛋
cooking the eggs ...   煮鸡蛋……
flipping a hash brown patty   煎土豆饼
flipping a hash brown patty   煎土豆饼
flipping a hash brown patty   煎土豆饼
cooking the second side of hash browns...   煎土豆煎饼的另一面…
Remove toast from toaster   从烤面包机中取出吐司
Putting butter on the toast   把黄油涂在吐司上
Putting jam on the toast   把果酱涂在吐司上
toast is ready   烤面包好了
Put eggs on plate   把鸡蛋放在盘子里
eggs are ready   鸡蛋准备好了
Put hash browns on plate   把土豆饼放在盘子里
hash browns are ready   炸土豆饼好了
Pouring orange juice   倒橙汁
oj is ready   Oj准备好了
Breakfast is ready!   早餐准备好了！
 
总结：最终代码是异步的。它更准确地反映了一个人做早餐的方式
async 和 await 关键字的语言特性实现了人人都能理解的编程范式转换：尽可能地启动任务，不要在等待任务完成时造成阻塞
*/
