namespace TweeterTotters
{
    using System.Windows;

    /// <summary>
    /// Represents the GUI layer of the MVVM architectural pattern.
    /// </summary>
    public partial class TweeterTottersView : Window
    {
        /// <summary>
        /// This is bound to the View and will be the connection between the View and the data.
        /// </summary>
        private ObservableObject tweeterTottersViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweeterTottersView"/> class.
        /// </summary>
        public TweeterTottersView()
        {
            InitializeComponent();

            tweeterTottersViewModel = new TweeterTottersViewModel();
            DataContext = tweeterTottersViewModel;
        }
    }   
}
