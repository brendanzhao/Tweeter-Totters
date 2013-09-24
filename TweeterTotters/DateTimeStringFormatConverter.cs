namespace TweeterTotters
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converts a DateTime object to a string.
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeStringFormatConverter : IValueConverter
    {
        /// <summary>
        /// Converts a DateTime object to a string.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Microsoft Interface")]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime localTime = TimeZoneInfo.ConvertTime((DateTime)value, TimeZoneInfo.Local);
            return localTime.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Not Implemented by the application.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Microsoft Interface")]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back.");
        }
    }
}
