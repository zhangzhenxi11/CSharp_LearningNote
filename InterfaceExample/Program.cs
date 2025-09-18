namespace InterfaceExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IProdouctFactory pizzaFactory = new PizzaFactory();
            IProdouctFactory toyCarFactory = new ToyCarFactory();
            WarpFactory warpFactory = new WarpFactory();


            Box box1 = warpFactory.WrapProduct(pizzaFactory); 
            Box box2 = warpFactory.WrapProduct(toyCarFactory);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
        }
    }

    interface IProdouctFactory
    {
        Product Make();
    }

    class PizzaFactory : IProdouctFactory
    {
        public Product Make()
        {
            Product product = new Product();
            product.Name = "Pizza";
            return product;
        }
    }

    class ToyCarFactory : IProdouctFactory
    {
        public Product Make()
        {
            Product product = new Product();
            product.Name = "Tom Car";
            return product;
        }
    }

    //产品
    class Product
    {
        public string Name { get; set; }
    }
    //包装盒
    class Box
    {
        public Product Product { get; set; }
    }

    //模板工厂/抽象工厂
    class WarpFactory
    {
        public Box WrapProduct(IProdouctFactory prodouctFactory)
        {
            Box box = new Box();
            Product product = prodouctFactory.Make();
            box.Product = product; //box把产品包装起来
            return box;
        }

    }
}
