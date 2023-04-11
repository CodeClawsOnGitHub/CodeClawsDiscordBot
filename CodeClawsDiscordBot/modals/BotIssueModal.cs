using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CodeClawsDiscordBot.modals;

public class SelectIssueComponent: IMessageComponent
{
    public string CustomId { get; set; }
    public List<SelectMenuOption> Options { get; set; }
    public int MinValues { get; set; }
    public int MaxValues { get; set; }
    public string Placeholder { get; set; }
    public ComponentType Type { get; set; }
    public string Label { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }
    public Emoji Emoji { get; set; }
    public string Url { get; set; }
    public List<IMessageComponent> Components { get; set; }
    public string Style { get; set; }
    public string Value { get; set; }
}

public class BotIssueModal
{
    public static ModalBuilder getModal()
    {
        return new ModalBuilder()
            .WithTitle("Submit a bot issue")
            .WithCustomId("bot_issue_modal")
            .AddTextInput("Title", "issue_title", placeholder: "Issue Title")
            .AddTextInput("Issue Type", "issue_type", placeholder: "BUG or SUGGESTION")
            .AddTextInput("Issue Description", "issue_description",
                placeholder: "Please describe the issue or suggestion");
    }

    public static async Task HandleModalSubmission(SocketMessageComponent interaction)
    {
        throw new System.NotImplementedException();
    }
}