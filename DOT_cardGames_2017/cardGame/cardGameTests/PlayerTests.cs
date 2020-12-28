using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetty.Transport.Channels;
using System.Collections.Generic;
using cardGame;

namespace cardGameTests
{
    [TestClass]
    public class PlayerTests
    {
       IChannelHandlerContext ctx;

        [TestMethod]
        public void TestGetName()
        {
            Player player = new Player(ctx, "player_test");

            Assert.AreEqual("player_test", player.GetName());
            Assert.AreNotEqual("blabla", player.GetName());
            Assert.AreNotEqual("zfjzogj", player.GetName());
        }

        [TestMethod]
        public void TestIsBusted()
        {
            Player player = new Player(ctx, "player_test");

            Assert.AreEqual(false, player.IsBusted());
            Assert.AreNotEqual(true, player.IsBusted());
        }
    }
}
