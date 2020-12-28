using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

namespace epicture
{
    public sealed partial class MainPage : Page
    {
        Imgur_client client_info;
        AutoResetEvent navCompleted;

        public MainPage()
        {
            this.InitializeComponent();
            navCompleted = new AutoResetEvent(false);
        }

        private async void ImgurLogin_OnClick(object sender, RoutedEventArgs e)
        {
            Uri authUri = new Uri("https://api.imgur.com/oauth2/authorize?&client_id=4c87c24c218e973&response_type=code");

            LoginContentFlickr.Visibility = Visibility.Collapsed;
            LoginContent.Visibility = Visibility.Collapsed;
            ImgurLogin.Visibility = Visibility.Visible;

            ImgurLogin.Navigate(authUri);

            await Task.Run(() => { navCompleted.WaitOne(); });

            navCompleted.Reset();
            ImgurLogin.Visibility = Visibility.Collapsed;
            ImgurLogin.Visibility = Visibility.Visible;
        }

        private async void CompletedNav(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            String[] res = args.Uri.ToString().Substring(args.Uri.ToString().IndexOf('#') + 1).Split('=');

            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                    {
                        { "client_id", "4c87c24c218e973" },
                        { "client_secret", "93e8a179129b8393c0256c60a57a0abd40da4b6b" },
                        { "grant_type", "authorization_code" },
                        { "code", res[1] }
                    };
                FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://api.imgur.com/oauth2/token", content);
                string ret = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(ret);
                client_info = JsonConvert.DeserializeObject<Imgur_client>(ret);
                if (client_info.account_id != 0)
                {
                    this.Frame.Navigate(typeof(HomeImgur), client_info);
                }
                else
                {
                    Imgur_Request request = JsonConvert.DeserializeObject<Imgur_Request>(ret);
                    Response.Text = "Error : " + request.data.error;
                }
            }
        }

        private void FailedNav(object sender, WebViewNavigationFailedEventArgs e)
        {
            Response.Visibility = Visibility.Visible;
            Response.Text = "Failed to navigate Imgur.";
        }

        private void FlickrLogin_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomeFlickr));
        }

        private void FailedNavFlickr(object sender, WebViewNavigationFailedEventArgs e)
        {
            Response.Visibility = Visibility.Visible;
            Response.Text = "Failed to navigate Flickr.";
        }

        private void CompletedNavFlickr(WebView sender, WebViewNavigationCompletedEventArgs args)
        {

        }
    }


}
