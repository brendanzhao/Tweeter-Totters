##Tweeter-Totters

####About (Currently in the very early stages of development)
This is WPF project intended to be a desktop application providing basic to advanced Twitter functionality. <br />
This project is more or a less a learning experience to get a basic grasp on the underlying Twitter API, and to learn how to use WPF along with it's many features.<br />
Currently the project is written with one main WPF window holding all logical fields and GUI elements and calls on a static utility class to perform any logic or request.<br />

It is written in C# and will be using the TweetSharp library that has been reccommended by Twitter. The library can be found here: https://github.com/danielcrenna/tweetsharp

####Current Working Functionality/Features
-Ability to Login<br />
-View homepage tweets along with information about the tweet (user, date, name, profile picture)<br />
-Date is shown in user's timezone instead of standard UCT timezone<br />
-All requests to Twitter are checked for HTTP and other errors.<br />
-Tweetbox is water marked with "What are you doing"<br />
-Displays number of characters being tweeted

####Near Future TODO
-Be able to Tweet<br/>
-Count the remaining characters left in a tweet (0 - 140) instead of total characters.<br />
-View Profile Tweets<br />
-Be able to Reply, Favourite and Retweet<br />
-Separate GUI, Data and Logic into three layers in MVVM design
