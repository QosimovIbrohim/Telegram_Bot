using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Telegram_Bot
{
    public class CrudForCategory
    {
        private static readonly string path = @"C:\Users\user\Desktop\Categories.json";

        public static void Create(Categories ct)
        {
            try
            {
                List<Categories> categories = GetAllCats();
                if (categories.Any(c => c.Category_name == ct.Category_name))
                {
                    return;
                }
                categories.Add(ct);
                SaveCats(categories);
            }
            catch
            {
            }
        }

        public static string Read()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<Categories> categories = GetAllCats();
                foreach (Categories c in categories)
                {
                    sb.Append($"Name: {c.Category_name}\n");
                }
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void Update(string new_name)
        {
            try
            {
                List<Categories> categories = GetAllCats();
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
            catch 
            {
            }
        }

        public static void Delete(string del_name)
        {
            try
            {
                List<Categories> categories = GetAllCats();
                var catToRemove = categories.Find(ct => ct.Category_name == del_name);

                if (catToRemove != null)
                {
                    categories.Remove(catToRemove);
                    SaveCats(categories);
                }
              
            }
            catch
            {
                
            }
        }

        public static List<Categories> GetAllCats()
        {
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<List<Categories>>(json) ?? new List<Categories>();
                }
                else
                {
                    return new List<Categories>();
                }
            }
            catch
            {
                return new List<Categories>();
            }
        }

        public static void SaveCats(List<Categories> categories)
        {
            try
            {
                string json = JsonSerializer.Serialize(categories);
                File.WriteAllText(path, json);
            }
            catch
            {
            }
        }

        public class Categories
        {
            public string Category_name;
        }
    }
}
