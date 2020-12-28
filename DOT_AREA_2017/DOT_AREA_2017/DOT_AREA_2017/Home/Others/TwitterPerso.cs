using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOT_AREA_2017.Home.Others
{
    public class TwitterPerso
    {
        private string oauth_consumer_key = "Cq9CVm4dCDhD3hZASE37ddWP1";
        private string oauth_consumer_secret = "te7n7cQKDRT34LxZQ6oj7SDuzabBmGpixyb8EUzF1Tc7d0bagy";
        private string oauth_token = "929416438162640902-kEhpyU7rQZyzzGQL8hroHy2owKct2jQ";
        private string oauth_token_secret = "f0t5Q4jdmXGcDDqqU1o8x4P8P63bQFq3OGMGmw7ObMyk9";

        public TwitterPerso()
        {

        }

        public string getConsumerKey()
        {
            return (this.oauth_consumer_key);
        }

        public string getConsumerSecret()
        {
            return (this.oauth_consumer_secret);
        }

        public string getToken()
        {
            return (this.oauth_token);
        }

        public string getTokenSecret()
        {
            return (this.oauth_token_secret);
        }

    }
}