namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(Action);
            Console.WriteLine(t.IsClass);
        }
    }
}
