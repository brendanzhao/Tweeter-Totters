namespace TweeterTotters
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Used to count the remaining characters left in the user's tweet where tweet's have a max length of 140 characters.
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class TweetLengthToRemainingCharsConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string to an integer representing 140 minus the character count of the string.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Microsoft Interface")]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                int tweetLength = (int)value;
                int remainingCharCount = TwitterUtility.MaxTweetLength - tweetLength;

                return remainingCharCount.ToString();
            }

            return 0;
        }

        /// <summary>
        /// Not Implemented by the application.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Microsoft Interface")]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(Properties.Resources.ConvertNotImplemented);
        }
    }
}
