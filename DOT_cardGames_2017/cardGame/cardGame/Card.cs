using System.Collections.Generic;

namespace cardGame
{ 
    public class Card
    {
        private int _number;
        private int _value;
        private Suit _suit;
        private Dictionary<int, string> _dico;

        public Card(int number, Suit suit)
        {
            _number = number;
            _suit = suit;
            _value = (number > 10 ? 10 : number);
            _dico = new Dictionary<int, string>()
            {
                { 1, "As"},
                { 2, "Two"},
                { 3, "Three"},
                { 4, "Four"},
                { 5, "Five"},
                { 6, "Six"},
                { 7, "Seven"},
                { 8, "Eight"},
                { 9, "Nine"},
                { 10, "Ten"},
                { 11, "Jack"},
                { 12, "Queen"},
                { 13, "King"},
            };
        }

        public Suit Suit
        {
            get => default(Suit);
            set
            {
            }
        }

        public int Getnumber()
        {
            return _number;
        }

        public int GetValue()
        {
            return _value;
        }

        public Suit GetSuit()
        {
            return _suit;
        }

        public string GetCardString()
        {
            string str = "";

            str += _dico[_number] + " of " + _suit.ToString().ToLower();
            return str;
        }
    }
}