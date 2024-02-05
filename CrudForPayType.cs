using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class PayType
    {
        public string cash { get; set; }

        public static string path = @"C:\Users\user\Desktop\DatabseFolders\PayTypes.json";

        public static void Create(PayType pay)
        {
            List<PayType> categories = DeserializeSerialize<PayType>.GetAll(path);
            if (categories.Any(c => c.cash == pay.cash))
            {
                return;
            }
            categories.Add(pay);
            DeserializeSerialize<PayType>.Save(categories, path);
        }
        public static string Read()
        {
            string str = "";
            List<PayType> categories = DeserializeSerialize<PayType>.GetAll(path);
            foreach (PayType c in categories)
            {
                str += $"Name: {c.cash}\n";
            }
            return str;
        }

        public static void Update(string last_name, string new_name)
        {
            try
            {
                List<PayType> categories = DeserializeSerialize<PayType>.GetAll(path);
                if (categories != null)
                {
                    int index = categories.FindIndex(name => name.cash == last_name);
                    if (index != -1)
                    {
                        categories[index].cash = new_name;
                        DeserializeSerialize<PayType>.Save(categories, path);

                    }
                }
            }
            catch { }
        }

        public static void Delete(string del_name)
        {
            try
            {
                List<PayType> categories = DeserializeSerialize<PayType>.GetAll(path);
                var catToRemove = categories.Find(ct => ct.cash == del_name);

                if (catToRemove != null)
                {
                    categories.Remove(catToRemove);
                    DeserializeSerialize<PayType>.Save(categories, path);
                }
            }
            catch { }
        }
    }
}

