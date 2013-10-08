namespace TweeterTotters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Used to determine if a watermark should be displayed on a text block.
    /// </summary>
    public class TextBoxTextToVisibilityConverter : IMultiValueConverter
    {
        /// <summary>
        /// Returns a visibility depending on whether a textbox is empty or is in focus.
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is bool && values[1] is bool)
            {
                return (bool)values[0] && !(bool)values[1] ? Visibility.Visible : Visibility.Collapsed;
            }
            
            return Visibility.Collapsed;
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
