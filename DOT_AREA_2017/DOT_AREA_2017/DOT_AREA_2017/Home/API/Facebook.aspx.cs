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

namespace DOT_AREA_2017
{
    public partial class Facebook : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonMail_Click(object sender, EventArgs e)
        {
            string message = TextFacebook.InnerText;
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
            data.Add("html", String.Concat("You post the following message on Facebook : ", message));
            //data.Add("attachment", attachment);

            Object sendEmail = sendinBlue.send_email(data);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string accessToken = "EAAMDizLaZCQABAMqAXjW5FsQyRtLnaDlJaszDyep74w6srKfMP6ZC4ASW4KeRoLlmfrosZBXSRfsbRTRpHuM4R4REqtMMlF3TnNbyeMcpPQQV06fuwSnNuOPvdxjCSBpboIlMIZCDXP0UaExAha5dXCUUCkHAZB2H838yIaHmDZBYrk2B4p6w492apZAEHkAtKGsNpKTVVdawZDZD";
            var objFacebookClient = new FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["message"] = message;
            try
            {
                objFacebookClient.Post("feed", parameters);
                LabelResult.ForeColor = System.Drawing.Color.Green;
                LabelResult.Visible = true;
                LabelResult.Text = "Message posted on Facebook and sended to " + TextDestination.Text + " !";
            }
            catch (FacebookApiException fbex)
            {
                LabelResult.Visible = true;
                LabelResult.Text = fbex.Message;
            }
        }

        protected void ButtonPhone_Click(object sender, EventArgs e)
        {
            string message = TextFacebook.InnerText;
            string accessToken = "EAAMDizLaZCQABAMqAXjW5FsQyRtLnaDlJaszDyep74w6srKfMP6ZC4ASW4KeRoLlmfrosZBXSRfsbRTRpHuM4R4REqtMMlF3TnNbyeMcpPQQV06fuwSnNuOPvdxjCSBpboIlMIZCDXP0UaExAha5dXCUUCkHAZB2H838yIaHmDZBYrk2B4p6w492apZAEHkAtKGsNpKTVVdawZDZD";
            var objFacebookClient = new FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["message"] = message;

            //////////////////////////////////////////// VIANETT /////////////////////////////////////////
            string username = "xiaclement@gmail.com";
            string password = "ras3r";
            string msgsender = "33683821203";
            string destinationaddr = string.Concat("33", TextDestination.Text.Remove(0, 1).ToString());

            ViaNettSMS s = new ViaNettSMS(username, password);
            ViaNettSMS.Result result;
            try
            {
                objFacebookClient.Post("feed", parameters);
                result = s.sendSMS(msgsender, destinationaddr, String.Concat("You post on Facebook : ", message));
                LabelResult.ForeColor = System.Drawing.Color.Green;
                LabelResult.Visible = true;
                LabelResult.Text = "Message posted on Facebook and sended to " + TextDestination.Text + " !";
            }
            catch (FacebookApiException fbex)
            {
                LabelResult.Visible = true;
                LabelResult.Text = fbex.Message;
            }
        }

        protected void ButtonFb_Click(object sender, EventArgs e)
        {
            string message = TextFacebook.InnerText;
            //The facebook json url to update the status
            string twitterURL = "https://api.twitter.com/1.1/statuses/update.json";

            //set the access tokens (REQUIRED)
            string oauth_consumer_key = "OJiBjYwsTTPUHpgIpGbdDfoZZ";
            string oauth_consumer_secret = "xF2ExNCND6fTwzbt59vvusx3OFo1loCGKMaBlm47XVAwwLj4fU";
            string oauth_token = "929416438162640902-kEhpyU7rQZyzzGQL8hroHy2owKct2jQ";
            string oauth_token_secret = "f0t5Q4jdmXGcDDqqU1o8x4P8P63bQFq3OGMGmw7ObMyk9";

            // set the oauth version and signature method
            string oauth_version = "1.0";
            string oauth_signature_method = "HMAC-SHA1";

            // create unique request details
            string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            System.TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            // create oauth signature
            string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";

            string baseString = string.Format(
                baseFormat,
                oauth_consumer_key,
                oauth_nonce,
                oauth_signature_method,
                oauth_timestamp, oauth_token,
                oauth_version,
                Uri.EscapeDataString(message)
            );

            string oauth_signature = null;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(Uri.EscapeDataString(oauth_consumer_secret) + "&" + Uri.EscapeDataString(oauth_token_secret))))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes("POST&" + Uri.EscapeDataString(twitterURL) + "&" + Uri.EscapeDataString(baseString))));
            }

            // create the request header
            string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

            string authorizationHeader = string.Format(
                authorizationFormat,
                Uri.EscapeDataString(oauth_consumer_key),
                Uri.EscapeDataString(oauth_nonce),
                Uri.EscapeDataString(oauth_signature),
                Uri.EscapeDataString(oauth_signature_method),
                Uri.EscapeDataString(oauth_timestamp),
                Uri.EscapeDataString(oauth_token),
                Uri.EscapeDataString(oauth_version)
            );

            HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(twitterURL);
            objHttpWebRequest.Headers.Add("Authorization", authorizationHeader);
            objHttpWebRequest.Method = "POST";
            objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            using (Stream objStream = objHttpWebRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(message));
                objStream.Write(content, 0, content.Length);
            }

            var responseResult = "";
            ///////////////////////////////////////////////// FACEBOOK //////////////////////////////////////////////////
            string accessToken = "EAAMDizLaZCQABAMqAXjW5FsQyRtLnaDlJaszDyep74w6srKfMP6ZC4ASW4KeRoLlmfrosZBXSRfsbRTRpHuM4R4REqtMMlF3TnNbyeMcpPQQV06fuwSnNuOPvdxjCSBpboIlMIZCDXP0UaExAha5dXCUUCkHAZB2H838yIaHmDZBYrk2B4p6w492apZAEHkAtKGsNpKTVVdawZDZD";
            var objFacebookClient = new FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["message"] = message;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                objFacebookClient.Post("feed", parameters);
                //success posting
                WebResponse objWebResponse = objHttpWebRequest.GetResponse();
                StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                responseResult = objStreamReader.ReadToEnd().ToString();
            }
            catch (FacebookApiException fbex)
            {
                LabelResult.Visible = true;
                LabelResult.Text = fbex.Message;
            }
            catch (Exception ex)
            {
                //throw exception error
                responseResult = "Twitter Post Error: " + ex.Message.ToString() + ", authHeader: " + authorizationHeader;
            }
        }
    }
}