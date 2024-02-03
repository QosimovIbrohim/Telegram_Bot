using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace TelegramBot
{
    public static class CRUD
    {
        public static string filePath = @"C:\Users\user\Desktop\DatabseFolders\Database.json";

        public static void Create(BotUsers chat)
        {
            List<BotUsers> chats = GetAllChats();
            if (chats.Any(c => c.chatID == chat.chatID))
            {
                return;
            }
            chats.Add(chat);
            SaveChats(chats);
        }


        public static string Read(long chatId)
        {
            List<BotUsers> chats = GetAllChats();
            var chat = chats.Find(c => c.chatID == chatId);

            if (chat != null)
            {
                return $"{chat.chatID}:{chat.phoneNumber}";
            }
            else
            {
                return $"{chat.chatID}:{chat.phoneNumber}";
            }
        }
        public static bool IsPhoneNumberNull(long chatId)
        {
            List<BotUsers> chats = GetAllChats();
            var chat = chats.Find(c => c.chatID == chatId);

            if (chat != null && chat.phoneNumber != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Update(long chatId, string newPhoneNumber)
        {
            try
            {
                List<BotUsers> users = GetAllChats();

                if (users != null)
                {
                    int index = users.FindIndex(u => u.chatID == chatId);


                    if (index != -1)
                    {
                        users[index].phoneNumber = newPhoneNumber;

                        SaveChats(users);
                    }
                }
            }
            catch
            {

            }

        }

        public static void Delete(long chatId)
        {
            List<BotUsers> chats = GetAllChats();
            var chatToRemove = chats.Find(c => c.chatID == chatId);

            if (chatToRemove != null)
            {
                chats.Remove(chatToRemove);
                SaveChats(chats);
            }
        }

        private static List<BotUsers> GetAllChats()
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<BotUsers>>(json) ?? new List<BotUsers>();
            }
            else
            {
                return new List<BotUsers>();
            }
        }
        public static int GetStatusCode(long chatId)
        {

            List<BotUsers> users = GetAllChats();
            BotUsers? chatToRemove = users.Find(c => c.chatID == chatId);

            return chatToRemove.status;
        }

        public static void ChangeStatusCode(long chatId,int statusCode)
        {

            List<BotUsers> users = GetAllChats();
            int index = users.FindIndex(u => u.chatID == chatId);
            if(index != -1)
            {
                users[index].status = statusCode;
            }
            SaveChats(users);
        }

        public static List<BotUsers> GetAll()
        {
            return GetAllChats();
        }

        private static void SaveChats(List<BotUsers> chats)
        {
            string json = JsonSerializer.Serialize(chats);
            System.IO.File.WriteAllText(filePath, json);
        }
    }
    public class BotUsers
    {
        public long chatID { get; set; }

        public int status { get; set; }
        public string? phoneNumber { get; set; }
    }
}
