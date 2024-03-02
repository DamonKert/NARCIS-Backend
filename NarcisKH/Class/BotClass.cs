using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Data;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;

namespace NarcisKH.Class
{
    public class BotClass
    {
        private readonly Dictionary<long, UserRegistrationState> userStates = new Dictionary<long, UserRegistrationState>();
        private readonly Dictionary<long, UserData> userStateData = new Dictionary<long, UserData>();

        private readonly TelegramBotClient _botClient;
        private readonly NarcisKHContext _context;

        public BotClass(TelegramBotClient? botClient, NarcisKHContext context)
        {
            _botClient = botClient;
            _context = context;
        }
        public async Task StartReceiever()
        {
            var token = new CancellationTokenSource();
            var cancellationToken = token.Token;
            var ReOpt = new ReceiverOptions
            {
                AllowedUpdates = { } 
            };
            await _botClient.ReceiveAsync(onMessage, ErrorMessage, ReOpt, cancellationToken);

        }
        // Error Message
        public async Task ErrorMessage(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ApiRequestException requestException)
            {
                await botClient.SendTextMessageAsync(123456789, "Error: " + exception.Message);
            }
        }

        // Send chat ID function
        private async Task SendChatId(long chatId)
        {
            await _botClient.SendTextMessageAsync(chatId, $"Your chat ID is: {chatId}");
        }

        public async Task SendMessage()
        {
            await _botClient.SendTextMessageAsync(5867925791, "Someone Just made an order");
        }
        public async Task onMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is Message msg)
            {
                // Check if the message is a command and handle "/getId"
                if (msg.Text != null && msg.Text.StartsWith("/getId"))
                {
                    await SendChatId(msg.Chat.Id);
                }
                else if (msg.Text != null && msg.Text.StartsWith("/login"))
                {
                    // Start the login process
                    await botClient.SendTextMessageAsync(msg.Chat.Id, "What's your username?");
                    // Set the user state to expect the username
                    userStates[msg.Chat.Id] = UserRegistrationState.ExpectingUsername;
                }
                else if (userStates.TryGetValue(msg.Chat.Id, out var currentState))
                {
                    switch (currentState)
                    {
                        case UserRegistrationState.ExpectingUsername:
                            // Process the username and update the state
                            var username = msg.Text;
                            // Store the username or process it as needed (e.g., store in the database)
                            await botClient.SendTextMessageAsync(msg.Chat.Id, "Great! Now, please provide your password.");
                            // Set the user state to expect the password
                            userStates[msg.Chat.Id] = UserRegistrationState.ExpectingPassword;
                            // Save the username in the user state
                            userStateData[msg.Chat.Id] = new UserData { Username = username };
                            break;

                        case UserRegistrationState.ExpectingPassword:
                            // Process the password and update the state
                            var password = msg.Text;
                            // Retrieve the stored username from user state
                            if (userStateData.TryGetValue(msg.Chat.Id, out var userData))
                            {
                                var usernameString = userData.Username;
                                // Store the password or process it as needed (e.g., store in the database)
                                // Clear the user state
                                userStates.Remove(msg.Chat.Id);
                                userStateData.Remove(msg.Chat.Id);

                                try
                                {
                                   var user = await _context.User.FirstOrDefaultAsync(u => u.Username == usernameString && u.Password == password);

                                    await botClient.SendTextMessageAsync(msg.Chat.Id, "Login complete!");

                                    if (user != null)
                                    {
                                        await botClient.SendTextMessageAsync(msg.Chat.Id, "Login successful!");
                                        user.ChatId = msg.Chat.Id.ToString();
                                        _context.User.Update(user);
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        await botClient.SendTextMessageAsync(msg.Chat.Id, "Invalid username or password. Please try again.");
                                    }
                                }catch(Exception e)
                                {
                                    await botClient.SendTextMessageAsync(msg.Chat.Id, "Unexpected error. "+e.Message );
                                }
                                
                            }
                            else
                            {
                                // Handle the case where the username is not found in the user state data
                                await botClient.SendTextMessageAsync(msg.Chat.Id, "Unexpected error. Please try again.");
                            }
                            break;

                            // Add more cases for additional states if needed
                    }
                }
            }
        }

        public class UserData
        {
            public string Username { get; set; }
            // Add more user data properties as needed
        }
        public enum UserRegistrationState
        {
            ExpectingUsername,
            ExpectingPassword,
            // Add more states as needed
        }
    }
    }

