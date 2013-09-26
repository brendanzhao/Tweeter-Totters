namespace TweeterTotters
{
    using System;
    using System.Collections.Generic;
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
        /// <returns>An <see cref="IEnumerable"/> of TwitterStatus' that represent a collection of home page tweets.</returns>
        public static IEnumerable<TwitterStatus> GetHomePageTweets(TwitterService service)
        {
            IEnumerable<TwitterStatus> tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
            TwitterUtility.CheckError(service);
            return tweets;
        } 
    }
}
