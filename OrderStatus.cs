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
        public string status { get; set; }
        public static string path = @"C:\Users\user\Desktop\DatabseFolders\OrderSatus.json";

        public static void Create(OrderStatus ct)
        {
            List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
            if (orders.Any(c => c.status == ct.status))
            {
                return;
            }
            orders.Add(ct);
            DeserializeSerialize<OrderStatus>.Save(orders, path);
        }
        public static string Read()
        {
            StringBuilder sb = new StringBuilder();
            List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
            foreach (OrderStatus c in orders)
            {
                sb.Append($"Order Status: {c.status}\n");
            }
            return sb.ToString();
        }

        public static void Update(string name, string new_status)
        {
            try
            {
                List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
                if (orders != null)
                {
                    int index = orders.FindIndex(ord => ord.status == name);
                    if (index != -1)
                    {
                        orders[index].status = new_status;
                        DeserializeSerialize<OrderStatus>.Save(orders, path);
                    }
                }
            }
            catch { }
        }

        public static void Delete(string name)
        {
            try
            {
                List<OrderStatus> orders = DeserializeSerialize<OrderStatus>.GetAll(path);
                var catToRemove = orders.Find(ct => ct.status == name);

                if (catToRemove != null)
                {
                    orders.Remove(catToRemove);
                    DeserializeSerialize<OrderStatus>.Save(orders, path);
                }
            }
            catch { }
        }
    }
}
