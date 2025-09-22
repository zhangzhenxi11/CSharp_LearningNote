namespace ConsoleApp6
{
    public class Program
    {
        static void Main(string[] args)
        {
           Customer customer = new Customer();
           Waiter waiter = new Waiter();
           customer.Order += waiter.Action;
           customer.Action();
           customer.PayTheBill();
           Console.WriteLine("=====");
        }
    }
    public class OrderEventArgs : EventArgs //新建传递事件消息的类    
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }
    public class Customer
    {
        public event EventHandler Order;//使用默认EventHandler  //public delegate void EventHandler(object sender, EventArgs e);

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

    public class Waiter
    {
        public void Action(object sender, EventArgs e)  //使用默认EventHandler，参数改为object sender，EventArgs e
        {
            Customer customer = sender as Customer;
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
