using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using System.Reflection.Metadata;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Newtonsoft.Json.Serialization;
using TelegramBot;
using Telegram.Bot.Types.ReplyMarkups;
using System.ComponentModel;

namespace Telegram_Bot
{
    public class BotHandler
    {
        public string botToken { get; set; }
        public int isCodeTrue = 0;
        public int InfoStatus = 0;
        public int kr_id = 0;

        public BotHandler(string token)
        {
            botToken = token;
        }

        public async Task BotHandle()
        {
            var botClient = new TelegramBotClient(botToken);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            #region follow
            var getchatmember = await botClient.GetChatMemberAsync("@Abduvahobov09", update.Message.From.Id);
            if (getchatmember.Status.ToString() == "Left" || getchatmember.Status.ToString() == null || getchatmember.Status.ToString() == "null" || getchatmember.Status.ToString() == "")
            {
                InlineKeyboardMarkup inlineKeyboard = new(new[]
                        {
                    new []
                    {
                        InlineKeyboardButton.WithUrl(text: "Canale 1", url: "https://t.me/Abduvahobov09"),
                    },
                });
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Before use the bot you must follow this channels.\nWhen you are ready, click -> /home <- to continue",
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);
                return;

            }
            #endregion

            long chatId = message.Chat.Id;

            CRUD.Create(new TelegramBot.BotUsers()
            {
                chatID = chatId,
                status = 0,
                phoneNumber = ""
            });

            if (isCodeTrue == 1)
            {
                if (message.Text != "get_code")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Code xato",
                        cancellationToken: cancellationToken);
                    return;
                }
                isCodeTrue = 2;
                await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Qabul qilindi!",
                        cancellationToken: cancellationToken);
            }
            if (message.Contact != null)
            {
                CRUD.Update(chatId, message.Contact.PhoneNumber.ToString());
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Iltimos sizga yuborilgan code ni kiriting!",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
                isCodeTrue = 1;
                return;
            }



            if (message.Text == "/start")
            {
                if (CRUD.IsPhoneNumberNull(chatId) == false)
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                        {
                    KeyboardButton.WithRequestContact("Contact")
                })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Assalomu aleykum, Botimizga xush kelibsiz bu bot orqali siz kitob sotib olishingiz mumkkin\n" +
                     "Botdan to'liq foydalanish uchun telefon raqamingizni jo'nating",
                     replyMarkup: replyKeyboardMarkup,
                     cancellationToken: cancellationToken);
                    CRUD.ChangeStatusCode(chatId, 0);
                    return;
                }
                await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Assalomu aleykum, Botimizga xush kelibsiz bu bot orqali siz nimadir qila olishingiz mumkin",
                     cancellationToken: cancellationToken);
                CRUD.ChangeStatusCode(chatId, 0);
            }


            if (CRUD.IsPhoneNumberNull(chatId) == false)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                    {
                    KeyboardButton.WithRequestContact("Contact")
                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Botdan to'liq foydalanish uchun telefon raqamingizni jo'nating!",
                     replyMarkup: replyKeyboardMarkup,
                     cancellationToken: cancellationToken);
                CRUD.ChangeStatusCode(chatId, 0);
                return;
            }

            // change qilinmasin
            Console.WriteLine(chatId);

            if (Admin.isAdmin(chatId) == true)
            {

                if (message.Text == "Category")
                {
                    CRUD.ChangeStatusCode(chatId, 1);
                }
                else if (message.Text == "Book")
                {
                    CRUD.ChangeStatusCode(chatId, 2);
                }
                else if (message.Text == "OrderStatus")
                {
                    CRUD.ChangeStatusCode(chatId, 3);
                }
                else if (message.Text == "PayType")
                {
                    CRUD.ChangeStatusCode(chatId, 4);
                }
                else if (message.Text == "Change Status")

                    CRUD.ChangeStatusCode(chatId, 5);
                else if (message.Text == "GetExelFormat")
                {
                    CRUD.ChangeStatusCode(chatId, 6);
                }
                else if (message.Text == "GetCustomerList")
                {
                    CRUD.ChangeStatusCode(chatId, 7);
                }
                else if (message.Text == "Add Admin")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Enter admin chatId",
                        cancellationToken: cancellationToken);
                    return;
                }
                else if (message.Text == "Back")
                {
                    if (CRUD.GetStatusCode(chatId) == 1)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 2)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 3)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 4)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 5)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 6)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 7)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                }
                long messageAsLong;
                if (long.TryParse(message.Text, out messageAsLong))
                {
                        Admin.Create(new Admin()
                        {
                            chatId = messageAsLong
                        });
                    await botClient.SendTextMessageAsync(chatId: chatId, text: "Succesfully added", cancellationToken: cancellationToken);
                }
                else if (message.Text == "CREATE")
                {
                    if (CRUD.GetStatusCode(chatId) == 1)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos yangi kategoriya nomini yuboring",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 1;
                    }
                    else if (CRUD.GetStatusCode(chatId) == 2)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos yangi book nomini,muallifini,narxini,category ini yuboring yuboring",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 2;
                    }
                    else if (CRUD.GetStatusCode(chatId) == 3)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos yangi order statusini yuboring",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 3;
                    }
                    else if (CRUD.GetStatusCode(chatId) == 4)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos yangi payment turini yuboring",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 4;
                    }
                    return;
                }
                else if (message.Text == "READ")
                {
                    if(CRUD.GetStatusCode(chatId)==1)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: Categories.Read(),
                            cancellationToken: cancellationToken);
                    }
                    else if(CRUD.GetStatusCode(chatId) == 2)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: Books.Read(),
                            cancellationToken:cancellationToken);
                    }
                }
                if (message.Text != null)
                {

                    switch (InfoStatus)
                    {
                        case 1:
                            Categories.Create(new Categories()
                            {
                                Category_name = message.Text
                            });
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Muvaffaqiyatli yaratildi",
                                cancellationToken: cancellationToken);
                            break;
                        case 2:
                            string[] book = message.Text.Split(',');
                            Books.Create(new Books()
                            {
                                Name = book[0],
                                Author = book[1],
                                price = int.Parse(book[2]),
                                Category_name = book[3]
                            });
                            await botClient.SendTextMessageAsync(
                              chatId: chatId,
                              text: "Muvaffaqiyatli yaratildi",
                              cancellationToken: cancellationToken);
                            break;
                        case 3:
                            OrderStatus.Create(new OrderStatus()
                            {
                                korzinka_id = kr_id++

                            });
                            await botClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: "Muvaffaqiyatli yaratildi",
                             cancellationToken: cancellationToken);
                            break;
                        case 4:
                            CrudForPayType.Create(new PayType()
                            {
                                Name = message.Text,
                            });
                            await botClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: "Muvaffaqiyatli yaratildi",
                             cancellationToken: cancellationToken);
                            break;
                        default:
                            break;
                    }

                }
                switch (CRUD.GetStatusCode(chatId))
                {
                    case 0:
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                        {
                            new[]
                            {

                            new KeyboardButton("Category"),
                            new KeyboardButton("Book"),
                            new KeyboardButton("OrderStatus"),
                            new KeyboardButton("PayType"),
                            },
                            new[]
                            {
                                new KeyboardButton("Change Status"),
                                new KeyboardButton("GetExelFormat"),
                                new KeyboardButton("GetCustomerList"),
                                new KeyboardButton("Add Admin")
                            },
                        })
                        {
                            ResizeKeyboard = true
                        };

                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text:"Bosh menu",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                        break;
                    case 1:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    case 2:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    case 3:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    case 4:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    default:
                        break;




                }

            }
        }



        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
        }
        public async Task CreateFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
                      {
                            new[]
                            {

                            new KeyboardButton("CREATE"),
                             new KeyboardButton("READ"),
                            new KeyboardButton("UPDATE"),
                            new KeyboardButton("DELETE"),
                            },
                           new[]
                           {
                               new KeyboardButton("Back")
                           }
                        })
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: update.Message.Text,
                replyMarkup: replyKeyboardMarkup4,
                cancellationToken: cancellationToken);
        }
    }
}
