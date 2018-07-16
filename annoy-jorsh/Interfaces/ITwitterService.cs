using System;
using Tweetinvi;
using Tweetinvi.Models;
using System.Collections.Generic;
using System.Text;

namespace annoyjorsh.Interfaces
{
    public interface ITwitterService
    {

        void StreamStarted(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs args);

        void TweetRecieved(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs args);
        void TweetDeleted(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs args);

        void JsonObjectReceived(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs args);
        void UnmanagedEventReceived(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs args);
        void LimitReached(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs args);
        void DisconnectionMessageRecieved(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs args);


    }
}
