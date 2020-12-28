using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cardGame;

namespace cardGameTests
{


    [TestClass]
    public class CardTests
    {
        Card cardH1 = new Card(1, cardGame.Suit.HEART);
        Card cardD1 = new Card(1, cardGame.Suit.DIAMOND);
        Card cardS1 = new Card(1, cardGame.Suit.SPADE);
        Card cardC1 = new Card(1, cardGame.Suit.CLUB);

        Card cardH10 = new Card(10, cardGame.Suit.HEART);
        Card cardD10 = new Card(10, cardGame.Suit.DIAMOND);
        Card cardS10 = new Card(10, cardGame.Suit.SPADE);
        Card cardC10 = new Card(10, cardGame.Suit.CLUB);

        [TestMethod]
        public void TestGetnumber()
        {
            Assert.AreEqual(1, cardH1.Getnumber());
            Assert.AreEqual(1, cardD1.Getnumber());
            Assert.AreEqual(1, cardS1.Getnumber());
            Assert.AreEqual(1, cardC1.Getnumber());

            Assert.AreEqual(10, cardH10.Getnumber());
            Assert.AreEqual(10, cardD10.Getnumber());
            Assert.AreEqual(10, cardS10.Getnumber());
            Assert.AreEqual(10, cardC10.Getnumber());

            Assert.AreNotEqual(12, cardH1.Getnumber());
            Assert.AreNotEqual(13, cardD1.Getnumber());
            Assert.AreNotEqual(14, cardS1.Getnumber());
            Assert.AreNotEqual(15, cardC1.Getnumber());

            Assert.AreNotEqual(1, cardH10.Getnumber());
            Assert.AreNotEqual(2, cardD10.Getnumber());
            Assert.AreNotEqual(3, cardS10.Getnumber());
            Assert.AreNotEqual(4, cardC10.Getnumber());
        }

        [TestMethod]
        public void TestGetSuit()
        {
            Assert.AreEqual(Suit.HEART, cardH1.GetSuit());
            Assert.AreEqual(Suit.DIAMOND, cardD1.GetSuit());
            Assert.AreEqual(Suit.SPADE, cardS1.GetSuit());
            Assert.AreEqual(Suit.CLUB, cardC1.GetSuit());

            Assert.AreEqual(Suit.HEART, cardH1.GetSuit());
            Assert.AreEqual(Suit.DIAMOND, cardD1.GetSuit());
            Assert.AreEqual(Suit.SPADE, cardS1.GetSuit());
            Assert.AreEqual(Suit.CLUB, cardC1.GetSuit());

            Assert.AreNotEqual(Suit.HEART, cardC1.GetSuit());
            Assert.AreNotEqual(Suit.DIAMOND, cardS1.GetSuit());
            Assert.AreNotEqual(Suit.SPADE, cardD1.GetSuit());
            Assert.AreNotEqual(Suit.CLUB, cardH1.GetSuit());

            Assert.AreNotEqual(Suit.HEART, cardC1.GetSuit());
            Assert.AreNotEqual(Suit.DIAMOND, cardS1.GetSuit());
            Assert.AreNotEqual(Suit.SPADE, cardD1.GetSuit());
            Assert.AreNotEqual(Suit.CLUB, cardH1.GetSuit());
        }

        [TestMethod]
        public void TestGetCardString()
        {
            Assert.AreEqual("As of heart", cardH1.GetCardString());
            Assert.AreEqual("As of spade", cardS1.GetCardString());
            Assert.AreEqual("As of diamond", cardD1.GetCardString());
            Assert.AreEqual("As of club", cardC1.GetCardString());

            Assert.AreEqual("Ten of heart", cardH10.GetCardString());
            Assert.AreEqual("Ten of spade", cardS10.GetCardString());
            Assert.AreEqual("Ten of diamond", cardD10.GetCardString());
            Assert.AreEqual("Ten of club", cardC10.GetCardString());

            Assert.AreNotEqual("As of hddddddeart", cardH1.GetCardString());
            Assert.AreNotEqual("As of spadffffffffe", cardS1.GetCardString());
            Assert.AreNotEqual("As of diamonddddd", cardD1.GetCardString());
            Assert.AreNotEqual("As of clubfffffff", cardC1.GetCardString());

            Assert.AreNotEqual("As of hzefzef", cardH10.GetCardString());
            Assert.AreNotEqual("As of spadgsdgsdge", cardS10.GetCardString());
            Assert.AreNotEqual("As of diamqrgerg", cardD10.GetCardString());
            Assert.AreNotEqual("As of clsfsdgsdgb", cardC10.GetCardString());
        }
    }
}
