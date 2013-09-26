namespace TweeterTotters
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Microsoft Interface")]
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is bool && values[1] is bool)
            {
                bool hasText = !(bool)values[0];
                bool hasFocus = (bool)values[1];

                if (hasFocus || hasText)
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Visible;
        }
        
        /// <summary>
        /// Not implemented by the application.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Microsoft Interface")]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(Properties.Resources.ConvertNotImplemented);
        }
    }
}
