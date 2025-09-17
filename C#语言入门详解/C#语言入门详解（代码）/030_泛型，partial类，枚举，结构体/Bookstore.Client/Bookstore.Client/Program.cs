using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Client {
    internal class Program {
        static void Main(string[] args) {
            var dbContext = new BookstoreEntities();
            var books = dbContext.Books;
            foreach (var book in books) {
                //Console.WriteLine(book.Name);
                Console.WriteLine(book.Report());
            }
        }
    }
}
