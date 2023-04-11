using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.commands;

public class PingCommand
{
    public static SlashCommandBuilder GetCommand()
    {
        var command = new SlashCommandBuilder();
        command.WithName("ping");
        command.WithDescription("Pong!");
        command.DefaultMemberPermissions = GuildPermission.BanMembers;
        return command;
    }
    
    public static async Task HandleCommand(SocketSlashCommand command)
    {
        await command.RespondAsync("Pong!");
    }
}