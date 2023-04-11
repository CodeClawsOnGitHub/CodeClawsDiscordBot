using System;
using System.Threading.Tasks;
using CodeClawsDiscordBot.modals;
using Discord.Interactions.Builders;
using Discord.WebSocket;
using ModalBuilder = Discord.ModalBuilder;

namespace CodeClawsDiscordBot.controllers;

public class ButtonHandler
{
    public static async Task Handle(SocketMessageComponent interaction)
    {
        switch (interaction.Data.CustomId)
        {
            case "create_issue":
                //create a modal
                ModalBuilder  mb = BotIssueModal.getModal();
                await interaction.RespondWithModalAsync(mb.Build());
                break;
                
        }
    }
}