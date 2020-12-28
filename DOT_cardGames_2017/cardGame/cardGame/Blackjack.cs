using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;

namespace cardGame
{
    public class Blackjack
    {
        private List<Player> _players;
        private int _numberOfPlayer;
        private GameState _state;
        private Player _playerTurn;
        private Dealer _dealer;
        private int _numberOfDeck;

        public Blackjack()
        {
            _players = new List<Player>();
            _numberOfPlayer = 0;
            _state = GameState.LOBBY;
            _playerTurn = null;
            _dealer = null;
            _numberOfDeck = 1;
        }

        public Card Card
        {
            get => default(Card);
            set
            {
            }
        }

        internal GameState GameState
        {
            get => default(GameState);
            set
            {
            }
        }

        public Dealer Dealer
        {
            get => default(Dealer);
            set
            {
            }
        }

        public Blackjack Blackjack1
        {
            get => default(Blackjack);
            set
            {
            }
        }

        public Player Player
        {
            get => default(Player);
            set
            {
            }
        }

        private void CommandShowLobby(Player player)
        {
            foreach (var p in _players)
                player.Write(p.GetName());
        }

        //handle command that can be use at anytime
        private bool GeneralCommand(Player player, string cmd)
        {
            if (string.Equals("showlobby", cmd, StringComparison.OrdinalIgnoreCase))
                CommandShowLobby(player);
            else
                return false;
            return true;
        }

        //Start a new turn of blackjack    
        private void HandOutTwoCardToAll()
        {
            _dealer.ClearHand();
            foreach (var p in _players)
                p.ClearHand();
            _dealer.DealCard(null);
            foreach (var p in _players)
                _dealer.DealCard(p);
            _dealer.DealCard(null);
            foreach (var p in _players)
                _dealer.DealCard(p);
        }

        //print cards to all player
        private void DisplayTurnStateToAllPlayer()
        {
            foreach (var p in _players)
                _dealer.DisplayHideHandToPlayer(p);
            foreach (var p in _players)
                p.DisplayHand();
        }

        //print "it is player's turn"
        public void DisplayPlayerTurnToAll()
        {
            foreach (var p in _players)
            {
                if (p != _playerTurn)
                    p.Write("It is " + _playerTurn.GetName() + "'s turn");
            }
        }



        //Start a new turn of black from "start" command
        private void InitiateNewGame(Player player)
        {
            _dealer = new Dealer(_numberOfDeck);
            _playerTurn = player;
            HandOutTwoCardToAll();
            DisplayTurnStateToAllPlayer();
            DisplayPlayerTurnToAll();
            _playerTurn.AskPlayerAction();
        }

        private void CommandStart(Player player)
        {
            _state = GameState.GAME;
            _playerTurn = player;
            InitiateNewGame(player);
        }

        private void CommandDeck(Player player, string cmd)
        {
            int number = 1;

            if (Int32.TryParse(cmd, out number) == false)
                player.InvalidCommand();
            else if (number < 1 || number > 4)
                player.Write("You can play with 1 to 4 deck only");
            else
            {
                _numberOfDeck = number;
                player.Write("Number of deck set to " + _numberOfDeck);
            }

        }

        //handle command from lobby
        private void LobbyCommand(Player player, string cmd)
        {
            if (string.Equals("start", cmd, StringComparison.OrdinalIgnoreCase))
                CommandStart(player);
            else if (cmd.ToLower().StartsWith("deck ") == true)
                CommandDeck(player, cmd.Substring(5));
            else
                player.InvalidCommand();
        }

        private int GetPlayerByIndex(Player player)
        {
            int i = 0;

            for (i = 0; i < _players.Count; i++)
                if (_players[i] == player)
                    break;
            return i;
        }

        private void SetNextPlayerturn()
        {
            int index = GetPlayerByIndex(_playerTurn);
            int max = _players.Count;
            Player next;

            if (index >= max - 1)
                next = _players[0];
            else
                next = _players[index + 1];
            _playerTurn = next;
            if (_playerTurn.IsHandEmpty() == true && _players.Count > 1)
            {
                SetNextPlayerturn();
            }

        }

        private bool IsEndTurn()
        {
            foreach (var p in _players)
                if (p.GetHasPlay() == false && p.IsHandEmpty() == false)
                    return false;
            return true;
        }

        private void StartNewTurn()
        {
            foreach (var p in _players)
            {
                p.SetHasPlay(false);
                p.Write("Starting new turn !");
            }
            if (_dealer.GetShoeCount() < 15)
            {
                _dealer.ClearShoe();
                _dealer.GenerateShoe();
            }
            HandOutTwoCardToAll();
            DisplayTurnStateToAllPlayer();
            DisplayPlayerTurnToAll();
            _playerTurn.AskPlayerAction();
        }

        private void CalculateWinners()
        {
            if (_dealer.IsBusted())
            {
                foreach (var p in _players)
                {
                    if (p.IsHandEmpty() == false)
                    {
                        if (p.IsBusted())
                            p.Write("You lost this hand");
                        else
                            p.Write("You won this hand !");
                    }
                }
            }
            else
            {
                foreach (var p in _players)
                {
                    if (p.IsHandEmpty() == false)
                    {
                        if (p.IsBusted() || p.GetHandValue() < _dealer.GetHandValue())
                            p.Write("You lost this hand");
                        else if (p.GetHandValue() == _dealer.GetHandValue())
                            p.Write("Draw");
                        else
                            p.Write("You won this hand !");
                    }
                }
            }
        }

        private void DealerTurn()
        {
            foreach (var p in _players)
            {
                if (p.IsHandEmpty() == false)
                {
                    p.Write("Dealer's turn");
                    _dealer.DisplayHandToPlayer(p, true);
                }
            }
            while (_dealer.GetHandValue() < 17)
            {
                _dealer.DealCard(null);
                foreach (var p in _players)
                {
                    if (p.IsHandEmpty() == false)
                        _dealer.DisplayHandToPlayer(p, true);
                }
            }
        }

        private void CommandHit(Player player)
        {
            _dealer.DealCard(player);
            player.DisplayHand();
            if (player.IsBusted() == true)
            {
                player.Write("Busted !");
                player.SetHasPlay(true);
                SetNextPlayerturn();
                if (IsEndTurn() == true)
                {
                    DealerTurn();
                    CalculateWinners();
                    StartNewTurn();
                }
                else
                {
                    DisplayPlayerTurnToAll();
                    _playerTurn.AskPlayerAction();
                }
            }
            else
                _playerTurn.AskPlayerAction();
        }

        private void CommandStand(Player player)
        {
            player.SetHasPlay(true);
            SetNextPlayerturn();
            if (IsEndTurn() == true)
            {
                DealerTurn();
                CalculateWinners();
                StartNewTurn();
            }
            else
            {
                DisplayPlayerTurnToAll();
                _playerTurn.AskPlayerAction();
            }
        }

        private void CommandDouble(Player player)
        {
            _dealer.DealCard(player);
            player.DisplayHand();
            player.SetHasPlay(true);
            SetNextPlayerturn();
            if (player.IsBusted() == true)
                player.Write("Busted !");
            if (IsEndTurn() == true)
            {
                DealerTurn();
                CalculateWinners();
                StartNewTurn();
            }
            else
            {
                DisplayPlayerTurnToAll();
                _playerTurn.AskPlayerAction();
            }
        }

        //handle command related to ingame blackjack
        private void GameCommand(Player player, string cmd)
        {
            if (player != _playerTurn)
                player.InvalidCommand();
            else if (string.Equals("hit", cmd, StringComparison.OrdinalIgnoreCase))
                CommandHit(player);
            else if (string.Equals("stand", cmd, StringComparison.OrdinalIgnoreCase))
                CommandStand(player);
            else if (string.Equals("double", cmd, StringComparison.OrdinalIgnoreCase))
                CommandDouble(player);
            else
                player.InvalidCommand();
        }

        //handle command related to blackjack
        private void BlackjackCommand(Player player, string cmd)
        {
            if (_state == GameState.LOBBY)
                LobbyCommand(player, cmd);
            else if (_state == GameState.GAME)
                GameCommand(player, cmd);
        }

        // main function
        public void BlackjackHandler(IChannelHandlerContext contex, string cmd)
        {
            Player player = GetPlayerByContex(contex);

            if (GeneralCommand(player, cmd) == false)
                BlackjackCommand(player, cmd);
        }

        public void AddPlayer(IChannelHandlerContext contex)
        {
            string name = "Player " + _numberOfPlayer++;
            Player player = new Player(contex, name);
            
            foreach (var p in _players)
            {
                if (p != player)
                    p.Write(player.GetName() + " has join");
            }
            _players.Add(player);
            if (_state == GameState.GAME)
            {
                player.Write("Wait for next turn");
                _playerTurn.AskPlayerAction();
            }
        }

        public Player GetPlayerByContex(IChannelHandlerContext contex)
        {
            Player player = null;

            foreach (var p in _players)
            {
                if (p.GetContex() == contex)
                {
                    player = p;
                    break;
                }
            }
            return player;
        }

        public void RemovePlayerByContex(IChannelHandlerContext contex)
        {
            Player player = GetPlayerByContex(contex);

            foreach (var p in _players)
            {
                if (player != p)
                    p.Write(player.GetName() + " has left");
            }
            _players.Remove(player);
            if (_players.Count == 0)
                _state = GameState.LOBBY;
            if (_state == GameState.GAME)
            {
                SetNextPlayerturn();
                if (IsEndTurn() == true)
                {
                    DealerTurn();
                    CalculateWinners();
                    StartNewTurn();
                }
                else
                {
                    DisplayPlayerTurnToAll();
                    _playerTurn.AskPlayerAction();
                }
            }
        }
    }
}