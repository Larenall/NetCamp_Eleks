using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Application.Common.Interfaces;
using System;

namespace UI
{
    public class LarenallTelegramBot
    {
        const string token  = "2028671026:AAHFuCYLc2BXj2vojSGDzw2DeHPtJSyt5bY";

        TelegramBotClient client;

        private readonly IExternalCryptoAPI cryptoAPI;

        public LarenallTelegramBot( IExternalCryptoAPI _api)
        {
            cryptoAPI = _api;
        }

        public void StartBot()
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            cryptoAPI.SendMessage += MessageHandler;
            cryptoAPI.StartCheckingForChanges();
        }

        async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                switch (msg.Text)
                {
                    case "Update me on BTC":
                        var result = cryptoAPI.SignUserForUpdates(msg.Chat.Id,"BTC");
                        await client.SendTextMessageAsync(msg.Chat.Id, result);
                        break;
                    case "Stop updating on BTC":
                        result = cryptoAPI.CancelUpdates(msg.Chat.Id, "BTC");
                        await client.SendTextMessageAsync(msg.Chat.Id, result);
                        break;
                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Pick the command: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }
        public async void MessageHandler(long ChatId, string msg)
        {
            await client.SendTextMessageAsync(ChatId, msg, replyMarkup: GetButtons());
        }
        static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Subscribe for updates"},new KeyboardButton { Text = "Unsubscribe from updates"} },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Update me on BTC" },new KeyboardButton { Text = "Stop updating on BTC" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Give me some data" } }
                }
            };
        }
    }
}

