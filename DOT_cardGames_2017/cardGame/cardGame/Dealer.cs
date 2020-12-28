using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace cardGame
{
    public class Dealer
    {
        private List<Card> _shoe = new List<Card>();
        private Hand _hand = new Hand();
        private int _numberOfDeck;

        public Dealer(int numberOfDeck)
        {
            _numberOfDeck = numberOfDeck;
            GenerateShoe();
        }

        public Card Card
        {
            get => default(Card);
            set
            {
            }
        }

        public void ClearShoe() => _shoe.Clear();

        //Create a new shoe from "_numberOfDeck"
        public void GenerateShoe()
        {
            for (int count = 0; count < _numberOfDeck; count++)
            {
                for (int i = 1; i <= 13; i++)
                    _shoe.Add(new Card(i, Suit.HEART));
                for (int i = 1; i <= 13; i++)
                    _shoe.Add(new Card(i, Suit.SPADE));
                for (int i = 1; i <= 13; i++)
                    _shoe.Add(new Card(i, Suit.DIAMOND));
                for (int i = 1; i <= 13; i++)
                    _shoe.Add(new Card(i, Suit.CLUB));
            }
            Shuffle();
        }

        public void Shuffle()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = _shoe.Count;

            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                Card card = _shoe[k];
                _shoe[k] = _shoe[n];
                _shoe[n] = card;
            }
        }

        public void DealCard(Player player, int index = 0)
        {
            Card card = _shoe[0];

            _shoe.RemoveAt(0);
            if (player == null)
            {
                TakeCard(card);
            }
            else
            {
                player.TakeCard(card, index);
            }
        }

        public int GetShoeCount() => _shoe.Count;

        public void TakeCard(Card card) => _hand.TakeCard(card);

        public void ClearHand(int index = 0)
        {
            _hand.ClearHand(index);
        }

        public int GetHandValue(int index = 0) => _hand.GetHandValue(index);

        public void DisplayHandToPlayer(Player player, bool dealer = false) => player.Write(_hand.GetDisplayHandMsg(0, dealer));

        public void DisplayHideHandToPlayer(Player player) => player.Write(_hand.GetDisplayHidHandMsg());

        public bool IsBusted() => _hand.IsBusted();
    }
}