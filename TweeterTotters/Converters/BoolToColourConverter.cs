namespace TweeterTotters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// Converts a bool to a certain brush color. Used to color hyperlinks properly if they've already been favorited or retweeted.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolToColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a bool to a Brush.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool favoritedOrRetweeted = (bool)value;

                if (favoritedOrRetweeted)
                {
                    return (Brush)new BrushConverter().ConvertFromString("#E6B800");
                }
            }

            return (Brush)new BrushConverter().ConvertFromString("#666666");
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