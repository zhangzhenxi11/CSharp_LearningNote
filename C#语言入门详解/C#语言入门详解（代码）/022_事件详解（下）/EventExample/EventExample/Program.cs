using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //事件完整声明
            Customer customer = new Customer(); //事件拥有者customer
            Waiter waiter = new Waiter();
            customer.Order += waiter.Action;    //事件customer.Order，事件处理器waiter.Action，事件订阅+=
            customer.Action();
            customer.PayTheBill();
            Console.WriteLine("=====");


            //事件简略声明
            Customer2 customer2 = new Customer2();
            Waiter2 waiter2 = new Waiter2();
            customer2.Order += waiter2.Action;
            customer2.Action();
            customer2.PayTheBill();
            Console.WriteLine("=====");


            //不安全的调用方式
            Customer3 customer3 = new Customer3();
            Waiter3 waiter3 = new Waiter3();
            customer3.Order += waiter3.Action;
            //customer3.Action();

            OrderEventArgs e = new OrderEventArgs();
            e.DishName = "Manhanquanxi";
            e.Size = "large";

            OrderEventArgs e2 = new OrderEventArgs();
            e2.DishName = "Beer";
            e2.Size = "large";

            Customer3 badguy = new Customer3();
            badguy.Order += waiter3.Action; //不使用event声明，方法外随意调用
            badguy.Order.Invoke(customer3, e);  //借刀杀人
            badguy.Order.Invoke(customer3, e2);

            customer3.PayTheBill();
            Console.WriteLine("=====");


            //使用默认EventHandler，object sender，EventArgs e，protected
            Customer4 customer4 = new Customer4();
            Waiter4 waiter4 = new Waiter4();
            customer4.Order += waiter4.Action;
            customer4.Action();
            customer4.PayTheBill();
            Console.WriteLine("=====");
        }
    }
    public class OrderEventArgs : EventArgs //新建传递事件消息的类    //此处使用EventArgs后缀，派生自EventArgs类（基类） //注意这不是委托类型的字段  //此处使用public，保证访问级别一致
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }
    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e); //新建委托类型，为了后续使用Order事件  //此处使用EventHandler后缀，此委托专门用来声明事件，此委托用于约束事件处理器，此委托将来创建的实例是专门用来存储事件处理器
    public class Customer   //此处使用public，保证访问级别一致
    {
        //public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);  不能放到类里面，这是嵌套类型
        private OrderEventHandler orderEventHandler;    //声明委托类型的字段，用于存储或者引用事件处理器   //完整声明
        public event OrderEventHandler Order    //声明自定义事件Order，此时使用public希望被外界访问
        {
            add
            {
                this.orderEventHandler += value;    //事件处理器的添加器，上下文关键字value
            }
            remove
            {
                this.orderEventHandler -= value;    //事件处理器的移除器
            }
        }
        public double Bill { get; set; }
        public void PayTheBill()
        {
            Console.WriteLine("I will pay ${0}.", this.Bill);
        }
        public void WalkIn()
        {
            Console.WriteLine("Walk into the restaurant.");
        }
        public void SitDown()
        {
            Console.WriteLine("Sit down.");
        }
        public void Think()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Let me think...");
                Thread.Sleep(100);
            }
            if (this.orderEventHandler != null)  //过滤，等于null时没人订阅事件，没有Waiter服务
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";
                this.orderEventHandler.Invoke(this, e);
            }
        }
        public void Action()
        {
            Console.ReadLine(); //等待回车
            this.WalkIn();
            this.SitDown();
            this.Think();
        }
    }

    public class Waiter //事件响应者Waiter
    {
        public void Action(Customer customer, OrderEventArgs e) //事件处理器waiter.Action
        {
            Console.WriteLine("I will serve you the dish - {0}.", e.DishName);
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }
            customer.Bill += price;
        }
    }


    public delegate void OrderEventHandler2(Customer2 customer2, OrderEventArgs e);

    public class Customer2
    {
        public event OrderEventHandler2 Order;    //事件的简略声明 //隐藏的字段简化声明Order:private class EventExample.OrderEventHandler2
        public double Bill { get; set; }
        public void PayTheBill()
        {
            Console.WriteLine("I will pay ${0}.", this.Bill);
        }
        public void WalkIn()
        {
            Console.WriteLine("Walk into the restaurant.");
        }
        public void SitDown()
        {
            Console.WriteLine("Sit down.");
        }
        public void Think()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Let me think...");
                Thread.Sleep(100);
            }
            //if (this.orderEventHandler != null)  //简略声明后这里会报错
			
			/*此处Order并不是字段，语法糖  在Customer类内部能够使用Order事件去做非空比较以及调用Order.Invoke方法纯属不得已而为之，
			因为使用事件的简化声明时，我们没有手动声明一个委托类型的字段。这是微软编译器语法糖所造成的语法冲突和前后不一致。*/
			//
            if (this.Order != null) 
			
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";
                //this.orderEventHandler.Invoke(this, e);
                this.Order.Invoke(this, e);
            }
        }
        public void Action()
        {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }
    }
    public class Waiter2
    {
        public void Action(Customer2 customer2, OrderEventArgs e)
        {
            Console.WriteLine("I will serve you the dish - {0}.", e.DishName);
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }
            customer2.Bill += price;
        }
    }


    public delegate void OrderEventHandler3(Customer3 customer3, OrderEventArgs e);

    public class Customer3
    {
        //public event OrderEventHandler3 Order;
        public OrderEventHandler3 Order;    //少了event后会变成委托类型字段
        public double Bill { get; set; }
        public void PayTheBill()
        {
            Console.WriteLine("I will pay ${0}.", this.Bill);
        }
        public void WalkIn()
        {
            Console.WriteLine("Walk into the restaurant.");
        }
        public void SitDown()
        {
            Console.WriteLine("Sit down.");
        }
        public void Think()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Let me think...");
                Thread.Sleep(100);
            }
            if (this.Order != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";
                this.Order.Invoke(this, e);
            }
        }
        public void Action()
        {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }
    }
    public class Waiter3
    {
        public void Action(Customer3 customer3, OrderEventArgs e)
        {
            Console.WriteLine("I will serve you the dish - {0}.", e.DishName);
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }
            customer3.Bill += price;
        }
    }



    public class Customer4
    {
        public event EventHandler Order;    //使用默认EventHandler  //public delegate void EventHandler(object sender, EventArgs e);
        public double Bill { get; set; }
        public void PayTheBill()
        {
            Console.WriteLine("I will pay ${0}.", this.Bill);
        }
        public void WalkIn()
        {
            Console.WriteLine("Walk into the restaurant.");
        }
        public void SitDown()
        {
            Console.WriteLine("Sit down.");
        }
        public void Think()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Let me think...");
                Thread.Sleep(100);
            }
            this.OnOrder("Kongpao Chicken", "large");   //单一职责原则（Single Responsibility Principle）
        }
        protected void OnOrder(string dishName, string size)   //访问级别protected，protected成员可以被派生类对象访问，不能被用户代码（类外）访问
        {
            if (this.Order != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = dishName;
                e.Size = size;
                this.Order.Invoke(this, e);
            }
        }
        public void Action()
        {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }
    }
    public class Waiter4
    {
        public void Action(object sender, EventArgs e)  //使用默认EventHandler，参数改为object sender，EventArgs e
        {
            Customer4 customer = sender as Customer4;
            OrderEventArgs orderInfo = e as OrderEventArgs;
            Console.WriteLine("I will serve you the dish - {0}.", orderInfo.DishName);
            double price = 10;
            switch (orderInfo.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }
            customer.Bill += price;
        }
    }
}
