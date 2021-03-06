﻿namespace TweeterTotters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Windows;
    using Microsoft.VisualBasic;
    using TweetSharp;

    /// <summary>
    /// Contains general methods used to interact with the Twitter API.
    /// </summary>
    public static class TwitterAPIUtility
    {
        /// <summary>
        /// Passes my registered application credentials to Twitter and then authenticates the user using OAuth.
        /// </summary>
        /// <param name="consumerKey">A <see cref="String"/> provided by Twitter which represents the API key associated with Tweeter Totters.</param>
        /// <param name="consumerSecret">A <see cref="String"/> provided by Twitter that authenticates the application access to Twitter.</param>
        /// <returns>An authenticated <see cref="TwitterService"/> that can now be used to to call the Twitter API.</returns>
        /// <remarks>The Consumer Secret Key is not meant to be public but as a random GitHub project, I see no repercussions.</remarks>
        public static TwitterService CreateAndAuthenticateService(string consumerKey, string consumerSecret)
        {
            TwitterService service = new TwitterService(System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"], System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"]);
            OAuthRequestToken requestToken = service.GetRequestToken();

            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());

            string verifier = Interaction.InputBox(Properties.Resources.PinLoginMessage, Properties.Resources.PinLoginTitle, null);
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);
            service.AuthenticateWith(access.Token, access.TokenSecret);

            TwitterAPIUtility.CheckError(service);

            return service;
        }

        /// <summary>
        /// Checks for actual Twitter Errors and also for http request errors.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> that is being checked for errors.</param>
        public static void CheckError(TwitterService service)
        {
            //if (service.Response.StatusCode == HttpStatusCode.
            if (service.Response.StatusCode != HttpStatusCode.OK)
            {
                if (service.Response.RateLimitStatus.RemainingHits == 0)
                {
                    MessageBox.Show(string.Format(Properties.Resources.ErrorMessageRateLimit, service.Response.RateLimitStatus.ResetTime));
                }
                else
                {
                    throw new WebException(Properties.Resources.ErrorMessageHttp);
                }
            }
            else if (service.Response.Error != null)
            {
                throw new WebException(Properties.Resources.ErrorMessageTweetSharp);
            }
        }

        /// <summary>
        /// Deletes the Tweet specified.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <param name="tweetId">A <see cref="long"/> representing the id of the Tweet to delete.</param>
        public static void DeleteTweet(TwitterService service, long tweetId)
        {
            service.DeleteTweet(new DeleteTweetOptions() { Id = tweetId, TrimUser = true });
            TwitterAPIUtility.CheckError(service);
        }

        /// <summary>
        /// Favorites the Tweet specified.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <param name="tweetId">A <see cref="long"/> representing the id of the Tweet to favorite.</param>
        public static void Favorite(TwitterService service, long tweetId)
        {
            service.FavoriteTweet(new FavoriteTweetOptions() { Id = tweetId });
            TwitterAPIUtility.CheckError(service);
        }

        /// <summary>
        /// Gets the user that logged into Twitter and then checks to see if an error has occurred.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <returns>A <see cref="TwitterUser"/> representing the user that is logged in.</returns>
        public static TwitterUser GetCurrentUser(TwitterService service)
        {
            TwitterUser user = service.GetUserProfile(new GetUserProfileOptions());
            TwitterAPIUtility.CheckError(service);
            return user;
        }

        /// <summary>
        /// Gets a collection of the most recent home page Tweets.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <returns>A <see cref="IEnumerable"/> of Twitter Status' on the user's homepage.</returns>
        public static IEnumerable<TwitterStatus> GetHomePageTweets(TwitterService service)
        {
            IEnumerable<TwitterStatus> tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
            TwitterAPIUtility.CheckError(service);
            return tweets;
        }

        /// <summary>
        /// Gets a collection of the most recent profile page Tweets.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <returns>A <see cref="IEnumerable"/> of Twitter Status' on the user's profile page.</returns>
        public static IEnumerable<TwitterStatus> GetProfilePageTweets(TwitterService service)
        {
            IEnumerable<TwitterStatus> tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions());
            TwitterAPIUtility.CheckError(service);
            return tweets;
        }

        /// <summary>
        /// Retweets the Tweet specified.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to the call the Twitter API.</param>
        /// <param name="tweetId">A <see cref="long"/> representing the Tweet to retweet.</param>
        public static void Retweet(TwitterService service, long tweetId)
        {
            service.Retweet(new RetweetOptions() { Id = tweetId, TrimUser = true });
            TwitterAPIUtility.CheckError(service);
        }

        /// <summary>
        /// Sends a new Tweet.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <param name="text">A <see cref="string"/> representing the text of the Tweet.</param>
        /// <param name="replyId">A <see cref="long"/> representing the Tweet that you're replying to. When this value is 0, it is not replying to anyone.</param>
        public static void Tweet(TwitterService service, string text, long replyId)
        {
            service.SendTweet(new SendTweetOptions() { Status = text, InReplyToStatusId = replyId });
            TwitterAPIUtility.CheckError(service);
        }

        /// <summary>
        /// Unfavorites the Tweet specified.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <param name="tweetId">A <see cref="long"/> representing the id of the Tweet to unfavorite.</param>
        public static void Unfavorite(TwitterService service, long tweetId)
        {
            service.UnfavoriteTweet(new UnfavoriteTweetOptions() { Id = tweetId });
            TwitterAPIUtility.CheckError(service);
        }
    }
}