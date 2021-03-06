﻿namespace TweeterTotters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converts a DateTime object to a string.
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeToFormattedStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts a DateTime object to a string.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime localTime = TimeZoneInfo.ConvertTime((DateTime)value, TimeZoneInfo.Local);
                return localTime.ToString(CultureInfo.InvariantCulture);
            }

            return null;
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
