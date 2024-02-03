using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using Telegram.Bot.Types;
using TelegramBot;
using Telegram_Bot;
namespace Telegram_Bot
{
    public class OrderStatus
    {
        public static string path = @"C:\Users\user\Desktop\DatabseFolders\OrderSatus.json";

        public static void Create(OrderStatus ct)
        {
            List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
            if (orders.Any(c => c.korzinka_id == ct.korzinka_id))
            {
                return;
            }
            orders.Add(ct);
            DeserializeSerialize<OrderStatus>.Save(orders,path);
        }
        public static string Read()
        {
            StringBuilder sb = new StringBuilder();
            List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
            foreach (OrderStatus c in orders)
            {
                sb.Append($"Korzinka_id: {c.korzinka_id}\nOrder Status: {c.status}");
            }
            return sb.ToString();
        }

        public static void Update(int id, string new_status)
        {
            try
            {
                List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
                if (orders != null)
                {
                    int index = orders.FindIndex(name => name.korzinka_id == id);
                    if (index != -1)
                    {
                        orders[index].status = new_status;
                        DeserializeSerialize<OrderStatus>.Save(orders, path);
                    }
                }
            }
            catch { }
        }

        public static void Delete(int id)
        {
            try
            {
                List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
                var catToRemove = orders.Find(ct => ct.korzinka_id == id);

                if (catToRemove != null)
                {
                    orders.Remove(catToRemove);
                    DeserializeSerialize<OrderStatus>.Save(orders, path);
                }
            }
            catch { }
        }
        public int korzinka_id { get; set; }
        public string status { get; set; }
    }
}
