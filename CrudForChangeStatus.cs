using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class CrudForChangeStatus
    {
        public static string path = @"C:\Users\user\Desktop\Categpries.json";

        public static void Create(Statuse ct)
        {
            List<Statuse> categories = GetAllCats();
            if (categories.Any(c => c.Category_name == ct.Category_name))
            {
                return;
            }
            categories.Add(ct);
            SaveCats(categories);
        }
        public static string Read()
        {
            StringBuilder sb = new StringBuilder();
            List<Statuse> categories = GetAllCats();
            foreach (Statuse c in categories)
            {
                sb.Append(c.Category_name + " ");
            }
            return sb.ToString();
        }

        public static void Update(string new_name)
        {
            try
            {
                List<Statuse> categories = GetAllCats();
                if (categories != null)
                {
                    int index = categories.FindIndex(name => name.Category_name == new_name);
                    if (index != -1)
                    {
                        categories[index].Category_name = new_name;
                        SaveCats(categories);
                    }
                }
            }
            catch { }
        }

        public static void Delete(string del_name)
        {
            try
            {
                List<Statuse> categories = GetAllCats();
                var catToRemove = categories.Find(ct => ct.Category_name == del_name);

                if (catToRemove != null)
                {
                    categories.Remove(catToRemove);
                    SaveCats(categories);
                }
            }
            catch { }
        }
        public static List<Statuse> GetAllCats()
        {

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Statuse>>(json) ?? new List<Statuse>();
            }
            else
            {
                return new List<Statuse>();
            }
        }
        public static void SaveCats(List<Statuse> categories) 
        {
            string json = JsonSerializer.Serialize(categories);
            System.IO.File.WriteAllText(path, json);
        }
    }
    public class Statuse
    {
        public string Category_name;
        public long price;
    }
    }
}
