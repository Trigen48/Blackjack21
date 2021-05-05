using System.Collections.Generic;
using Blackjack21.Game.Exceptions;
using Blackjack21.Game.Model;
using Blackjack21.Game.Logic;

namespace Blackjack21.Game
{
    /// <summary>
    /// A game class containing blackjack game logic
    /// </summary>
    public class BlackjackGame
    {

        private readonly Player _dealer;
        private readonly List<Player> _players;
        private readonly CardDeck _deck;


        /// <summary>
        /// initialize the blackjack game
        /// </summary>
        public BlackjackGame()
        {
            this._dealer = new Player("Blackjack Dealer");
            this._players = new List<Player>();
            this._deck = new CardDeck();
        }

        /// <summary>
        /// Initialize a new deck
        /// </summary>
        /// <param name="deckCount"></param>
        public void InitializeDeck(int deckCount = 1)
        {
            this._deck.NewDeck(deckCount);
        }

        /// <summary>
        /// Start a new round of blackjack, throws an exception if a deck has not been initialized
        /// </summary>
        public void NewGame()
        {
            const int MIN_DECK_CARDS = 10;

            this._dealer.ClearHands();

            foreach (Player player in this._players)
            {
                player.ClearHands();
            }

            if (this._deck.CardCount <= MIN_DECK_CARDS)
            {
                this._deck.ShuffleDeck();
            }
        }

        /// <summary>
        /// Find the player's index in the array and return -1 if the player was not found
        /// </summary>
        /// <param name="playerName">The player's name to search</param>
        /// <returns>Return the player's index</returns>
        private int FindPlayerIndex(string playerName)
        {
            return this._players.FindIndex(delegate (Player player) { return player.PlayerName == playerName; }); ;
        }

        /// <summary>
        /// Add two cards to the dealer's hand and players hand in turns
        /// </summary>
        public void DealCards()
        {
            const int CARD_DEAL_MAX = 2;

            for (int cardsToDeal = 0; cardsToDeal < CARD_DEAL_MAX; cardsToDeal++)
            {
                foreach (Player player in this._players)
                {
                    player.AddFirstHandCard(this.HitCard());
                }

                this._dealer.AddFirstHandCard(this.HitCard());
            }

            this._dealer.FirstHand.SetDealerResult();
        }

        /// <summary>
        /// Returns the top most card from the deck
        /// </summary>
        /// <returns>Retorn the top removed card</returns>
        public Card HitCard()
        {
            return this._deck.HitCard();
        }

        /// <summary>
        /// Add cards to the dealer's hand if the dealer's hand value is under 17
        /// </summary>
        public void DealerHit()
        {
            const int MINIMUM_DEALER_VALUE = 17;

            while(this._dealer.FirstHandValue < MINIMUM_DEALER_VALUE)
            {
                this.AddDealerCard(this.HitCard());
            }
        
        }

        /// <summary>
        /// Add a new player to the game table, throws an exception if the player name already exists
        /// </summary>
        /// <param name="playerName">The new players name</param>
        public void AddPlayer(string playerName)
        {
            // look for the player name in the list
            int index = this._players.FindIndex(delegate (Player player) { return player.PlayerName == playerName; });
            if (index != -1)
            {
                throw new PlayerExistsException(playerName);
            }

            this._players.Add(new Player(playerName));
        }

        /// <summary>
        /// Add a card to the dealer's hand
        /// </summary>
        /// <param name="card">A Card to add to the dealer's hand</param>
        public void AddDealerCard(Card card)
        {
            this._dealer.AddFirstHandCard(card);
        }

        /// <summary>
        /// Add cards to the dealer's hand
        /// </summary>
        /// <param name="cards">Cards to add to the dealer's hand</param>
        public void AddDealerCards(Card[] cards)
        {
            foreach (Card card in cards)
            {
                AddDealerCard(card);
            }
        }

        /// <summary>
        /// Add a card to the players hand, throws an exception if the player is not found
        /// </summary>
        /// <param name="index">The index order of the player</param>
        /// <param name="card">A card to add to the player's hand</param>
        public void AddPlayerCard(int index, Card card)
        {
            var player = this[index];
            player.AddFirstHandCard(card);
        }

        /// <summary>
        /// Add a card to the player's hand, throws an exception if the player is not found
        /// </summary>
        /// <param name="index">The index order of the player</param>
        /// <param name="cards">Cards to add to the player's hand</param>
        public void AddPlayerCards(int index, Card[] cards)
        {
            foreach (Card card in cards)
            {
                AddPlayerCard(index, card);
            }
        }

        /// <summary>
        /// Add a card to a players hand using their name, throws an exception if the player is not found
        /// </summary>
        /// <param name="PlayerName">The player's name</param>
        /// <param name="card">The card to add to the player's hand</param>
        public void AddPlayerCard(string playerName, Card card)
        {
            int index = FindPlayerIndex(playerName);
            AddPlayerCard(index, card);
        }

        /// <summary>
        /// Add cards to a players hand using their name, throws an exception if the player is not found
        /// </summary>
        /// <param name="PlayerName">The player's name</param>
        /// <param name="cards">Cards to add to the player's hand</param>
        public void AddPlayerCards(string playerName, Card[] cards)
        {
            int index = FindPlayerIndex(playerName);
            AddPlayerCards(index, cards);
        }

        public Player Dealer
        {
            get
            {
                return this._dealer;
            }
        }

        /// <summary>
        /// Get the player using the index number, throws a player not found exception if the player index is not correct
        /// </summary>
        /// <param name="index">Player's++ index number</param>
        /// <returns>Return the player object</returns>
        public Player this[int index]
        {
            get
            {
                if (this._players.Count > index && index >= 0)
                {
                    return this._players[index];
                }

                throw new PlayerNotFoundException(index);
            }
        }

        /// <summary>
        /// Return the player object uing the player's name, throws an exception if the player is not found
        /// </summary>
        /// <param name="playerName">The player name to search</param>
        /// <returns>Returns the player object</returns>
        public Player this[string playerName]
        {
            get
            {
                int index = FindPlayerIndex(playerName);

                if (index != -1)
                {
                    return this[index];
                }

                throw new PlayerNotFoundException(playerName);
            }
        }


        /// <summary>
        /// Complete the current round of blackjack
        /// </summary>
        public void ConcludeRound()
        {

            this._dealer.FirstHand.SetDealerResult();
            foreach (Player player in _players)
            {
                player.SetHandResult(this._dealer);
            }
        }

        public override string ToString()
        {
            string result = "+ Dealer Details\n\n";

            result += this._dealer.ToString();

            result+= "+ Players\n\n";
            foreach (Player player in this._players)
            {
                result += player.ToString() ;
            }


            return result;
        }

    }
}
