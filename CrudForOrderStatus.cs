using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using Telegram.Bot.Types;
using TelegramBot;

namespace Telegram_Bot
{
    public static class CrudForOrderStatus
    {
        public static string path = @"C:\Users\user\Desktop\OrderSatus.json";

        public static void Create(OrderStatus ct)
        {
            List<OrderStatus> orders = GetAllCats();
            if (orders.Any(c => c.korzinka_id == ct.korzinka_id))
            {
                return;
            }
            orders.Add(ct);
            SaveCats(orders);
        }
        public static string Read()
        {
            StringBuilder sb = new StringBuilder();
            List<OrderStatus> orders = GetAllCats();
            foreach (OrderStatus c in orders)
            {
                sb.Append($"Korzinka_id: {c.korzinka_id}\nOrder Status: {c.status}");
            }
            return sb.ToString();
        }

        public static void Update(int id,string new_status)
        {
            try
            {
                List<OrderStatus> orders = GetAllCats();
                if (orders != null)
                {
                    int index = orders.FindIndex(name => name.korzinka_id == id);
                    if (index != -1)
                    {
                        orders[index].status = new_status;
                        SaveCats(orders);
                    }
                }
            }
            catch { }
        }

        public static void Delete(int id)
        {
            try
            {
                List<OrderStatus> orders = GetAllCats();
                var catToRemove = orders.Find(ct => ct.korzinka_id == id);

                if (catToRemove != null)
                {
                    orders.Remove(catToRemove);
                    SaveCats(orders);
                }
            }
            catch { }
        }
        public static List<OrderStatus> GetAllCats()
        {

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<OrderStatus>>(json) ?? new List<OrderStatus>();
            }
            else
            {
                return new List<OrderStatus>();
            }
        }
        public static void SaveCats(List<OrderStatus> orders)
        {
            string json = JsonSerializer.Serialize(orders);
            System.IO.File.WriteAllText(path, json);
        }
    }
    public class OrderStatus
    {
        public int korzinka_id;
        public string status;
    }
}
