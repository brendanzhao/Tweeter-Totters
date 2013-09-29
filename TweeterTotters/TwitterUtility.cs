namespace TweeterTotters
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Net;
    using Microsoft.VisualBasic;
    using TweetSharp;

    /// <summary>
    /// Contains general methods and constants used to interact with Twitter.
    /// </summary>
    public static class TwitterUtility
    {
        /// <summary>
        /// Represents the maximum length of a tweet.
        /// </summary>
        public const int MaxTweetLength = 140;

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

            TwitterUtility.CheckError(service);

            return service;
        }

        /// <summary>
        /// Checks for actual Twitter Errors and also for http request errors.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> that is being checked for errors.</param>
        public static void CheckError(TwitterService service)
        {
            if (service.Response.StatusCode != HttpStatusCode.OK || service.Response.Error != null)
            {
                throw new WebException(Properties.Resources.ErrorMessage);
            }
        }

        /// <summary>
        /// Gets the user that logged into Twitter and then checks to see if an error has occurred.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <returns>A <see cref="TwitterUser"/> representing the user that is logged in.</returns>
        public static TwitterUser GetCurrentUser(TwitterService service)
        {
            TwitterUser user = service.GetUserProfile(new GetUserProfileOptions());
            TwitterUtility.CheckError(service);
            return user;
        }

        /// <summary>
        /// Gets a collection of the most recent home page tweets.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <param name="tweets">An <see cref="ObservableColletion"/> of TwitterStatus' that represent the tweets being displayed.</param>
        /// <remarks>In order for the observable collection to properly update the UI when we get a new collection of tweets,
        /// I cannot change the reference to our original tweets collection. I abuse the fact that C# methods are pass by
        /// reference value so the new tweets will update without needing to be returned.</remarks>
        public static void UpdateHomePageTweets(TwitterService service, ObservableCollection<TwitterStatus> tweets)
        {
            tweets.Clear();
            IEnumerable<TwitterStatus> newTweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
            TwitterUtility.CheckError(service);

            foreach (TwitterStatus ts in newTweets)
            {
                tweets.Add(ts);
            }
        }

        /// <summary>
        /// Sends a new Tweet.
        /// </summary>
        /// <param name="service">A <see cref="TwitterService"/> used to call the Twitter API.</param>
        /// <param name="text">A <see cref="string"/> representing the text of the Tweet.</param>
        public static void Tweet(TwitterService service, string text)
        {
            service.SendTweet(new SendTweetOptions() { Status = text });
            TwitterUtility.CheckError(service);
        }

        /// <summary>
        /// Checks if the tweet valid and can be tweeted.
        /// </summary>
        /// <param name="tweet">A <see cref="string"/> representing the tweet.</param>
        /// <returns>true if the tweet is valid; otherwise false.</returns>
        public static bool TweetIsValid(string tweet)
        {
            if (string.IsNullOrWhiteSpace(tweet) || tweet.Length > 140)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
