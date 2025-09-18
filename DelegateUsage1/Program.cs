using System;
using System.Collections.Generic;



namespace DelegateUsage_Templete
{
    class Program
    {
        static void Main(string[] args)
        {
            ProdouctFactory prodouctFactory = new ProdouctFactory();    
            WarpFactory warpFactory = new WarpFactory();

            Func<Product> func1 = new Func<Product>(prodouctFactory.MakePizza);
            Func<Product> func2 = new Func<Product>(prodouctFactory.MakeToyCar);

            Box box1 = warpFactory.WrapProduct(func1);   //=> Func<Product> getProduct = new Func<Product>(prodouctFactory.MakePizza);
            Box box2 = warpFactory.WrapProduct(func2);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
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
        public Box WrapProduct(Func<Product> getProduct)//Func<Product> : 此委托封装的方法的返回值的类型：Product。
        { 
            Box box = new Box();
            Product product = getProduct.Invoke(); 
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
            return product;
        
        }
        public Product MakeToyCar()
        {
            Product product = new Product();
            product.Name = "Tom Car";
            return product;
        }
    
    }



}
/*
模板方法,"借用"指定的外部方法来产生结果
相当于填空题
常位于代码中部
委托有返回值
*/
