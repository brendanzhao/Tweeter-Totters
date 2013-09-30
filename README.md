##Tweeter-Totters

####Background
A little background about this project. This all started as a hack day project that I wrote during my first coop term.
Just starting out and not sure where to begin, I created Tweeter Totters as a Windows Form using the [Linq 2 Twitter](http://linqtotwitter.codeplex.com/) library.
Unfortunately I packed everything into one file because at the time I wasn't sure quite how to separate the files nicely.
I left it as is and didn't touch it for a while and considered it one of those, it works - but not nicely projects.<br />

A few months later I learned about WPF and all it's wonderful features so I decided to get my hands dirty and completely redid 
this project in WPF with everything overhauled and I actually decided to use the TweetSharp library instead of the [Linq 2 Twitter](http://linqtotwitter.codeplex.com/) 
library this time. Although I wrote it better this time round, it still felt cluttered.<br />

Soon after that though I discovered an architectural pattern called [MVVM](http://msdn.microsoft.com/en-us/magazine/dd419663.aspx)
by Josh Smith and decided that this was ultimately how I should be designing Tweeter Totters. I completely refactored my code and architected
my program according the design and am now finally starting to feel happy about the project and where it is now.

####About
This is WPF project intended to be a desktop application providing basic to advanced Twitter functionality. <br />
It is written in C# and is using the [TweetSharp Library](https://github.com/danielcrenna/tweetsharp) written by
[Daniel Crenna](https://github.com/danielcrenna) that has been reccommended by Twitter.<br />
The entire project has it's UI, logic and data seperated as laid out by the WPF [MVVM](http://msdn.microsoft.com/en-us/magazine/dd419663.aspx) 
architectural pattern.

####Current Working Functionality/Features
-Main GUI is finished
-Ability to Login<br />
-View homepage tweets along with information about the tweet (user, date, name, profile picture)<br />
-Date is shown in user's timezone instead of standard UCT timezone<br />
-All requests to Twitter are checked for HTTP and other errors.<br />
-Tweetbox is water marked with "What are you doing?"<br />
-Displays remaining characters left in current Tweet<br />
-Able to Tweet with basic tweet validation<br />
-Full seperation of GUI, logic and data applied.

####Near Future TODO
-When your current tweet reaches > 140 characters, trigger the tweet length counter to turn red.<br />
-View user Profile Tweets<br />
-View Other Profile Tweets<br />
-Be able to Reply, Favourite, Delete and Retweet<br />
-Refactor the watermark to use a dependency property instead of the ghetto method I've implemented<br />
-Use a Style template for my ItemsControl holding all the tweets.
