namespace ConsoleApp5
{
    public class Program
    {
        static void Main(string[] args)
        {
            //不安全的调用方式
            Customer customer = new Customer(); 
            Waiter waiter = new Waiter();
            customer.Order += waiter.Action;
            //customer.Action();

            OrderEventArgs e = new OrderEventArgs();
            e.DishName = "Manhanquanxi";
            e.Size = "large";

            OrderEventArgs e2 = new OrderEventArgs();
            e2.DishName = "Beer";
            e2.Size = "large";

            Customer badguy = new Customer();
            badguy.Order += waiter.Action; //不使用event声明，方法外随意调用
            badguy.Order.Invoke(customer,e); //借刀杀人
            badguy.Order.Invoke(customer,e2);

            customer.PayTheBill();
            Console.WriteLine("=====");


        }
    }
    public delegate void OrderEventHandler (Customer customer, OrderEventArgs e);

    public class OrderEventArgs : EventArgs //新建传递事件消息的类 
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }
    public class Customer
    {
        //public event OrderEventHandler Order;
        public OrderEventHandler Order;    //少了event后会变成委托类型字段
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

    public class Waiter
    {
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
/*
 
I will serve you the dish - Manhanquanxi.
I will serve you the dish - Beer.
I will pay $30.
=====

借刀杀人，这里的I是customer顾客，不是badguy
*/