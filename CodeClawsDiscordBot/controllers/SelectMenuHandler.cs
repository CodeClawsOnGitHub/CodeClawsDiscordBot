using System.Threading.Tasks;
using CodeClawsDiscordBot.modals;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.controllers;

public class SelectMenuHandler
{
    public static async Task Handle(SocketMessageComponent interaction)
    {
        switch (interaction.Data.CustomId)
        {
            case "issue_type":
                //create a modal
                var text = string.Join(", ", interaction.Data.Values);
                await interaction.RespondAsync($"You have selected {text}", ephemeral: true);
                break;
                
        }
    }
}