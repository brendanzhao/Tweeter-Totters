namespace TweeterTotters
{
    using System;
    using System.Windows;
    
    /// <summary>
    /// Where all the magic begins.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The main starting point for the application.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
        }
    }
}
