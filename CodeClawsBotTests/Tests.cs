using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using CodeClawsDiscordBot;
using Discord;

namespace CodeClawsBotTests
{
    public class Tests
    {

        internal sealed class MockedMessage : IMessage
        {
            public Task AddReactionAsync(IEmote emote, RequestOptions options = null)
            {
                throw new NotImplementedException();
            }

            public Task RemoveReactionAsync(IEmote emote, IUser user, RequestOptions options = null)
            {
                throw new NotImplementedException();
            }

            public Task RemoveReactionAsync(IEmote emote, ulong userId, RequestOptions options = null)
            {
                throw new NotImplementedException();
            }

            public Task RemoveAllReactionsAsync(RequestOptions options = null)
            {
                throw new NotImplementedException();
            }

            public Task RemoveAllReactionsForEmoteAsync(IEmote emote, RequestOptions options = null)
            {
                throw new NotImplementedException();
            }

            public IAsyncEnumerable<IReadOnlyCollection<IUser>> GetReactionUsersAsync(IEmote emoji, int limit, RequestOptions options = null)
            {
                throw new NotImplementedException();
            }

            public MessageType Type { get; }
            public MessageSource Source { get; }
            public bool IsTTS { get; }
            public bool IsPinned { get; }
            public bool IsSuppressed { get; }
            public bool MentionedEveryone { get; }
            public string Content { get; set; }
            public string CleanContent { get; }
            public DateTimeOffset Timestamp { get; }
            public DateTimeOffset? EditedTimestamp { get; }
            public IMessageChannel Channel { get; }
            public IUser Author { get; set; }
            public IThreadChannel Thread { get; }
            public IReadOnlyCollection<IAttachment> Attachments { get; }
            public IReadOnlyCollection<IEmbed> Embeds { get; }
            public IReadOnlyCollection<ITag> Tags { get; }
            public IReadOnlyCollection<ulong> MentionedChannelIds { get; }
            public IReadOnlyCollection<ulong> MentionedRoleIds { get; }
            public IReadOnlyCollection<ulong> MentionedUserIds { get; }
            public MessageActivity Activity { get; }
            public MessageApplication Application { get; }
            public MessageReference Reference { get; }
            public IReadOnlyDictionary<IEmote, ReactionMetadata> Reactions { get; }
            public IReadOnlyCollection<IMessageComponent> Components { get; }
            public IReadOnlyCollection<IStickerItem> Stickers { get; }
            public MessageFlags? Flags { get; }
            public IMessageInteraction Interaction { get; }
            public MessageRoleSubscriptionData RoleSubscriptionData { get; }
            public ulong Id { get; }
            public DateTimeOffset CreatedAt { get; }
            public Task DeleteAsync(RequestOptions options = null)
            {
                throw new NotImplementedException();
            }
        }

        internal sealed class MockedUser : IUser
        {
            public ulong Id { get; set; }
            public DateTimeOffset CreatedAt { get; }
            public string Mention { get; }
            public UserStatus Status { get; }
            public IReadOnlyCollection<ClientType> ActiveClients { get; }
            public IReadOnlyCollection<IActivity> Activities { get; }
            public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
            {
                throw new NotImplementedException();
            }

            public string GetDefaultAvatarUrl()
            {
                throw new NotImplementedException();
            }

            public Task<IDMChannel> CreateDMChannelAsync(RequestOptions options = null)
            {
                throw new NotImplementedException();
            }

            public string AvatarId { get; }
            public string Discriminator { get; }
            public ushort DiscriminatorValue { get; }
            public bool IsBot { get; }
            public bool IsWebhook { get; }
            public string Username { get; }
            public UserProperties? PublicFlags { get; }
        }
        
        
        [Fact]
        public void FilterCaps()
        {
            MessageFilter messageFilter = new();
            MockedMessage message = new();
            message.Content = "HeLlO";

            FilterResult filterResult = messageFilter.FilterMessage(message);
            Assert.True(filterResult.IsFiltered);
            Assert.Equal("caps", filterResult.Reason);
        }
        
        
        [Fact]
        public void FilterChars()
        {
            MessageFilter messageFilter = new();
            MockedMessage message = new();
            message.Content = "❄︎⍓︎◻︎♏︎";

            FilterResult filterResult = messageFilter.FilterMessage(message);
            Assert.True(filterResult.IsFiltered);
            Assert.Equal("invalidChar", filterResult.Reason);
        }
        
        [Fact]
        public void FilterSpam()
        {
            MessageFilter messageFilter = new();
            MockedMessage message = new();
            FilterResult filterResult = new();
            MockedUser user = new MockedUser();
            user.Id = 1;
            message.Author = user;
            for (int i = 0; i < 7; i++)
            {
                message.Content = "spam";
                filterResult = messageFilter.FilterMessage(message);
            }
            Assert.True(filterResult.IsFiltered);
            Assert.Equal("spam", filterResult.Reason);
        }
    }
}