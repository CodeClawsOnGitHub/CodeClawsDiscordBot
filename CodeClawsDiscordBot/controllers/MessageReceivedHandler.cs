using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.controllers;

public class MessageReceivedHandler
{
    public static async Task MessageReceived(SocketMessage message)
    {
        if (message.Author.IsBot)
        {
            return;
        }
        
        SocketGuildChannel channel = (SocketGuildChannel)message.Channel;
        string messageContent = message.Content;
        int messageLength = messageContent.Length;

        MessageFilter messageFilter = new MessageFilter();
        FilterResult filterResult = messageFilter.FilterMessage(message);
        
        if (filterResult.IsFiltered)
        {
            switch (filterResult.Reason)
            {
                case "caps":

                    await message.Author.SendMessageAsync(
                        $"Hey {message.Author.Username}, please don't spam caps in {channel.Guild.Name}!");
                    await message.DeleteAsync();
                    break;

                case "spam":
                    await message.Author.SendMessageAsync(
                        $"Hey {message.Author.Username}, you have been muted for 5 minutes due to spamming {channel.Guild.Name}!");
                    SocketGuildUser user = channel.Guild.GetUser(message.Author.Id);
                    await user.SetTimeOutAsync(TimeSpan.FromMinutes(5));
                    var textChannel = (ITextChannel)channel;
                    var messages = await textChannel.GetMessagesAsync(10).FlattenAsync();
                    await message.DeleteAsync();
                    foreach (var msg in messages)
                    {
                        if (msg.Author.Id == message.Author.Id)
                        {
                            await msg.DeleteAsync();
                        }
                    }
                    break;

                case "invalidChar":
                    await message.Author.SendMessageAsync(
                        $"Hey {message.Author.Username}, too many invalid characters in message. From {channel.Guild.Name}!");
                    await message.DeleteAsync();
                    break;
                
                case "none":
                    break;
            }
        }
    }
}