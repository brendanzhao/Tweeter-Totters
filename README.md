##Tweeter-Totters

####About (Currently in the very early stages of development)
This is WPF project intended to be a desktop application providing basic to advanced Twitter functionality. <br />
This project is more or a less a learning experience to get a basic grasp on the underlying Twitter API, and to learn how to use WPF along with it's many features.<br />
Currently the project is written with one main WPF window holding all logical fields and GUI elements and calls on a static utility class to perform any logic or request.<br />

It is written in C# and will be using the TweetSharp library that has been reccommended by Twitter. The library can be found here: https://github.com/danielcrenna/tweetsharp

####Current Working Functionality/Features
-Main GUI is finished
-Ability to Login<br />
-View homepage tweets along with information about the tweet (user, date, name, profile picture)<br />
-Date is shown in user's timezone instead of standard UCT timezone<br />
-All requests to Twitter are checked for HTTP and other errors.<br />
-Tweetbox is water marked with "What are you doing"<br />
-Displays remaining characters left in current Tweet<br />

####Near Future TODO
-When your current tweet reaches > 140 characters, trigger the tweet length counter to turn red.<br />
-Be able to Tweet<br/>
-View Profile Tweets<br />
-Be able to Reply, Favourite and Retweet<br />
-Separate GUI, Data and Logic into three layers in MVVM design
