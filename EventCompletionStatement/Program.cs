namespace EventCompletionStatement
{
    public class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer(); //事件拥有者customer
            Waiter waiter = new Waiter();
            customer.Order += waiter.Action;    //事件customer.Order，事件处理器waiter.Action，事件订阅+=
            customer.Action();
            customer.PayTheBill();
            Console.WriteLine("=====");
        }
    }

    public class OrderEventArgs : EventArgs
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }

    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);

    public class Customer //事件的拥有者
    {
        public event OrderEventHandler Order;    //事件的简略声明 //隐藏的字段简化声明Order:private class EventExample.OrderEventHandler
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
            //if (this.orderEventHandler != null)  //简略声明后这里会报错

            /*此处Order并不是字段，语法糖  在Customer类内部能够使用Order事件去做非空比较以及调用Order.Invoke方法纯属不得已而为之，
			因为使用事件的简化声明时，我们没有手动声明一个委托类型的字段。这是微软编译器语法糖所造成的语法冲突和前后不一致。*/
            //
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
            Console.ReadLine(); //等待回车
            this.WalkIn();
            this.SitDown();
            this.Think();
        }

        public void PayTheBill()
        {
            Console.WriteLine("I will pay ${0}.", this.Bill);
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

