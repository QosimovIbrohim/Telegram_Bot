using System.Text.Json;

namespace Telegram_Bot
{
    public static class DeserializeSerialize<T>
    {
        public static List<T> GetAll(string path)
        {

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            else
            {
                return new List<T>();
            }
        }
        public static void Save(List<T> books,string path)
        {
            string json = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText(path, json);
        }
    }
}
