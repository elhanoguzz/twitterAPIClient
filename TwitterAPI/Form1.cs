using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using TweetSharp;
using Tweetinvi;
using System.IO;
using Tweetinvi.Models;

namespace TwitterAPI
{
    public partial class Form1 : Form
    {
       
        TwitterService service;
        TwitterService twitterApp;
        TwitterAuth newAuth = new TwitterAuth("hrGMeBXNQZyYmiJfXAhjnwsWl", "rBm7rKUVfhqmXIl4n5CnPoVMO9bdRg0CFUjWHIq30wn1VvSGvJ", "2574315773-HtgAFRHbRa2GomHqtZSDjEEMVWh4MHUIY4lFqmS", "fdsNQhBt9nqVl7U8POUT7CNzbTfBKcBl0Nnbbg4Ynr5rj");
        public Form1()
        {
            InitializeComponent();


            //var user = User.GetAuthenticatedUser();

            //lblUser.Text = user.ToString();
            var twitterApp = new TwitterService("hrGMeBXNQZyYmiJfXAhjnwsWl", "rBm7rKUVfhqmXIl4n5CnPoVMO9bdRg0CFUjWHIq30wn1VvSGvJ");
            twitterApp.AuthenticateWith("2574315773-HtgAFRHbRa2GomHqtZSDjEEMVWh4MHUIY4lFqmS", "fdsNQhBt9nqVl7U8POUT7CNzbTfBKcBl0Nnbbg4Ynr5rj");

            //tweet with an image

            Auth.SetUserCredentials("hrGMeBXNQZyYmiJfXAhjnwsWl", "rBm7rKUVfhqmXIl4n5CnPoVMO9bdRg0CFUjWHIq30wn1VvSGvJ",
               "2574315773-HtgAFRHbRa2GomHqtZSDjEEMVWh4MHUIY4lFqmS", "fdsNQhBt9nqVl7U8POUT7CNzbTfBKcBl0Nnbbg4Ynr5rj");


            service = new TwitterService("hrGMeBXNQZyYmiJfXAhjnwsWl", "rBm7rKUVfhqmXIl4n5CnPoVMO9bdRg0CFUjWHIq30wn1VvSGvJ", "2574315773-HtgAFRHbRa2GomHqtZSDjEEMVWh4MHUIY4lFqmS", "fdsNQhBt9nqVl7U8POUT7CNzbTfBKcBl0Nnbbg4Ynr5rj");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //service = new TwitterService("bKDEZiufwYqlXZVhbigGEc82A", "j1Bkznmp8C80Cdv9LtNzNNX7Yt9OXpXHH8GT9z9hrPRWVqEkjI", "2574315773-tmnmxQkJBxYWU3nzg5mE1jXkESG5R9iY9cxqhyM", "Gg8TYmoZHpNFS0rVtZ6cbUpiyhFDGJOhnqPKLt5doLqEd");
        }
        ListBox tweetid = new ListBox(); //HOLDER LISTBOXS
        ListBox userid = new ListBox();
        ListBox favoriteHolder = new ListBox();
        ListBox permissionChecker = new ListBox();
     
    private void Button1_Click(object sender, EventArgs e) //HOMEPAGE
        {
            favoriteHolder.Items.Clear();
            listBox1.Items.Clear();
            tweetid.Items.Clear();
            permissionChecker.Items.Clear();
            var received = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 1 });
            foreach (var tweet in received)
            {
                listBox1.Items.Add(tweet.Text);//can add retweet/favorite count+ IsRetweeted/Favorited+ creation time + data about creator
                tweetid.Items.Add(tweet.Id);
                favoriteHolder.Items.Add(tweet.IsFavorited);
                permissionChecker.Items.Add(tweet.User.Id);
            }
            //var mainPage = newAuth.getMainPage();


        }



        private void Button2_Click(object sender, EventArgs e)//TWEET
        {
            service.SendTweet(new SendTweetOptions() {Status=textBox1.Text });
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Please enter a status.");
            } else
            {
                MessageBox.Show("Succeed");
            }
        }

        private void Button3_Click(object sender, EventArgs e)//RETWEET
        {
           
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a tweet to retweet.");
            }else {

                service.Retweet(new RetweetOptions() { Id = long.Parse(tweetid.Items[listBox1.SelectedIndex].ToString()) });
                MessageBox.Show("Succeed");
            }
            
        }

        private void Button5_Click(object sender, EventArgs e)//MYPAGE
        {
            permissionChecker.Items.Clear();
            favoriteHolder.Items.Clear();
            tweetid.Items.Clear();
            listBox1.Items.Clear();
            var mypage = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { Count=3}); 
            foreach (var tweet in mypage)
            {
                listBox1.Items.Add(tweet.Text+tweet.IsFavorited);
                tweetid.Items.Add(tweet.Id);
                favoriteHolder.Items.Add(tweet.IsFavorited);
                permissionChecker.Items.Add(tweet.User.Id);

            }
        }

        private void Button4_Click(object sender, EventArgs e) //DELETE
        {
            permissionChecker.SelectedIndex = listBox1.SelectedIndex;
            string checker = permissionChecker.Items[permissionChecker.SelectedIndex].ToString();

            if (checker == "2574315773")
            {
                long idToDelete = long.Parse(tweetid.Items[listBox1.SelectedIndex].ToString());
                var response = newAuth.DeleteTweet(idToDelete);
                string answer = response.ToString();
                if (answer.Length == 0)
                {
                    MessageBox.Show("Nothing to delete.");

                }
                else
                {
                    MessageBox.Show("Tweet Deleted");
                    listBox1.Items.Clear();

                }
            }
            else

            {
                MessageBox.Show("You can't delete other peoples' tweets.");
            }

        }

        private void Button6_Click(object sender, EventArgs e) // Favorites
        {
            tweetid.SelectedIndex = listBox1.SelectedIndex;
            favoriteHolder.SelectedIndex = listBox1.SelectedIndex;
            string checker = favoriteHolder.Items[favoriteHolder.SelectedIndex].ToString();
           if(checker == "True")
            {
                MessageBox.Show("Already added to favorites.");
            }
            else
            {
                var favorite = service.FavoriteTweet(new FavoriteTweetOptions() { Id = long.Parse(tweetid.SelectedItem.ToString()) });
                MessageBox.Show("Favorited.");
                listBox1.Items.Clear();


            }

          
            
        }

        private void Button7_Click(object sender, EventArgs e) //followers
        {
            listBox1.Items.Clear();
            var followers = service.ListFriendIdsOf(new ListFriendIdsOfOptions() { });
            foreach (var follower in followers)
            {
                
                listBox1.Items.Add(follower);
                userid.Items.Add(follower);
            }

        }

        private void Button8_Click(object sender, EventArgs e) //messagebox
        {

            //var sentMessages = service.ListDirectMessagesSent(new ListDirectMessagesSentOptions() {Count=1 });
            //var receivedMessages = service.ListDirectMessagesReceived(new ListDirectMessagesReceivedOptions() {Count=1 });
            //foreach(var sent in sentMessages)
            //{
            //    listBox1.Items.Add(sent.Text);
            //}
            //foreach (var received in receivedMessages)
            //    listBox1.Items.Add(received.Text);
            // var dmlist = newAuth.DMhistory(1,-1);
            //listBox1.Items.Add(dmlist.ToString());
            // listBox1.Items.Add(dmlist.Result);
            var received = Tweetinvi.Message.GetLatestMessagesReceived();
            var sent = Tweetinvi.Message.GetLatestMessagesSent();
            foreach(var message in received)
            {
                listBox1.Items.Add(message.Text);
            }
            foreach(var message in sent)
            {
                listBox1.Items.Add(message.Text);
            }

        }

        private void Button9_Click(object sender, EventArgs e)// sent dm
        {
            if(listBox1.SelectedIndex==-1)
            {
                MessageBox.Show("Please select a user to send DM.");
            } else
            {
                if(textBox1.Text.Length==0)
                {
                    MessageBox.Show("Please add a text to your DM.");

                } else
                {
                    //long userId = long.Parse(userid.Items[listBox1.SelectedIndex].ToString());
                    //var directMessage = service.SendDirectMessage(new SendDirectMessageOptions { UserId = long.Parse(userid.Items[listBox1.SelectedIndex].ToString()), Text = textBox1.Text });
                    //var directMessage = Tweetinvi.Message.PublishMessage(textBox1.Text, 1002583098628935681);
                    twitterApp.SendDirectMessage(new SendDirectMessageOptions() { ScreenName = "ekinakyazicii", Text = DateTime.UtcNow.Ticks.ToString() });

                    MessageBox.Show("Direct Message sent.");
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e) // bugged
        {
            byte[] file = File.ReadAllBytes(@"C:\Users\tryout.JPG");
            try
            {

            var imageUrl = Tweet.PublishTweetWithImage("Tryout tweet with Image.", file).Entities.Medias.First().MediaURL;
             Tweet.PublishTweetWithImage("Tryout tweet with Image.", file) ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
   
}
