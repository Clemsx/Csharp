using DotNetty.Transport.Channels;
using System.Collections.Generic;

namespace cardGame
{
    public class Player
    {
        private IChannelHandlerContext _contex;
        private string _name;
        private bool _hasPlay;
        private Hand _hand = new Hand();

        public Player(IChannelHandlerContext contex, string name)
        {
            _contex = contex;
            _name = name;
            _hasPlay = false;
        }

        public Hand Hand
        {
            get => default(Hand);
            set
            {
            }
        }

        public IChannelHandlerContext GetContex() => _contex;

        public string GetName() => _name;

        public void Write(string msg) => _contex.WriteAndFlushAsync("[Server]: " + msg + "\r\n");

        public void InvalidCommand() => _contex.WriteAndFlushAsync("[Server]: invalid command\r\n");

        public void TakeCard(Card card, int index = 0) => _hand.TakeCard(card, index);

        public void ClearHand() => _hand.ClearHand();

        public int GetHandValue(int index = 0) => _hand.GetHandValue(index);

        public void DisplayHand(int index = 0) => Write(_hand.GetDisplayHandMsg(index));

        public bool IsBusted(int index = 0) => _hand.IsBusted(index);

        public List<Card> GetHand(int index = 0) => _hand.GetHand(index);

        public bool GetHasPlay() => _hasPlay;

        public void SetHasPlay(bool b) => _hasPlay = b;

        public void SplitCard(int index = 0) => _hand.SplitCard(index);

        //Ask what to do
        public void AskPlayerAction(int index = 0)
        {          
            Write(_hand.GetPlayerActionMsg());
        }

        public bool IsHandEmpty()
        {
            return _hand.IsEmpty();
        }
    }
}