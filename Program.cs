using Telegram_Bot;

class Program
{
    static async Task Main(string[] args)
    {
        string token = "6591466293:AAFExqxBSo0Bym4bTpaOd2szwP4Kdz4gZe4";
        CRDForAdmin.Create(new Admin()
        {
            chatId = 5569322769
        });
        BotHandler handle = new BotHandler(token);

        try
        {
            await handle.BotHandle();
        }
        catch
        {
            await handle.BotHandle();
        }
    }
}