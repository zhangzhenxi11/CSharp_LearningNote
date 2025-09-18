
using System;
using System.Collections.Generic;



namespace DelegateUsage_Callback
{
    class Program
    {
        static void Main(string[] args)
        {
            ProdouctFactory prodouctFactory = new ProdouctFactory();
            WarpFactory warpFactory = new WarpFactory();

            Func<Product> func1 = new Func<Product>(prodouctFactory.MakePizza);
            Func<Product> func2 = new Func<Product>(prodouctFactory.MakeToyCar);
            Logger logger = new Logger();
            Action<Product> log = new Action<Product>(logger.Log);

            Box box1 = warpFactory.WrapProduct(func1, log);   
            //=> Func<Product> getProduct = new Func<Product>(prodouctFactory.MakePizza);
            //Action<Product> logCallBack = new Action<Product>(logger.Log); 

            Box box2 = warpFactory.WrapProduct(func2, log);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
        }
    }

    class Logger
    {
        public void Log(Product product)
        { 
            Console.WriteLine("Product '{0}' create at {1}.Price is {2}", product.Name,DateTime.UtcNow,product.Price);
        }
    }

     //产品
     class Product
     {
         public string Name { get; set; }
         public double Price { get; set; }
     }
     //包装盒
     class Box
     {
         public Product Product { get; set; }
     }

     //模板工厂/抽象工厂
     class WarpFactory
     {
         public Box WrapProduct(Func<Product> getProduct,Action<Product> logCallBack)
         {
             Box box = new Box();
             Product product = getProduct.Invoke();
             if (product.Price >= 50)
             { 
                 logCallBack(product);
             }
             box.Product = product; //box把产品包装起来
             return box;
         }

     }
     class ProdouctFactory
     {
         public Product MakePizza()
         {
             Product product = new Product();
             product.Name = "Pizza";
             product.Price = 12;
             return product;

         }
         public Product MakeToyCar()
         {
             Product product = new Product();
             product.Name = "Tom Car";
             product.Price = 100;
             return product;
         }

     }
}
/*回调方法,调用指定的外部方法
相当于流水线
常位于代码结尾
委托无返回值
 
 */