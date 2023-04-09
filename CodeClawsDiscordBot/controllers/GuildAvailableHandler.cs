using System;
using System.Threading.Tasks;
using CodeClawsDiscordBot.commands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace CodeClawsDiscordBot.controllers;

public class GuildAvailableHandler
{
    public static async Task GuildAvailable(SocketGuild guild)
    {
        await Console.Out.WriteLineAsync("Guild available!");
        try
        {
            // Now that we have our builder, we can call the CreateApplicationCommandAsync method to make our slash command.
            var pingCommand = PingCommand.GetCommand();
            await guild.CreateApplicationCommandAsync(pingCommand.Build());
        }
        catch(HttpException exception)
        {
            // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

            // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
            Console.WriteLine(json);
        }
    }
}