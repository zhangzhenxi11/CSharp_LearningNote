namespace EventExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer(); //事件拥有者customer
            Waiter waiter = new Waiter();
            customer.Order += waiter.Action;
            customer.Action();
            customer.PayBill();
            Console.WriteLine("=====");
        }
    }
    //新建传递事件消息的类
    //此处使用EventArgs后缀，派生自EventArgs类（基类） 
    //注意这不是委托类型的字段  
    //此处使用public，保证访问级别一致
    public class OrderEventArgs: EventArgs
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }

    //新建委托类型，为了后续使用Order事件
    //此处使用EventHandler后缀有三层意思：
    //1.此委托专门用来声明事件，
    //2.此委托用于约束事件处理器，
    //3.此委托将来创建的实例是专门用来存储事件处理器
    
    public delegate void OrderEventHandler(Customer customer,OrderEventArgs e);

    public class Customer //事件的拥有者
    {
        //public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);  不能放到类里面，这是嵌套类型

        private OrderEventHandler orderEventHandler;     //声明委托类型的字段，用于存储或者引用事件处理器   //完整声明

        public event OrderEventHandler Order             //声明自定义事件Order，此时使用public希望被外界访问
        {
            add
            {
                this.orderEventHandler += value;         //事件处理器的添加器，上下文关键字value
            }
            remove {
                this.orderEventHandler -= value;         //事件处理器的移除器
            }
        
        }

        public double Bill { get; set; }
        public void PayBill()
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
                this.orderEventHandler.Invoke(this,e); 
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
        //事件处理器waiter.Action
        public void Action(Customer customer, OrderEventArgs e)
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
}
//事件是基于委托的

/*
Walk into the restaurant.
Sit down.
Let me think...
Let me think...
Let me think...
Let me think...
Let me think...
I will serve you the dish - Kongpao Chicken.
I will pay $15.
=====
 
*/
