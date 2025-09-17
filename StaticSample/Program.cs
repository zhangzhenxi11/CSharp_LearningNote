
using System.Windows.Forms;

namespace StaticSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Form form = new Form();
            form.Text = "hello";
            form.ShowDialog();
        }
    }
}
