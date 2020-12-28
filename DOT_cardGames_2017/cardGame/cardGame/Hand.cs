using System.Collections.Generic;

namespace cardGame
{
    public class Hand
    {
        private List<List<Card>> _hands;
        private List<bool> _hasPlay;

        public Hand()
        {
            _hands = new List<List<Card>>();
            _hands.Add(new List<Card>());
            _hasPlay = new List<bool>();
            _hasPlay.Add(false);
        }

        public Card Card
        {
            get => default(Card);
            set
            {
            }
        }

        public void SetHasPlay(int index, bool play) => _hasPlay[index] = play;

        public bool GetHasPlay(int index) => _hasPlay[index];

        public void ClearHand(int index = 0)
        {
            if (_hands[0].Count != 0)
                _hands[index].Clear();
        }

        public void TakeCard(Card card, int index = 0) => _hands[index].Add(card);

        public int GetHandValue(int index = 0)
        {
            int value = 0;
            int cardValue;

            foreach (var card in _hands[index])
            {
                cardValue = card.GetValue();
                if (cardValue == 1)
                    value += (value + 11 > 21 ? 1 : 11);
                else
                    value += cardValue;
            }
            return value;
        }

        public string GetDisplaySingleHandMsg(int index = 0, bool dealer = false)
        {
            string msg = "";
            int value = GetHandValue();
            bool first = true;

            msg += value.ToString();
            msg += " (";
            foreach (var card in _hands[0])
            {
                if (first == false)
                    msg += " + ";
                msg += card.GetCardString();
                first = false;
            }
            msg += ")";
            if (dealer)
                return "Dealer's hand:\r\n\t" + msg;
            return "Your hand:\r\n\t" + msg;
        }

        public string GetDisplaySplitHandMsg()
        {
            string msg = "";
            bool first = true;

            for (int i = 0; i < _hands.Count; i++)
            {
                if (first == false)
                    msg += "[Server]: ";
                msg += GetDisplaySingleHandMsg(i);
                first = false;
            }
            return msg;
        }

        public string GetDisplayHandMsg(int index = 0, bool dealer = false)
        {
            if (_hands.Count > 1)
                return GetDisplaySplitHandMsg();
            return GetDisplaySingleHandMsg(index, dealer);
        }

        public string GetDisplayHidHandMsg()
        {
            string msg = "";
            int value = _hands[0][0].GetValue();

            msg += value.ToString() +
                " (" + _hands[0][0].GetCardString() + ")";
            return "Dealer's hand:\r\n\t" + msg;
        }

        public bool IsBusted(int index = 0)
        {
            int value = GetHandValue(index);

            if (value > 21)
                return true;
            else
                return false;
        }

        public List<Card> GetHand(int index = 0)
        {
            if (index < _hands.Count)
                return _hands[index];
            return null;
        }

        public void SplitCard(int index = 0)
        {
            Card card = _hands[index][1];

            _hands[index].RemoveAt(1);
            _hands[index + 1] = new List<Card>();
            _hands[index + 1].Add(card);
            _hasPlay.Add(false);
        }

        public bool IsDoublable(int index)
        {
            if (_hands[index].Count == 2)
                return true;
            return false;
        }

        public bool IsSplittable(int index)
        {
            if (_hands[index][0].GetValue() == _hands[index][1].GetValue())
                return true;
            return false;
        }

        public string GetPlayerActionMsg()
        {
            string msg = "Would you like to HIT, STAND";

            if (IsDoublable(0))
                msg += ", DOUBLE";
            msg += " ?";
            return msg;
        }

        public bool IsEmpty()
        {
            if (_hands[0].Count == 0)
                return true;
            return false;
        }
    }
}