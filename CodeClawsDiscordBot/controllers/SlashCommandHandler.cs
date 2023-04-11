using System;
using System.Threading.Tasks;
using CodeClawsDiscordBot.commands;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.controllers;

public class SlashCommandHandler
{
    public static async Task Handle(SocketSlashCommand command)
    {
        Console.Out.WriteLine("Slash command executed!");
        switch (command.CommandName)
        {
            case "ping":
                await PingCommand.HandleCommand(command);
                break;
            case "codeclaws":
                await CodeClawsCommands.HandleCommand(command);
                break;
            
            default:
                Console.Out.WriteLine("Unknown command!");
                await command.RespondAsync($"{command.CommandName}");
                break;
        }
    }
}