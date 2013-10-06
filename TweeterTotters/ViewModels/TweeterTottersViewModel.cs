namespace TweeterTotters
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using TweetSharp;

    /// <summary>
    /// Represents the logic layer of the MVVM architectural pattern. All Data is stored as properties and manipulated here.
    /// </summary>
    public class TweeterTottersViewModel : ObservableObject
    {
        /// <summary>
        /// Represents the maximum valid length of a Tweet.
        /// </summary>
        private const int MaxTweetLength = 140;

        /// <summary>
        /// Represents the service connecting to the Twitter API.
        /// </summary>
        private readonly TwitterService service;

        /// <summary>
        /// Represents if the application is currently deleting.
        /// </summary>
        private bool deleteInProgress;

        /// <summary>
        /// Represents if the application is currently favoriting.
        /// </summary>
        private bool favoriteInProgress;

        /// <summary>
        /// Represents if the application is currently toggling reply mode.
        /// </summary>
        private bool replyModeInProgress;

        /// <summary>
        /// Represents if the application is currently retweeting.
        /// </summary>
        private bool retweetInProgress;

        /// <summary>
        /// Represents if the application is currently Tweeting.
        /// </summary>
        private bool tweetInProgress;

        /// <summary>
        /// Backs the CurrentTweet property.
        /// </summary>
        private string currentTweet = string.Empty;

        /// <summary>
        /// Backs the TweetIdToReplyTo property.
        /// </summary>
        private long tweetIdToReplyTo;

        /// <summary>
        /// Backs the HomePageTweets property.
        /// </summary>
        private IEnumerable<TwitterStatus> homePageTweets;

        /// <summary>
        /// Backs the ProfilePageTweets property.
        /// </summary>
        private IEnumerable<TwitterStatus> profilePageTweets;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweeterTottersViewModel"/> class.
        /// </summary>
        public TweeterTottersViewModel()
        {
            InitializeCommands();
            service = TwitterAPIUtility.CreateAndAuthenticateService(System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"], System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"]);
            CurrentUser = TwitterAPIUtility.GetCurrentUser(service);
            Refresh();
        }

        /// <summary>
        /// Gets a <see cref="string"/> representing the current Tweet being typed into the Tweet box.
        /// </summary>
        /// <remarks>RaisePropertyChanged() had to be implemented in order for the GUI reflect changes when the data updates.</remarks>
        public string CurrentTweet
        {
            get
            {
                return currentTweet;
            }

            private set
            {
                currentTweet = value;
                RaisePropertyChanged("CurrentTweet");
                RaisePropertyChanged("RemainingCharsInCurrentTweet");
                RaisePropertyChanged("TweetIsPastMaxLength");
            }
        }

        /// <summary>
        /// Gets a <see cref="TwitterUser"/> representing the user that is logged into Twitter.
        /// </summary>
        public TwitterUser CurrentUser
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of TwitterStatus' representing the latest home page Tweets.
        /// </summary>
        /// <remarks>RaisePropertyChanged() had to be implemented in order for the GUI to reflect changes when the data updates.</remarks>
        public IEnumerable<TwitterStatus> HomePageTweets
        {
            get
            {
                return homePageTweets;
            }

            private set
            {
                homePageTweets = value;
                RaisePropertyChanged("HomePageTweets");
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of TwitterStatus' representing the latest profile page Tweets.
        /// </summary>
        /// <remarks>RaisePropertyChanged() had to be implemented in order for the GUI to reflect changes when the data updates.</remarks>
        public IEnumerable<TwitterStatus> ProfilePageTweets
        {
            get
            {
                return profilePageTweets;
            }

            private set
            {
                profilePageTweets = value;
                RaisePropertyChanged("ProfilePageTweets");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current Tweet is greater than 140 characters.
        /// </summary>
        public bool TweetIsPastMaxLength
        {
            get { return currentTweet.Length > MaxTweetLength; }
        }

        /// <summary>
        /// Gets a <see cref="long"/> representing the id of the Tweet you are replying to.
        /// </summary>
        /// <remarks>RaisePropertyChanged() had to be implemented in order for the GUI to reflect changes when the data updates.</remarks>
        public long TweetIdToReplyTo
        {
            get
            {
                return tweetIdToReplyTo;
            }

            private set
            {
                tweetIdToReplyTo = value;
                RaisePropertyChanged("TweetIdToReplyTo");
            }
        }

        /// <summary>
        /// Gets an <see cref="int"/> representing the number of characters still possible to add to the current Tweet.
        /// </summary>
        public int RemainingCharsInCurrentTweet
        {
            get { return MaxTweetLength - CurrentTweet.Length; }
        }

        /// <summary>
        /// Gets a <see cref="ICommand"/> representing the command to each Delete hyperlink under each user Tweet.
        /// </summary>
        public ICommand DeleteCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a <see cref="ICommand"/> representing the command bound to each Favorite hyperlink under each Tweet.
        /// </summary>
        public ICommand FavoriteCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a <see cref="ICommand"/> representing the command bound to each Reply hyperlink under each Tweet.
        /// </summary>
        public ICommand ReplyModeCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a <see cref="ICommand"/> representing the command bound to each Retweet hyperlink under each Tweet.
        /// </summary>
        public ICommand RetweetCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a <see cref="ICommand"/> representing the command bound to the Tweet button.
        /// </summary>
        public ICommand TweetCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes all the commands on the application.
        /// </summary>
        public void InitializeCommands()
        {
            DeleteCommand = new RelayCommand<TwitterStatus>(ExecuteDelete, (obj) => !deleteInProgress);
            FavoriteCommand = new RelayCommand<TwitterStatus>(ExecuteFavorite, (obj) => !favoriteInProgress);
            ReplyModeCommand = new RelayCommand<TwitterStatus>(ExecuteReplyMode, (obj) => !replyModeInProgress);
            RetweetCommand = new RelayCommand<TwitterStatus>(ExecuteRetweet, (obj) => !retweetInProgress && !obj.IsTruncated);
            TweetCommand = new RelayCommand(ExecuteTweet, () => !string.IsNullOrWhiteSpace(CurrentTweet) && CurrentTweet.Length <= MaxTweetLength && !tweetInProgress);
        }

        /// <summary>
        /// Deletes the specified Twitter status.
        /// </summary>
        /// <param name="deleteTweet">A <see cref="TwitterStatus"/> representing the status to be deleted.</param>
        public void ExecuteDelete(TwitterStatus deleteTweet)
        {
            deleteInProgress = true;

            TwitterAPIUtility.DeleteTweet(service, deleteTweet.Id);
            Refresh();

            deleteInProgress = false;
        }

        /// <summary>
        /// Favorites or unfavorites the specified Twitter status.
        /// </summary>
        /// <param name="favoriteTweet">A <see cref="TwitterStatus"/> representing the status to be favorited.</param>
        public void ExecuteFavorite(TwitterStatus favoriteTweet)
        {
            favoriteInProgress = true;

            if (favoriteTweet.IsFavorited)
            {
                TwitterAPIUtility.Unfavorite(service, favoriteTweet.Id);
                favoriteTweet.IsFavorited = false;
            }
            else
            {
                TwitterAPIUtility.Favorite(service, favoriteTweet.Id);
                favoriteTweet.IsFavorited = true;
            }

            favoriteInProgress = false;
        }

        /// <summary>
        /// Toggles the application so that the next Tweet sent will be a reply.
        /// </summary>
        /// <param name="replyTweet">A <see cref="TwitterStatus"/> containing information about the Tweet being replied to.</param>
        public void ExecuteReplyMode(TwitterStatus replyTweet)
        {
            replyModeInProgress = true;

            if (TweetIdToReplyTo == replyTweet.Id)
            {
                TweetIdToReplyTo = 0;
                CurrentTweet = string.Empty;
            }
            else
            {
                TweetIdToReplyTo = replyTweet.Id;
                CurrentTweet = string.Format("@{0} ", replyTweet.User.ScreenName);
            }

            replyModeInProgress = false;
        }

        /// <summary>
        /// Retweets the specified Twitter status.
        /// </summary>
        /// <param name="retweetTweet">A <see cref="TwitterStatus"/> representing the status to be retweeted.</param>
        public void ExecuteRetweet(TwitterStatus retweetTweet)
        {
            retweetInProgress = true;

            TwitterAPIUtility.Retweet(service, retweetTweet.Id);
            retweetTweet.IsTruncated = true;
            Refresh();

            retweetInProgress = false;
        }

        /// <summary>
        /// Sends the current Tweet.
        /// </summary>
        public void ExecuteTweet()
        {
            tweetInProgress = true;

            TwitterAPIUtility.Tweet(service, CurrentTweet, tweetIdToReplyTo);
            CurrentTweet = string.Empty;
            Refresh();

            tweetInProgress = false;
        }

        /// <summary>
        /// A really dirty hack to set the IsTruncated property of a collection ofTwitterStatus to represent if the user has Retweeted the TwitterStatus.
        /// </summary>
        /// <param name="tweets">An <see cref="IEnumerable"/> of TwitterStatus' to be modified.</param>
        /// <remarks>The TweetSharp library does not expose the "retweeted" JSON element from it's REST API calls when it GETs Tweets.
        /// The TwitterStatus class also does not define a bool to indicate whether the TwitterStatus is retweeted so I'm just basically 
        /// misusing an existing property that I don't need. The TweetSharp library however DOES expose the entire raw JSON source so
        /// in order to not have to redo a ton of code because of this third party library flaw, I'm doing a really really sketchy
        /// manual string search to parse the JSON and see if the TwitterStatus has been retweeted. REALLY DIRTY LOL.
        /// The alternative is sending an additional request for each Tweet loaded to get a list of it's retweets and then
        /// do a search of the user's associated with each retweet and check if the current user is in that list. 40 extra
        /// requests on application startup seemed unfavorable to this hack.</remarks>
        private void IsTruncatedToIsRetweetedHack(IEnumerable<TwitterStatus> tweets)
        {
            // if you find this string, then the tweet has been retweeted
            string trueJSON = "\"retweeted\": true,";

            // if you find this string, then the tweet is not retweeted
            string falseJSON = "\"retweeted\": false,";

            // if there's if find both JSON element strings, then we'll assume it's not been retweeted.
            int occurrencesTrue;
            int occurrencesFalse;

            // used to determine what's left to check in the RawSource for any more occurrences of the specified JSON
            int currentIndex;

            foreach (TwitterStatus ts in tweets)
            {
                occurrencesTrue = 0;
                occurrencesFalse = 0;
                currentIndex = 0;

                while ((currentIndex = ts.RawSource.IndexOf(trueJSON, currentIndex)) != -1)
                {
                    currentIndex += trueJSON.Length;
                    occurrencesTrue++;
                }

                currentIndex = 0;

                while ((currentIndex = ts.RawSource.IndexOf(falseJSON, currentIndex)) != -1)
                {
                    currentIndex += falseJSON.Length;
                    occurrencesFalse++;
                }

                if (occurrencesTrue >= 1 && occurrencesFalse == 0)
                {
                    ts.IsTruncated = true;
                }
                else if (occurrencesFalse >= 1 && occurrencesTrue == 0)
                {
                    ts.IsTruncated = false;
                }
                else
                {
                    ts.IsTruncated = false;
                }
            }
        }

        /// <summary>
        /// Refreshes the Tweets loaded to the application.
        /// </summary>
        private void Refresh()
        {
            HomePageTweets = TwitterAPIUtility.GetHomePageTweets(service);
            ProfilePageTweets = TwitterAPIUtility.GetProfilePageTweets(service);
            IsTruncatedToIsRetweetedHack(homePageTweets);
            IsTruncatedToIsRetweetedHack(profilePageTweets);
        }
    }
}