##Tweeter-Totters

####Background
A little background about this project. This all started as a project I wrote during my first coop term.
Just starting out and not sure where to begin, I created Tweeter Totters as a Windows Form using the [Linq 2 Twitter](http://linqtotwitter.codeplex.com/) library.
Unfortunately I packed everything into one file because at the time I wasn't sure quite how to separate the files nicely.
I left it as is and didn't touch it for a while and considered it one of those, it works - but not nicely, type of project.<br />

A few months later I learned about WPF and all it's wonderful features so I decided to get my hands dirty and completely redid 
this project in WPF with everything overhauled and I actually decided to use the TweetSharp library instead of the Linq 2 Twitter
library this time. Although I wrote it better this time round, it still felt cluttered.<br />

Soon after that though I discovered an architectural pattern called [MVVM](http://msdn.microsoft.com/en-us/magazine/dd419663.aspx)
by Josh Smith and decided that this was ultimately how I should be designing Tweeter Totters. I completely refactored my code and architected
my program according the design and am now finally starting to feel happy about the project and where it is now.

####About
This is C# WPF project written in the MVVM pattern and is intended to be a desktop application providing basic to advanced Twitter functionality.
It's using the [TweetSharp Library](https://github.com/danielcrenna/tweetsharp) written by
[Daniel Crenna](https://github.com/danielcrenna) and it's also using the custom MVVM implementations of RelayCommand 
and ObservableObject instead of the default classes in the .NET framework.

####Current Working Functionality/Features
-An aesthetically pleasant GUI<br />
-User authentication<br />
-View homepage and profile page tweets along with information about the tweet (user, date, name, profile picture)<br />
-Date is shown in user's timezone instead of standard UCT timezone<br />
-All requests to Twitter are checked for HTTP and TweetSharp errors.<br />
-Tweetbox is water marked with "What are you doing?"<br />
-Displays remaining characters left in current Tweet and turns red when you pass the maximum length.<br />
-Able to Tweet with basic tweet validation (tweet button will disable)<br />
-Can reply to Tweets<br />
-Can favourite Tweets<br />
-Tweet hyperlinks change colours to indicate if the Tweet is currently being replied to, or is favorited.

####Near Future TODO
-View Other Profile Tweets<br />
-View Direct Messages<br />
-Be able to Delete and Retweet<br />
-Retweet hyperlink should not appear if the user has his profile set to private<br />
-Refactor the watermark to use a dependency property instead of the ghetto method I've implemented<br />
