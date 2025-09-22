using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace EventExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();//事件拥有者timer，包含事件Elapsed    //类Timer，类型变量timer引用实例new Timer()
            timer.Interval = 1000;//时间间隔 1000ms=1s
            Boy boy = new Boy(); //事件响应者boy，包含事件处理器Action
            Girl girl = new Girl(); //事件响应者girl
            timer.Elapsed += boy.Action;  //事件Elapsed（带闪电图标），事件订阅操作符+=，事件拥有者.事件+=事件响应者.事件处理器  //使用自动生成Action方法
            timer.Elapsed += girl.Action; //一个事件有两个事件处理器boy.Action和girl.Action //事件订阅，本质上是一种以委托类型为基础的“约定”（事件处理器与事件匹配）
            timer.Start();  //启动Timer
            Console.ReadLine();
            timer.Stop();
            Console.WriteLine("==========");
        }


    }

    class Boy
    {
        internal void Action(object? sender, ElapsedEventArgs e)//事件处理器Action
        {
            Console.WriteLine(sender);  //System.Timers.Timer
            Console.WriteLine(e);   //System.Timers.ElapsedEventArgs

            Console.WriteLine("Jump!");
        }
    }
    class Girl
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Sing!");
        }
    }


}
