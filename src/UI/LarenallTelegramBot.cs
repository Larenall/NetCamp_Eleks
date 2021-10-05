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
        const string token = "2028671026:AAHFuCYLc2BXj2vojSGDzw2DeHPtJSyt5bY";

        TelegramBotClient client;

        private readonly IExternalCryptoAPI cryptoAPI;

        public LarenallTelegramBot(IExternalCryptoAPI _api)
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
            client.OnCallbackQuery += Client_OnCallbackQuery;
        }

        async private void Client_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            string answer;
            switch (e.CallbackQuery.Data)
            {
                case "Sub":
                    answer = cryptoAPI.SubUserForUpdates(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.ReplyToMessage.Text);
                    await client.AnswerCallbackQueryAsync(e.CallbackQuery.Id, answer);
                    break;
                case "Unsub":
                    answer = cryptoAPI.UnsubUserFromUpdates(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.ReplyToMessage.Text);
                    await client.AnswerCallbackQueryAsync(e.CallbackQuery.Id, answer);
                    break;
                case "Info":
                    answer = await cryptoAPI.GetInfoOnCurrency(e.CallbackQuery.Message.ReplyToMessage.Text);
                    await client.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id,answer);
                    //await client.EditMessageTextAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId, answer, replyMarkup: MainMarkup);
                    //await client.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "No new info yet");
                    break;
                case "Back":
                    await client.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);
                    await client.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Enter the symbol for the currency. Or maybe you need some help?", replyMarkup: HelpMarkup);
                    break;
                case "Symbols":
                    answer = await cryptoAPI.GetCryptoSymbols();
                    await client.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, answer, replyMarkup: null);
                    break;
            }
        }

        async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                if (await cryptoAPI.AssetExists(msg.Text))
                {
                    await client.SendTextMessageAsync(msg.Chat.Id, $"You selected - {msg.Text}", replyToMessageId: msg.MessageId, replyMarkup: MainMarkup);
                }
                else
                {
                    await client.SendTextMessageAsync(msg.Chat.Id, "Enter the symbol for the currency. Or maybe you need some help?",replyMarkup: HelpMarkup);
                }
            }
        }
        public async void MessageHandler(long ChatId, string msg)
        {
            await client.SendTextMessageAsync(ChatId, msg);
        }

        InlineKeyboardMarkup HelpMarkup = new InlineKeyboardMarkup(

        new List<List<InlineKeyboardButton>>
        {
                new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData(
                    "Get me some symbols",
                    "Symbols"
                    )
                }
        }
        );
        InlineKeyboardMarkup MainMarkup = new InlineKeyboardMarkup(

        new List<List<InlineKeyboardButton>>
        {
                new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData(
                    "Subscribe for changes on price",
                    "Sub"
                    )
                },
                new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData(
                    "Unsubscribe from changes on price",
                    "Unsub"
                    )
                },
                new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData(
                    "Get me some info on it",
                    "Info"
                    )
                },
                new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData(
                    "Select other currency",
                    "Back"
                    )
                }

        }
        );
    }
}

