using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using epicture;

namespace epictureTest
{
    [TestClass]
    public class ImgurTest
    {
        private Imgur_client imgur { get; set; }

        [TestInitialize]
        public void Initializer()
        {
            this.imgur = new Imgur_client();
        }

        [TestMethod]
        public void client_info()
        { 
            string id = "4c87c24c218e973";
            string sct = "93e8a179129b8393c0256c60a57a0abd40da4b6b";

            Assert.AreEqual(this.imgur.app_id, id);
            Assert.AreEqual(this.imgur.secret, sct);
        }

        [TestMethod]
        public void username()
        {
            string name = null;

            Assert.AreEqual(this.imgur.account_username, name);
        }

        [TestMethod]
        public void testHttpGet()
        {
            string x = "false";
             Task.Run(async () =>
            {
                string str = await this.imgur.HttpGet("blabla");
                Assert.AreNotEqual(str, x);
            });
        }

        [TestMethod]
        public void testHttpDelete()
        {
            string x = "false";
            Task.Run(async () =>
            {
                string str = await this.imgur.HttpGet("sfsdmlfksdmlfksdfksml");
                Assert.AreNotEqual(str, x);
            });
        }

    }
}
