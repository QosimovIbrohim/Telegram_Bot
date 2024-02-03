using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class CrudForPayType
    {
        public static string path = @"C:\Users\user\Desktop\PayTypes.json";

        public static void Create(PayType pay)
        {
            List<PayType> categories = GetAllCats();
            if (categories.Any(c => c.Name == pay.Name))
            {
                return;
            }
            categories.Add(pay);
            SaveCats(categories);
        }
        public static string Read()
        {
            StringBuilder sb = new StringBuilder();
            List<PayType> categories = GetAllCats();
            foreach (PayType c in categories)
            {
                sb.Append($"Name: {c.Name}\n");
            }
            return sb.ToString();
        }

        public static void Update(string last_name, string new_name)
        {
            try
            {
                List<PayType> categories = GetAllCats();
                if (categories != null)
                {
                    int index = categories.FindIndex(name => name.Name == last_name);
                    if (index != -1)
                    {
                        categories[index].Name = new_name;
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
                List<PayType> categories = GetAllCats();
                var catToRemove = categories.Find(ct => ct.Name == del_name);

                if (catToRemove != null)
                {
                    categories.Remove(catToRemove);
                    SaveCats(categories);
                }
            }
            catch { }
        }
        public static List<PayType> GetAllCats()
        {

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<PayType>>(json) ?? new List<PayType>();
            }
            else
            {
                return new List<PayType>();
            }
        }
        public static void SaveCats(List<PayType> categories)
        {
            string json = JsonSerializer.Serialize(categories);
            System.IO.File.WriteAllText(path, json);
        }
    }
    public class PayType
    {
        public string Name;
    }
}

