namespace TweeterTotters
{
    using System.Windows;
    using TweetSharp;

    /// <summary>
    /// Represents the window being displayed, the main GUI.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Represents the service connecting to Twitter.
        /// </summary>
        private TwitterService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. This is the starting point for the application.
        /// </summary>
        public MainWindow()
        {
            service = TwitterUtility.CreateAndAuthenticateService(System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"], System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"]);
            InitializeComponent();
        }
    }
}
