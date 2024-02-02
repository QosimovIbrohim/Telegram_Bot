using Telegram_Bot;

class Program
{
    static async Task Main(string[] args)
    {
        string token = "6591950726:AAEwwdHl_O4lU48GyIqqXrJTAu1V-ImgqoI";

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