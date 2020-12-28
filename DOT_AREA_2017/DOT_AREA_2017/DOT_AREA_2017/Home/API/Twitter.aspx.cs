using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mailinblue;
using Vianett.Common.Log4net;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Facebook;
using Tweetinvi;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using DOT_AREA_2017.Home.Others;

namespace DOT_AREA_2017
{
    public partial class Twitter : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
        }

        protected void ButtonMail_Click(object sender, EventArgs e)
        {
            TwitterPerso TP = new TwitterPerso();

            string message = TextTwitter.InnerText;

            string oauth_consumer_key = TP.getConsumerKey();
            string oauth_consumer_secret = TP.getConsumerSecret();
            string oauth_token = TP.getToken();
            string oauth_token_secret = TP.getTokenSecret();

            ///////////////////////////////////////////////// SEND IN BLUE //////////////////////////////////////////////////
            API sendinBlue = new mailinblue.API("ERWXqjT4mS3aA05V", 5000);    //Optional parameter: Timeout in MS
            Dictionary<string, Object> data = new Dictionary<string, Object>();
            Dictionary<string, string> to = new Dictionary<string, string>();
            to.Add(TextDestination.Text, "to whom!");
            List<string> from_name = new List<string>();
            from_name.Add("area.dotnet@gmail.com");
            from_name.Add("AREA.NET");
            //List<string> attachment = new List<string>();
            //attachment.Add("https://domain.com/path-to-file/filename1.pdf");
            //attachment.Add("https://domain.com/path-to-file/filename2.jpg");

            data.Add("to", to);
            data.Add("from", from_name);
            data.Add("subject", "AREA send in blue");
            data.Add("html", String.Concat("You post the following message on Twitter : ", message));
            //data.Add("attachment", attachment);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Auth.SetUserCredentials(oauth_consumer_key, oauth_consumer_secret, oauth_token, oauth_token_secret);
            try
            {
                LabelResult.ForeColor = System.Drawing.Color.Green;
                LabelResult.Visible = true;
                LabelResult.Text = "Message posted on Twitter and sended to " + TextDestination.Text;
                Object sendEmail = sendinBlue.send_email(data);
                Tweet.PublishTweet(message);
            }
            catch (Exception ex)
            {
                LabelResult.ForeColor = System.Drawing.Color.Red;
                LabelResult.Visible = true;
                LabelResult.Text = ex.Message;
            }

        }

        protected void ButtonPhone_Click(object sender, EventArgs e)
        {
            string message = TextTwitter.InnerText;

            TwitterPerso TP = new TwitterPerso();
            string oauth_consumer_key = TP.getConsumerKey();
            string oauth_consumer_secret = TP.getConsumerSecret();
            string oauth_token = TP.getToken();
            string oauth_token_secret = TP.getTokenSecret();

            Auth.SetUserCredentials(oauth_consumer_key, oauth_consumer_secret, oauth_token, oauth_token_secret);

            ///////////////////////////////////////////////// VIANETT //////////////////////////////////////////////////
            string username = "xiaclement@gmail.com";
            string password = "ras3r";
            string msgsender = "33683821203";
            string destinationaddr = string.Concat("33", TextDestination.Text.Remove(0, 1).ToString());
            

            ViaNettSMS s = new ViaNettSMS(username, password);
            ViaNettSMS.Result result;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                Tweet.PublishTweet(message);
                result = s.sendSMS(msgsender, destinationaddr, String.Concat("You post on Twitter : ", message));
                LabelResult.ForeColor = System.Drawing.Color.Green;
                LabelResult.Visible = true;
                LabelResult.Text = "Message posted on Twitter and sended to " + TextDestination.Text + " !";
            }
            catch (Exception ex)
            {
                LabelResult.ForeColor = System.Drawing.Color.Red;
                LabelResult.Visible = true;
                LabelResult.Text = ex.Message;
            }
        }

        protected void ButtonFb_Click(object sender, EventArgs e)
        {
            string message = TextTwitter.InnerText;


            TwitterPerso TP = new TwitterPerso();
            string oauth_consumer_key = TP.getConsumerKey();
            string oauth_consumer_secret = TP.getConsumerSecret();
            string oauth_token = TP.getToken();
            string oauth_token_secret = TP.getTokenSecret();

            ///////////////////////////////////////////////// FACEBOOK //////////////////////////////////////////////////
            string accessToken = "EAACEdEose0cBAKCPP3ut9E354xwmlPCcby2JP8gNGXYYeR6ZC4EwSrx7BjnI1UoDJqiG1lWr4BOE7zU6pZCalddnQvH7wJPkHeA2vaZBZC1s7lBbGmIYnjHfYu4dvfZAYjIUOuHW7kP3E13Fr7cTzyy8PeYNRdp4UZBeZAZASD2kIKY9tIGPwM6zxdUbV2xQVAOz3hAfbn45XgZDZD";
            var objFacebookClient = new FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["message"] = message;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Auth.SetUserCredentials(oauth_consumer_key, oauth_consumer_secret, oauth_token, oauth_token_secret);

            try
            {
                objFacebookClient.Post("feed", parameters);
                Tweet.PublishTweet(message);
            }
            catch (Exception ex)
            {
                LabelResult.ForeColor = System.Drawing.Color.Red;
                LabelResult.Visible = true;
                LabelResult.Text = ex.Message;
            }   
        }

        protected void ButtonTrigger_Click(object sender, EventArgs e)
        {

            TwitterPerso TP = new TwitterPerso();
            string oauth_consumer_key = TP.getConsumerKey();
            string oauth_consumer_secret = TP.getConsumerSecret();
            string oauth_token = TP.getToken();
            string oauth_token_secret = TP.getTokenSecret();

            Auth.SetUserCredentials(oauth_consumer_key, oauth_consumer_secret, oauth_token, oauth_token_secret);

            var stream = Tweetinvi.Stream.CreateUserStream();
            stream.FollowedUser += (senderr, args) =>
            {
                API sendinBlue = new mailinblue.API("ERWXqjT4mS3aA05V", 5000);    //Optional parameter: Timeout in MS
                Dictionary<string, Object> data = new Dictionary<string, Object>();
                Dictionary<string, string> to = new Dictionary<string, string>();
                to.Add("area.dotnet@gmail.com", "to whom!");
                List<string> from_name = new List<string>();
                from_name.Add("area.dotnet@gmail.com");
                from_name.Add("AREA.NET");

                data.Add("to", to);
                data.Add("from", from_name);
                data.Add("subject", "AREA send in blue");
                data.Add("html", "Pop , you followed " + args.User + " on Twitter :)");
                Object sendEmail = sendinBlue.send_email(data);
                Tweet.PublishTweet("I followed " + args.User + " on Twitter");

            };
            stream.StartStream();

        }

        protected void ButtonUnfollowed_Click(object sender, EventArgs e)
        {
            TwitterPerso TP = new TwitterPerso();
            string oauth_consumer_key = TP.getConsumerKey();
            string oauth_consumer_secret = TP.getConsumerSecret();
            string oauth_token = TP.getToken();
            string oauth_token_secret = TP.getTokenSecret();

            Auth.SetUserCredentials(oauth_consumer_key, oauth_consumer_secret, oauth_token, oauth_token_secret);

            var stream = Tweetinvi.Stream.CreateUserStream();
            stream.UnFollowedUser += (senderr, args) =>
            {
                string message = "I unfollowed " + args.User + " on Twitter :)";
                string accessToken = "EAACEdEose0cBAEdgdkCpfUkjqz5ZBZCe8GkMRGd3SYDHrcIcUuiofEVSUpoPmhfLMwk7rHP6Ub4FQMh5ZBT8d1TcYZBCvCY9cYB8kZACAvaRjRbJEXxlfqUEt0yi7jf1HOBwe1BxJWkbtwNNa9wicotvQbC67nulGv06YRza5rH3YPM4sP8ZAeyODXjfNbqIY9QXYKBPFJIwZDZD";
                var objFacebookClient = new FacebookClient(accessToken);
                var parameters = new Dictionary<string, object>();
                parameters["message"] = message;

                try
                {
                    Tweet.PublishTweet(message);
                    objFacebookClient.Post("feed", parameters);
                }
                catch (FacebookApiException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };
            stream.StartStream();
        }

        protected void ButtonDrop_Click(object sender, EventArgs e)
        {

            TwitterPerso TP = new TwitterPerso();
            string oauth_consumer_key = TP.getConsumerKey();
            string oauth_consumer_secret = TP.getConsumerSecret();
            string oauth_token = TP.getToken();
            string oauth_token_secret = TP.getTokenSecret();

            Auth.SetUserCredentials(oauth_consumer_key, oauth_consumer_secret, oauth_token, oauth_token_secret);
            int i = 0;
            var stream = Tweetinvi.Stream.CreateUserStream();
            stream.TweetCreatedByMe += (senderr, args) =>
            {
                DropboxClient DBClient = new DropboxClient("L1EBtFath0AAAAAAAAAADV7jbG16YHUWMXrqSsQYdb_tFUv3G6JYlbW26bSqzyr0");
                try
                {
                    string dest = String.Format("D:/Epitech/DOT_AREA_2017/picture({0}).jpg", i++);
                    var media = args.Tweet.Media[0].MediaURLHttps;
                    System.Diagnostics.Debug.WriteLine(media);

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(media, dest);
                    }
                    using (var streams = new MemoryStream(File.ReadAllBytes(dest)))
                    {
                        var response = DBClient.Files.UploadAsync("/area/" + dest, WriteMode.Overwrite.Instance, body: streams);
                        var result = response.Result;
                    }
                }
                catch (Exception ex)
                {
                    LabelResult.Visible = true;
                    LabelResult.Text = ex.Message;
                }
            };
            stream.StartStream();
        }

        protected void ButtonTriggerSMS_Click(object sender, EventArgs e)
        {
            TwitterPerso TP = new TwitterPerso();
            string oauth_consumer_key = TP.getConsumerKey();
            string oauth_consumer_secret = TP.getConsumerSecret();
            string oauth_token = TP.getToken();
            string oauth_token_secret = TP.getTokenSecret();

            string username = "xiaclement@gmail.com";
            string password = "ras3r";
            string msgsender = "33683821203";
            string destinationaddr = string.Concat("33", TextDestination.Text.Remove(0, 1).ToString());


            ViaNettSMS s = new ViaNettSMS(username, password);
            ViaNettSMS.Result result;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var stream = Tweetinvi.Stream.CreateUserStream();
            stream.BlockedUser += (senderr, args) =>
            {
                try
                {
                    //Tweet.PublishTweet(message);
                    result = s.sendSMS(msgsender, destinationaddr, "Hey ! Why you blocked " + args.User + " on Twitter :(");
                    LabelResult.ForeColor = System.Drawing.Color.Green;
                    LabelResult.Visible = true;
                    LabelResult.Text = "Message posted on Twitter and sended to " + TextDestination.Text + " !";
                }
                catch (Exception ex)
                {
                    LabelResult.ForeColor = System.Drawing.Color.Red;
                    LabelResult.Visible = true;
                    LabelResult.Text = ex.Message;
                }
            };
            stream.StartStream();
        }
    }
}