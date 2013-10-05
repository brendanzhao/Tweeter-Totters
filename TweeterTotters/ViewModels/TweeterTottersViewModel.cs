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
        /// Represents if the application is currently favoriting.
        /// </summary>
        private bool favoriteInProgress;

        /// <summary>
        /// Represents if the application is currently Tweeting.
        /// </summary>
        private bool tweetInProgress;

        /// <summary>
        /// Represents if the application is currently toggling reply mode.
        /// </summary>
        private bool replyModeInProgress;      

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
            HomePageTweets = TwitterAPIUtility.GetHomePageTweets(service);
            ProfilePageTweets = TwitterAPIUtility.GetProfilePageTweets(service);
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
            FavoriteCommand = new RelayCommand<TwitterStatus>(ExecuteFavorite, (obj) => !favoriteInProgress);
            ReplyModeCommand = new RelayCommand<TwitterStatus>(ExecuteReplyMode, (obj) => !replyModeInProgress);
            TweetCommand = new RelayCommand(ExecuteTweet, () => !string.IsNullOrWhiteSpace(CurrentTweet) && CurrentTweet.Length <= MaxTweetLength && !tweetInProgress);           
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
        /// Sends the current Tweet and then refreshes the Tweets on the app.
        /// </summary>
        public void ExecuteTweet()
        {
            tweetInProgress = true;

            TwitterAPIUtility.Tweet(service, CurrentTweet, tweetIdToReplyTo);
            CurrentTweet = string.Empty;
            HomePageTweets = TwitterAPIUtility.GetHomePageTweets(service);
            ProfilePageTweets = TwitterAPIUtility.GetProfilePageTweets(service);

            tweetInProgress = false;
        }  
    }
}