﻿using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using Telegram.Bot.Types;
using TelegramBot;

namespace Telegram_Bot
{
    public static class CrudForOrderStatus
    {
        public static string path = @"C:\Users\user\Desktop\Categpries.json";

        public static void Create(Categorie ct)
        {
            List<Categorie> categories = GetAllCats();
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
            List<Categorie> categories = GetAllCats();
            foreach (Categorie c in categories)
            {
                sb.Append(c.Category_name + " ");
            }
            return sb.ToString();
        }

        public static void Update(string new_name)
        {
            try
            {
                List<Categorie> categories = GetAllCats();
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
                List<Categorie> categories = GetAllCats();
                var catToRemove = categories.Find(ct => ct.Category_name == del_name);

                if (catToRemove != null)
                {
                    categories.Remove(catToRemove);
                    SaveCats(categories);
                }
            }
            catch { }
        }
        public static List<Categorie> GetAllCats()
        {

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Categorie>>(json) ?? new List<Categorie>();
            }
            else
            {
                return new List<Categorie>();
            }
        }
        public static void SaveCats(List<Categorie> categories) 
        {
            string json = JsonSerializer.Serialize(categories);
            System.IO.File.WriteAllText(path, json);
        }
    }
    public class Categorie
    {
        public string Category_name;
        public long price;
    }
}
