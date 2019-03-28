using System;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg
{
    public static class StreamedTweetFactory
    {

        public static StreamedTweet CreateStreamedTweet(
            string query,
            long statusId, 
            long userId,
            string userName, string name, 
            string text, 
            long? inReplyToStatusId, 
            string profileImageUrl, string profileImageUrlHttps)
        {
            if (statusId < 1) throw new ArgumentNullException("statusId");
            if (userId < 1) throw new ArgumentNullException("userId");
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userName");
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

            return new StreamedTweet()
            {
                StatusId = statusId,
                Query = query,
                UserId = userId,
                UserName = userName,
                InReplyToStatusId = inReplyToStatusId,
                Name = name,
                ProfileImageUrl = profileImageUrl,
                ProfileImageUrlHttps = profileImageUrlHttps,
                Text = text
            };
        }
    }
}
