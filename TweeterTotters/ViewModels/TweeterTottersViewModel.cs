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
        /// Represents the maximum valid length of a tweet.
        /// </summary>
        public const int MaxTweetLength = 140;

        /// <summary>
        /// Represents the current tweet being typed into the tweetBox.
        /// </summary>
        private string currentTweet = string.Empty;

        /// <summary>
        /// Represents the latest home page tweets.
        /// </summary>
        private IEnumerable<TwitterStatus> homePageTweets;

        /// <summary>
        /// Represents the latest profile page tweets.
        /// </summary>
        private IEnumerable<TwitterStatus> profilePageTweets;

        /// <summary>
        /// Represents the id of the tweet being replied to. When this is equal to 0, it means it is not a reply.
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
        /// Gets a <see cref="string"/> representing the current tweet being typed into the tweetBox.
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
        /// Gets an <see cref="IEnumerable"/> of TwitterStatus' representing the latest home page tweets.
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
        /// Gets a value indicating whether the current tweet is greater than 140 characters.
        /// </summary>
        public bool IsTweetPastMaxLength
        {
            get { return currentTweet.Length > MaxTweetLength; }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of TwitterStatus' representing the latest profile page tweets.
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
        /// Gets an <see cref="int"/> representing the number of characters still possible to add to the current tweet.
        /// </summary>
        public int RemainingCharsInCurrentTweet
        {
            get { return MaxTweetLength - CurrentTweet.Length; }
        }

        /// <summary>
        /// Gets a <see cref="ICommand"/> representing the command bound to each Reply hyperlink under each tweet.
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
        /// Gets a <see cref="long"/> representing the id of the tweet you are replying to.
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
            TweetCommand = new RelayCommand(ExecuteTweet, () => !string.IsNullOrWhiteSpace(CurrentTweet) && CurrentTweet.Length <= 140);
            ReplyModeCommand = new RelayCommand<TwitterStatus>(ExecuteReplyMode);
        }

        /// <summary>
        /// Sends the current tweet and then refreshes the tweets on the app.
        /// </summary>
        public void ExecuteTweet()
        {
            TwitterAPIUtility.Tweet(Service, CurrentTweet, tweetIdToReplyTo);
            CurrentTweet = string.Empty;
            HomePageTweets = TwitterAPIUtility.GetHomePageTweets(Service);
            ProfilePageTweets = TwitterAPIUtility.GetProfilePageTweets(Service);
        }

        /// <summary>
        /// Toggles the application so that the next tweet sent will be a reply.
        /// </summary>
        /// <param name="replyTweet">A <see cref="TwitterStatus"/> containing information about the tweet being replied to.</param>
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
    }
}
