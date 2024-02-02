using Telegram_Bot;

class Program
{
    static async Task Main(string[] args)
    {
        string token = "";
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