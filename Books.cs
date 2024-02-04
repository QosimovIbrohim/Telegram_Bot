using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class Books
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public long price { get; set; }
        public string Category_name { get; set; }

        public static string path = @"C:\Users\user\Desktop\DatabseFolders\Books.json";

        public static void Create(Books bk)
        {
            List<Books> books = DeserializeSerialize<Books>.GetAll(path);
            if (books.Any(c => c.Name == bk.Name))
            {
                return;
            }
            books.Add(bk);
            DeserializeSerialize<Books>.Save(books, path);
        }
        public static string Read()
        {
            StringBuilder builder = new StringBuilder();
            List<Books> books = DeserializeSerialize<Books>.GetAll(path);
            foreach (Books c in books)
            {
                builder.Append($"Book name:{c.Name}\n" +
                    $"Book Author: {c.Author}\n" +
                    $"Book Category: {c.Category_name}\n" +
                    $"Book price: {c.price}\n"+ "\n📖----------------------📖" + "\n");
            }
            return builder.ToString();
        }

        public static void Update(string last_name, string new_price, string new_name, string new_author, string new_category_name)
        {
            try
            {
                List<Books> books = DeserializeSerialize<Books>.GetAll(path);
                if (books != null)
                {
                    int index = books.FindIndex(name => name.Name == last_name);
                    if (index != -1)
                    {
                        books[index].Name = new_name;
                        books[index].Author = new_author;
                        books[index].price = Convert.ToUInt16(new_price);
                        books[index].Category_name = new_category_name;
                        DeserializeSerialize<Books>.Save(books, path);
                    }
                }
            }
            catch { }
        }

        public static void Delete(string del_name)
        {
            try
            {
                List<Books> books = DeserializeSerialize<Books>.GetAll(path);
                var catToRemove = books.Find(ct => ct.Category_name == del_name);

                if (catToRemove != null)
                {
                    books.Remove(catToRemove);
                    DeserializeSerialize<Books>.Save(books, path);
                }
            }
            catch { }
        }
    }
}

