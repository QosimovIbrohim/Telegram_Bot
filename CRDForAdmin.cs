using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Telegram_Bot
{
    public static class CRDForAdmin
    {
        public static string path = @"C:\Users\user\Desktop\DatabseFolders\Admins.json";

        public static void Create(Admin admiin)
        {
            List<Admin> admins = GetAllAdmins();
            if (admins.Any(c => c.chatId == admiin.chatId))
            {
                return;
            }
            admins.Add(admiin);
            SaveAdmins(admins);
        }

        public static string Read()
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Admin> admins = GetAllAdmins();
            foreach (Admin admin in admins)
            {
                stringBuilder.Append($"{admin.chatId}\n");
            }
            return stringBuilder.ToString();
        }
        public static bool isAdmin(long chatId)
        {
            List<Admin> admins = GetAllAdmins();
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
                List<Admin> admins = GetAllAdmins();
                var catToRemove = admins.Find(ct => ct.chatId == chatId);

                if (catToRemove != null)
                {
                    admins.Remove(catToRemove);
                    SaveAdmins(admins);
                }
            }
            catch { }
        }
        public static List<Admin> GetAllAdmins()
        {

            if (System.IO.File.Exists(path))
            {
                using (StreamReader sm = new StreamReader(path))
                {
                    string json = sm.ReadToEnd();
                    if(!string.IsNullOrEmpty(json))
                        return JsonSerializer.Deserialize<List<Admin>>(json)!;
                    else
                        return new List<Admin>();
                }
            }
            else
            {
                return new List<Admin>();
            }
        }
        public static void SaveAdmins(List<Admin> admins)
        {
            string json = JsonSerializer.Serialize<List<Admin>>(admins);
            using (StreamWriter sw = new StreamWriter(path))
            {
               sw.Write(json);
            }
        }

    }

    public class Admin
    {
        
        public long chatId { get; set; }
    }
}
