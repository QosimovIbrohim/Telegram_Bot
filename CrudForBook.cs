using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class CrudForBook
    {
        public static string path = @"C:\Users\user\Desktop\DatabseFolders\Books.json";

           

        public static void Update(string last_name, string new_price, string new_name, string new_author, string new_category_name)
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
        public string Name { get; set; }
        public string Author { get; set; }
        public long price { get; set; } 
        public string Category_name { get; set; }
    }
}

