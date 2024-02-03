using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Telegram_Bot
{
    public class Admin
    {
        public long chatId { get; set; }

        public static string path = @"C:\Users\user\Desktop\DatabseFolders\Admins.json";

        public static void Create(Admin admiin)
        {
            List<Admin> admins = DeserializeSerialize<Admin>.GetAll(path);
            if (admins.Any(c => c.chatId == admiin.chatId))
            {
                return;
            }
            admins.Add(admiin);
            DeserializeSerialize<Admin>.Save(admins, path);
        }

        public static string Read()
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Admin> admins = DeserializeSerialize<Admin>.GetAll(path);
            foreach (Admin admin in admins)
            {
                stringBuilder.Append($"{admin.chatId}\n");
            }
            return stringBuilder.ToString();
        }
        public static bool isAdmin(long chatId)
        {
            List<Admin> admins = DeserializeSerialize<Admin>.GetAll(path);
            if (admins.Any(c => c.chatId == chatId))
            {
                return true;
            }
            return false;
        }
        public static void Delete(long chatId)
        {
            try
            {
                List<Admin> admins = DeserializeSerialize<Admin>.GetAll(path);
                var catToRemove = admins.Find(ct => ct.chatId == chatId);

                if (catToRemove != null)
                {
                    admins.Remove(catToRemove);
                    DeserializeSerialize<Admin>.Save(admins, path);
                }
            }
            catch { }
        }
}
