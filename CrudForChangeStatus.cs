using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class CrudForChangeStatus
    {
        public static string path = @"C:\Users\user\Desktop\Status.json";

        public static void Create(Statuse st)
        {
            List<Statuse> mystatus = GetStatus();
            if (mystatus.Any(c => c.Category_name == st.Category_name))
            {
                return;
            }
            mystatus.Add(st);
            SaveCats(mystatus);
        }
        public static string Read()
        {
            StringBuilder sb = new StringBuilder();
            List<Statuse> status = GetStatus();
            foreach (Statuse c in status)
            {
                sb.Append(c.Status + " ");
            }
            return sb.ToString();
        }

        public static void Update(string Bystatus)
        {
            try
            {
                List<Statuse> status = GetStatus();
                if (status != null)
                {
                    int index = status.FindIndex(name => name.Category_name == new_name);
                    if (index != -1)
                    {
                        status[index].Category_name = new_name;
                        SaveCats(status);
                    }
                }
            }
            catch { }
        }

        public static void Delete(string del_name)
        {
            try
            {
                List<Statuse> categories = GetStatus();
                var catToRemove = categories.Find(ct => ct.Category_name == del_name);

                if (catToRemove != null)
                {
                    categories.Remove(catToRemove);
                    SaveCats(categories);
                }
            }
            catch { }
        }
        public static List<Statuse> GetStatus()
        {

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Categories>>(json) ?? new List<Categories>();
            }
            else
            {
                return new List<Categories>();
            }
        }
        public static void SaveCats(List<Statuse> status) 
        {
            string json = JsonSerializer.Serialize(status);
            System.IO.File.WriteAllText(path, json);
        }
    }
    public class Categories
    {
        public string Status;
    }
}

