using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.commands;

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
        }
    }
}