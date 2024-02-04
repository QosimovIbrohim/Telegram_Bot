using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Telegram_Bot
{
    public class Categories
    {
        public string Category_name { get; set; }

        private static readonly string path = @"C:\Users\user\Desktop\DatabseFolders\Categories.json";

        public static void Create(Categories ct)
        {
            try
            {
                List<Categories> categories = DeserializeSerialize<Categories>.GetAll(path);
                if (categories.Any(c => c.Category_name == ct.Category_name))
                {
                    return;
                }
                categories.Add(ct);
                DeserializeSerialize<Categories>.Save(categories, path);
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
                List<Categories> categories = DeserializeSerialize<Categories>.GetAll(path);
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

        public static void Update(string last_name,string new_name)
        {
            try
            {
                List<Categories> categories = DeserializeSerialize<Categories>.GetAll(path);
                if (categories != null)
                {
                    int index = categories.FindIndex(name => name.Category_name == last_name);
                    if (index != -1)
                    {
                        categories[index].Category_name = new_name;
                        DeserializeSerialize<Categories>.Save(categories, path);
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
                List<Categories> categories = DeserializeSerialize<Categories>.GetAll(path);
                var catToRemove = categories.Find(ct => ct.Category_name == del_name);

                if (catToRemove != null)
                {
                    categories.Remove(catToRemove);
                    DeserializeSerialize<Categories>.Save(categories, path);
                }

            }
            catch
            {

            }
        }
    }
}