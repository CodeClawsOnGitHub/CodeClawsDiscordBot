using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;

namespace CodeClawsDiscordBot;

public struct FilterResult
{
    public bool IsFiltered;
    public string Reason;
}

public class MessageFilter
{
    
    
    private Dictionary<ulong,MessageCount> _messageCount = new();
    IMessage _currentMessage = null;
    
    bool CharacterFilter()
    {   string messageContent = _currentMessage.Content;
        int count = 0;
        for (int i = 0; i < messageContent.Length; i++)
        {
            if (messageContent[i] > 8000)
            {
                count++;
            }
        }
        if (count > 3)
        {
            return true;
        }
        return false;
    }
    
    bool SpamFilter()
    {
        string messageContent = _currentMessage.Content;
        int messageLength = messageContent.Length;
        if (_messageCount.ContainsKey(_currentMessage.Author.Id))
        {
            MessageCount messageCount = _messageCount[_currentMessage.Author.Id];
            if (messageCount.messageCount > 5 && messageCount.trackingStartTime.AddMinutes(5) > DateTime.Now)
            {
                if (messageCount.lastMessageTime.AddSeconds(2) > DateTime.Now)
                {
                    _messageCount.Remove(_currentMessage.Author.Id);
                    return true;
                }
                messageCount.messageCount = 0;
                messageCount.trackingStartTime = DateTime.Now;
            }
            messageCount.messageCount++;
            messageCount.lastMessageTime = DateTime.Now;
            _messageCount[_currentMessage.Author.Id] = messageCount;
        }
        else
        {
            MessageCount newMessageCount = new MessageCount
            {
                messageCount = 1,
                lastMessageTime = DateTime.Now,
                trackingStartTime = DateTime.Now
            };
            _messageCount.Add(_currentMessage.Author.Id, newMessageCount);
        }
        return false;
    }
    
    bool CapsFilter()
    {
        string messageContent = _currentMessage.Content;
        int messageLength = messageContent.Length;
        int count = 0;
        for (int i = 0; i < messageContent.Length; i++)
        {
            if (char.IsUpper(messageContent[i])) {
                count++;
            }
        }

        float percentage = (float) count / messageLength;
        if (count > 1 &&  percentage > 0.2)
        {
          return true;
        }
        return false;
    }
    
    public FilterResult FilterMessage(IMessage message)
    {
        _currentMessage = message;
        if (CharacterFilter())
        {
            return new FilterResult
            {
                IsFiltered = true,
                Reason = "invalidChar"
            };
        }
        
        if (CapsFilter())
        {
            return new FilterResult
            {
                IsFiltered = true,
                Reason = "caps"
            };
        }
        
        if (SpamFilter())
        {
            return new FilterResult
            {
                IsFiltered = true,
                Reason = "spam"
            };
        }
        
        return new FilterResult
        {
            IsFiltered = false,
            Reason = "none"
        };
    }
}