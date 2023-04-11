using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.controllers;

public class OnReadyHandler
{
    public static DiscordSocketClient Client { get; set; }
    public static async Task Ready()
    {
        Console.Out.WriteLine("Client ready!");
        await Client.SetGameAsync("with code", null, ActivityType.Playing);
        // var globalCommands = Client.GetGlobalApplicationCommandsAsync();
        // foreach (var command in globalCommands.Result)
        // {
        //     if (command.Name == "first-global-command")
        //     {
        //         await command.DeleteAsync();
        //     }
        // }
    }
}