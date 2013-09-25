namespace TweeterTotters
{
    using System.Collections.Generic;
    using System.Windows;
    using TweetSharp;

    /// <summary>
    /// Represents the window being displayed, the main GUI.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Represents the currently logged in user.
        /// </summary>
        private TwitterUser currentUser;

        /// <summary>
        /// Represents the service connecting to Twitter.
        /// </summary>
        private TwitterService service;

        /// <summary>
        /// Represents the tweets that are currently being displayed.
        /// </summary>
        private IEnumerable<TwitterStatus> tweets;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. This is the starting point for the application.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            service = TwitterUtility.CreateAndAuthenticateService(System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"], System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"]);
            tweets = TwitterUtility.GetHomePageTweets(service);
            currentUser = TwitterUtility.GetCurrentUser(service);

            this.DataContext = currentUser;
            mainDisplay.ItemsSource = tweets;
        }
    }
}
