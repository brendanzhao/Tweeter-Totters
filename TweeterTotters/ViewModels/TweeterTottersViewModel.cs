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
        public const int MaxTweetLength = 140;

        /// <summary>
        /// Represents the current Tweet being typed into the Tweet box.
        /// </summary>
        private string currentTweet = string.Empty;

        /// <summary>
        /// Represents the latest home page Tweets.
        /// </summary>
        private IEnumerable<TwitterStatus> homePageTweets;

        /// <summary>
        /// Represents the latest profile page Tweets.
        /// </summary>
        private IEnumerable<TwitterStatus> profilePageTweets;

        /// <summary>
        /// Represents the id of the Tweet being replied to. When this is equal to 0, it means it is not a reply.
        /// </summary>
        private long tweetIdToReplyTo;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweeterTottersViewModel"/> class.
        /// </summary>
        public TweeterTottersViewModel()
        {
            InitializeCommands();
            Service = TwitterAPIUtility.CreateAndAuthenticateService(System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"], System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"]);
            CurrentUser = TwitterAPIUtility.GetCurrentUser(Service);
            HomePageTweets = TwitterAPIUtility.GetHomePageTweets(Service);
            ProfilePageTweets = TwitterAPIUtility.GetProfilePageTweets(Service);
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
                RaisePropertyChanged("IsTweetPastMaxLength");
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
        /// Gets a <see cref="ICommand"/> representing the command bound to each Favorite hyperlink under each Tweet.
        /// </summary>
        public ICommand FavoriteCommand
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
        /// Gets a value indicating whether the current Tweet is greater than 140 characters.
        /// </summary>
        public bool IsTweetPastMaxLength
        {
            get { return currentTweet.Length > MaxTweetLength; }
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
        /// Gets an <see cref="int"/> representing the number of characters still possible to add to the current Tweet.
        /// </summary>
        public int RemainingCharsInCurrentTweet
        {
            get { return MaxTweetLength - CurrentTweet.Length; }
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
        /// Gets a <see cref="ICommand"/> representing the command bound to the Tweet button.
        /// </summary>
        public ICommand TweetCommand
        {
            get;
            private set;
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
        /// Gets or sets a <see cref="TwitterService"/> representing the service that is connecting to Twitter
        /// </summary>
        private TwitterService Service
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes all the commands on the application.
        /// </summary>
        public void InitializeCommands()
        {
            FavoriteCommand = new RelayCommand<TwitterStatus>(ExecuteFavorite);
            ReplyModeCommand = new RelayCommand<TwitterStatus>(ExecuteReplyMode);
            TweetCommand = new RelayCommand(ExecuteTweet, () => !string.IsNullOrWhiteSpace(CurrentTweet) && CurrentTweet.Length <= MaxTweetLength);           
        }

        /// <summary>
        /// Favorites or unfavorites the specified Twitter status.
        /// </summary>
        /// <param name="favoriteTweet">A <see cref="TwitterStatus"/> representing the status to be favorited.</param>
        public void ExecuteFavorite(TwitterStatus favoriteTweet)
        {
            if (favoriteTweet.IsFavorited)
            {
                TwitterAPIUtility.Unfavorite(Service, favoriteTweet.Id);
                favoriteTweet.IsFavorited = false;
            }
            else
            {
                TwitterAPIUtility.Favorite(Service, favoriteTweet.Id);
                favoriteTweet.IsFavorited = true;
            }
        }

        /// <summary>
        /// Toggles the application so that the next Tweet sent will be a reply.
        /// </summary>
        /// <param name="replyTweet">A <see cref="TwitterStatus"/> containing information about the Tweet being replied to.</param>
        public void ExecuteReplyMode(TwitterStatus replyTweet)
        {
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
        }

        /// <summary>
        /// Sends the current Tweet and then refreshes the Tweets on the app.
        /// </summary>
        public void ExecuteTweet()
        {
            TwitterAPIUtility.Tweet(Service, CurrentTweet, tweetIdToReplyTo);
            CurrentTweet = string.Empty;
            HomePageTweets = TwitterAPIUtility.GetHomePageTweets(Service);
            ProfilePageTweets = TwitterAPIUtility.GetProfilePageTweets(Service);
        }    
    }
}