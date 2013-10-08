namespace TweeterTotters
{
    using System;
    using System.Windows.Data;

    /// <summary>
    /// Used to determine if two longs are equal to each other.
    /// </summary>
    public class CheckIfLongsAreEqualConverter : IMultiValueConverter
    {
        /// <summary>
        /// Returns a true of the longs are equal, otherwise false.
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is long && values[1] is long)
            {
                return (long)values[0] == (long)values[1];
            }

            return false;
        }

        /// <summary>
        /// Not implemented by the application.
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(Properties.Resources.ConvertNotImplemented);
        }
    }
}
