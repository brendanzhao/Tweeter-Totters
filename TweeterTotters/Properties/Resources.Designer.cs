﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TweeterTotters.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TweeterTotters.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  | Tweeter Totters - In Development.
        /// </summary>
        public static string ClientTitle {
            get {
                return ResourceManager.GetString("ClientTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Converting back has not been implemented by the application..
        /// </summary>
        public static string ConvertNotImplemented {
            get {
                return ResourceManager.GetString("ConvertNotImplemented", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The HTTP request didn&apos;t go through properly. Perhaps you&apos;ve been rate limited..
        /// </summary>
        public static string ErrorMessageHttp {
            get {
                return ResourceManager.GetString("ErrorMessageHttp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have been rate limited. Did you know there is maximum request limit of 15 per 15 minutes? Try again at {0}.
        /// </summary>
        public static string ErrorMessageRateLimit {
            get {
                return ResourceManager.GetString("ErrorMessageRateLimit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error has occurred in the TweetSharp library..
        /// </summary>
        public static string ErrorMessageTweetSharp {
            get {
                return ResourceManager.GetString("ErrorMessageTweetSharp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Home.
        /// </summary>
        public static string HomeTab {
            get {
                return ResourceManager.GetString("HomeTab", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Messages.
        /// </summary>
        public static string MessageTab {
            get {
                return ResourceManager.GetString("MessageTab", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login and then enter the 7 digit pin Twitter provides..
        /// </summary>
        public static string PinLoginMessage {
            get {
                return ResourceManager.GetString("PinLoginMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pin Authorization.
        /// </summary>
        public static string PinLoginTitle {
            get {
                return ResourceManager.GetString("PinLoginTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reply.
        /// </summary>
        public static string ReplyButton {
            get {
                return ResourceManager.GetString("ReplyButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Account Settings.
        /// </summary>
        public static string SettingsTab {
            get {
                return ResourceManager.GetString("SettingsTab", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tweet.
        /// </summary>
        public static string TweetButton {
            get {
                return ResourceManager.GetString("TweetButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to What are you doing?.
        /// </summary>
        public static string TweetWaterMark {
            get {
                return ResourceManager.GetString("TweetWaterMark", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Twitter Client by Brendan Zhao.
        /// </summary>
        public static string WindowTitle {
            get {
                return ResourceManager.GetString("WindowTitle", resourceCulture);
            }
        }
    }
}
