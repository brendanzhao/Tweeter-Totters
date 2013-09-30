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
        /// Represents the current tweet being typed into the tweetBox.
        /// </summary>
        private string currentTweet;

        /// <summary>
        /// Represents the latest home page tweets.
        /// </summary>
        private IEnumerable<TwitterStatus> homePageTweets;

        /// <summary>
        /// Represents the latest profile page tweets.
        /// </summary>
        private IEnumerable<TwitterStatus> profilePageTweets;

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
        /// Gets the current tweet being typed into the tweetBox.
        /// </summary>
        /// <remarks>RaisePropertyChanged() had to be implemented in order for the GUI reflect that the 
        /// we set the CurrentTweet to an empty string after it's been sent.</remarks>
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
        /// Gets an <see cref="IEnumerable"/> of TwitterStatus' representing the latest profile page tweets.
        /// </summary>
        /// <remarks>RaisePropertyChanged() had to be implemented in order for the GUI to reflect changes when the data updates.</remarks>
        public IEnumerable<TwitterStatus> ProfilePageTweets
        {
            get
            {
                return profilePageTweets;
            }
            set
            {
                profilePageTweets = value;
                RaisePropertyChanged("ProfilePageTweets");
            }
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
        /// Gets or sets the service that is connecting to Twitter
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
        }

        /// <summary>
        /// Represents the functionality of the TweetCommand.
        /// </summary>
        public void ExecuteTweet()
        {
            TwitterAPIUtility.Tweet(Service, CurrentTweet);
            CurrentTweet = string.Empty;
            HomePageTweets = TwitterAPIUtility.GetHomePageTweets(Service);
            ProfilePageTweets = TwitterAPIUtility.GetProfilePageTweets(Service);
        }
    }
}
