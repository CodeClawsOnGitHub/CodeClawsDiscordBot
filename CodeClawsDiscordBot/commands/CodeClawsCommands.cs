using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.commands;

public class CodeClawsCommands
{
    public static SlashCommandBuilder GetCommand()
    {
        var command = new SlashCommandBuilder();
        command.WithName("codeclaws");
        command.WithDescription("CodeClaws Discord Bot");
        command.DefaultMemberPermissions = GuildPermission.BanMembers;
        command.AddOption(new SlashCommandOptionBuilder()
            .WithName("add_embed")
            .WithDescription("Add an embed to the channel")
            .WithType(ApplicationCommandOptionType.SubCommandGroup)
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("bot_issue_embed")
                .WithDescription("Add a bot issue embed to the channel")
                .WithType(ApplicationCommandOptionType.SubCommand)
            )
        );
        return command;
    }
    
    public static async Task HandleCommand(SocketSlashCommand command)
    {
        var options = command.Data.Options.ToArray(); ;
        
            switch (options[0].Name)
            {
                case "add_embed":
                    await AddEmbed(command);
                    break;
                default:
                    await command.RespondAsync($"Unknown command! {options[0].Name}");
                    break;
            }
    }
    
    private static async Task AddEmbed(SocketSlashCommand command)
    {
        var options = command.Data.Options.ToArray();
        
            switch (options[0].Options.ToArray()[0].Name)
            {
                case "bot_issue_embed":
                    Console.Out.WriteLine("Bot issue embed");
                    await AddBotIssueEmbed(command);
                    break;
                default:
                    await command.RespondAsync($"Unknown command! {options[0].Options.ToArray()[0].Name}");
                    break;
            }
    }
    
    private static async Task AddBotIssueEmbed(SocketSlashCommand command)
    {
        var embed = new EmbedBuilder();
        embed.WithTitle("Bot Issues And Suggestions");
        embed.WithDescription("If you have any issues with the bot or have any suggestions for the bot, please click create issue and fill out the form.");
        embed.WithColor(Color.Red);
        embed.WithFooter("CodeClaws Discord Bot");
        embed.WithCurrentTimestamp();

        var buttonComponent = new ComponentBuilder()
            .WithSelectMenu(new SelectMenuBuilder()
                .WithPlaceholder("Select an option")
                .WithCustomId("issue_type")
                .WithMinValues(1)
                .WithMaxValues(1)
                .AddOption("Bug Report", "bug_report", "Report a bug with the bot")
                .AddOption("Feature Request", "feature_request", "Request a feature for the bot")
            );
        
        
        await command.RespondAsync(embed: embed.Build(), components: buttonComponent.Build());
    }
}