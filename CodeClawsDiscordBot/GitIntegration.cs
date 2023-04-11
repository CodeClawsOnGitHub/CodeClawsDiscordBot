using System;
using System.Net;
using System.Threading.Tasks;
using Octokit;

namespace CodeClawsDiscordBot;

public class GitIntegration
{
    static GitHubClient _client = new GitHubClient(new ProductHeaderValue("CodeClawsDiscordBot"));
    static Credentials _authToken = new Credentials(Environment.GetEnvironmentVariable("GITHUB_TOKEN"));
    static string _owner = "CodeClawsOnGitHub";
    
    
    private static async Task<Issue> CreateIssue(string title, string body, string label)
    {
        _client.Credentials = _authToken;
        var newIssue = new NewIssue(title);
        newIssue.Body = body;
        newIssue.Labels.Add(label);
        var issue = await _client.Issue.Create(_owner, "CodeClawsDiscordBot", newIssue);
        await Console.Out.WriteLineAsync(issue.HtmlUrl);
        return issue;
    }
    
    public static async Task<Issue> CreateBugReport(string title, string body)
    {
        return await CreateIssue(title, body, "bug");
    }
    
    public static async Task<Issue> CreateFeatureRequest(string title, string body)
    {
        return await CreateIssue(title, body, "enhancement");
    }
}
