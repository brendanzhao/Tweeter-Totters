<h2 align="center">Tweeter Totters</h2>
<p align="center"><img src="http://oi43.tinypic.com/10hvpz8.jpg" /></p>
####About
This is a C# WPF project written in the [MVVM](http://msdn.microsoft.com/en-us/magazine/dd419663.aspx) pattern and is intended to be an intertactive desktop application providing Twitter functionality.
It's using the [TweetSharp Library](https://github.com/danielcrenna/tweetsharp) written by
[Daniel Crenna](https://github.com/danielcrenna) and it's also using the custom MVVM implementations of RelayCommand 
and ObservableObject instead of the default classes in the .NET framework.

####Features
-An aesthetically pleasant GUI<br />
-Users can authenticate and login to their Twitter<br />
-Homepage and profile page Tweets are displayed<br />
-Each Tweet is attached with a respective Date, Reply button, Favorite button, Retweet button and Delete button<br />
-Protected user's tweets will not have a Retweet button and only the authenticated user's will have a Delete button<br />
-All requests to the Twitter API are checked for HTTP and TweetSharp errors.<br />
-The Tweet box is watermarked with the classic "What are you doing?"<br />
-There is a counter that triggers red when your tweet has past the maximum length<br />
-There is basic validation on all buttons such that you can't spam commands and commands enable/disable appropriately<br />

####Bugs
-Favoriting or Retweeting a Retweet messes up the application. The Twitter API (and/or TweetSharp) appears to return false for both the retweet and the original tweet when I query if they're favorited or retweeted.

####Note
-Due to the way the Twitter API works, you can not "un-retweet" but instead to delete a retweet, you need to simply
enter your profile page tab and delete the retweet from there.
