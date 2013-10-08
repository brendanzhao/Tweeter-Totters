namespace TweeterTotters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts a bool to a certain brush color. Used to color hyperlinks properly if they've already been favorited or retweeted.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertedBoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a bool to the Visibility enumeration.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// Not Implemented by the application.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(Properties.Resources.ConvertNotImplemented);
        }
    }
}