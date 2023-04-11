using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeClawsDiscordBot.modals;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.controllers;

public class ModalSubmissionHandler
{
    public static async Task Handle(SocketModal modal)
    {
        List<SocketMessageComponentData> components = modal.Data.Components.ToList();
        string issueType = components.First(x => x.CustomId == "issue_type").Value;
        string issueDescription = components.First(x => x.CustomId == "issue_description").Value;
        //force the issue type to be uppercase
        issueType = issueType.ToUpper();
        // see if the issue type is valid
        if (issueType != "BUG" && issueType != "SUGGESTION")
        {
            await modal.RespondAsync("Invalid issue type! Please use BUG or SUGGESTION", ephemeral: true);
            return;
        }
        await modal.RespondAsync("Thank you for submitting your issue!", ephemeral: true);
    }
}