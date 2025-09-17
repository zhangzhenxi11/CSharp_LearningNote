// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System;
namespace RectangleApplication
{
    class Rectangle
    {
        double length;
        double width;
        public void Acceptdetails()
        {
            length = 4.5;
            width = 3.5;
        }
        public double GetArea()
        {
            return length*width;
        }
        public void Display()
        {
            Console.WriteLine("Length: {0}", length);
            Console.WriteLine("Width:{0}",width);
            Console.WriteLine("Area: {0}", GetArea());
        }
    }
    class ExecuteRectangle

    {
        static void Main1(string[] args)
        {
            Rectangle r = new Rectangle();
            r.Acceptdetails();
            r.Display();
            Console.ReadLine();
            Console.WriteLine("Size of int:{0}",sizeof(int));
            Console.ReadLine();
        }
    } 
}

/*
Length: 4.5
Width:3.5
Area: 15.75
*/
