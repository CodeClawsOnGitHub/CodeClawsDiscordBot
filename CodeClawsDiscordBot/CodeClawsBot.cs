using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeClawsDiscordBot.commands;
using CodeClawsDiscordBot.controllers;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace CodeClawsDiscordBot;

public struct MessageCount{
    public int messageCount;
    public DateTime lastMessageTime;
    public DateTime trackingStartTime;
}

public class CodeClawsBot
{
    public static Task Main(string[] args) => new CodeClawsBot().MainAsync();

    private DiscordSocketClient _client;



    public async Task MainAsync()
    {
        DiscordSocketConfig config = new()
        {
            UseInteractionSnowflakeDate = false,
            GatewayIntents = GatewayIntents.All

        };
        _client = new DiscordSocketClient(config);

        _client.Log += Log;
        OnReadyHandler.Client = _client;
        _client.Ready += OnReadyHandler.Ready;
        _client.SlashCommandExecuted += SlashCommandHandler.Handle;
        _client.MessageReceived += MessageReceivedHandler.MessageReceived;
        _client.GuildAvailable += GuildAvailableHandler.GuildAvailable;
        _client.ButtonExecuted += ButtonHandler.Handle;
        _client.ModalSubmitted += ModalSubmissionHandler.Handle;
        _client.SelectMenuExecuted += SelectMenuHandler.Handle;

        string token = Environment.GetEnvironmentVariable("BOT_TOKEN");
        

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
    
}