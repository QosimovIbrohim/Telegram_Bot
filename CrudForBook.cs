using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class CrudForBook
    {
        public static string path = @"C:\Users\user\Desktop\Books.json";

        public static void Create(Books bk)
        {
            List<Books> books = GetAllCats();
            if (books.Any(c => c.Name == bk.Name))
            {
                return;
            }
            books.Add(bk);
            SaveCats(books);
        }
        public static string Read()
        {
            StringBuilder builder = new StringBuilder();
            List<Books> books = GetAllCats();
            foreach (Books c in books)
            {
                builder.Append($"Book name:{c.Name\n}\n" +
                    $"Book Author: {c.Author}\n" +
                    $"Book Category{c.Category_name}\n" +
                    $"Book price {c.price}");
            }
            return builder.ToString();
        }

        public static void Update(string last_name,string new_price,string new_name, string new_author,string new_category_name)
        {
            try
            {
                List<Books> books = GetAllCats();
                if (books != null)
                {
                    int index = books.FindIndex(name => name.Name == last_name);
                    if (index != -1)
                    {
                        books[index].Name = new_name;
                        SaveCats(books);
                    }
                }
            }
            catch { }
        }

        public static void Delete(string del_name)
        {
            try
            {
                List<Books> books = GetAllCats();
                var catToRemove = books.Find(ct => ct.Category_name == del_name);

                if (catToRemove != null)
                {
                    books.Remove(catToRemove);
                    SaveCats(books);
                }
            }
            catch { }
        }
        public static List<Books> GetAllCats()
        {

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Books>>(json) ?? new List<Books>();
            }
            else
            {
                return new List<Books>();
            }
        }
        public static void SaveCats(List<Books> books)
        {
            string json = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText(path, json);
        }
    }
    public class Books
    {
        public string Name;
        public string Author;
        public long price;
        public string Category_name;
    }
}
}
