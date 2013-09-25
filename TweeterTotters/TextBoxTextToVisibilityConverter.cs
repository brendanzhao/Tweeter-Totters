namespace TweeterTotters
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Used to determine if the text block watermark should be displayed.
    /// </summary>
    public class TextBoxTextToVisibilityConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts a Textbox to a Visibility enumeration value.
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
            throw new NotImplementedException();
        }
    }
}
