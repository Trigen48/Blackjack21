using System.Collections.Generic;
using Blackjack21.Game.Model;
using Blackjack21.Game.Exceptions;

namespace Blackjack21.Game.Logic
{
    /// <summary>
    /// A blackjack player object
    /// </summary>
    public class Player
    {
        // Store our hand in a list, a player can have an additional hand if they choose to split.
        private readonly List<PlayerHand> _playerHand; // a player can only split his hand once there for we use an array
        private PlayerHandType _playerHandType;
        private bool _playerInsured;
        private bool _playerIsuranceCorrect;

        private readonly string _playerName;

        /// <summary>
        /// initializes the player objecy
        /// </summary>
        /// <param name="playerName">The player's name</param>
        public Player(string playerName = "Player")
        {
            // set player default hands to 2
            this._playerHand = new List<PlayerHand>();
            this._playerName = playerName;
            /*this._playerHandType = PlayerHandType.SINGLE_HAND;
            this._playerIsuranceCorrect = false;
            this._playerInsured = false;*/

            ClearHands();
        }

        /// <summary>
        /// Returns a value to indicate if the player has a single or split hand
        /// </summary>
        public PlayerHandType CurrentHandType
        {
            get
            {
                return this._playerHandType;
            }
        }

        /// <summary>
        /// Gets the players first hand
        /// </summary>
        public PlayerHand FirstHand
        {
            get
            {
                return this._playerHand[0];
            }
        }

        /// <summary>
        /// Gets the player's split hand, throws an exception if the player does not have a split hand
        /// </summary>
        public PlayerHand SplitHand
        {
            get
            {
                if (this._playerHandType != PlayerHandType.SPLIT_HAND)
                {
                    throw new PlayerHandNotSplitException();
                }
                return this._playerHand[1];
            }
        }

        public void SetIsurance(int dealerHandValue)
        {
            this._playerInsured = true;
            this._playerIsuranceCorrect = dealerHandValue == PlayerHand.MAX_HAND_VALUE;
        }

        /// <summary>
        /// Gets if the player is insured
        /// </summary>
        public bool PlayerInsured
        {
            get
            {
                return _playerInsured;
            }
        }

        /// <summary>
        /// Gets if the players insurance was correct
        /// </summary>
        public bool PlayerIsuranceCorrect
        {
            get
            {
                return this._playerIsuranceCorrect;
            }
        }


        /// <summary>
        /// Returns the player's name, throws an exception if the player name is empty
        /// </summary>
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
        }


        /// <summary>
        /// Gets of the player's hand can be split
        /// </summary>
        public bool CanSplitHand
        {
            get
            {
                return this._playerHandType == PlayerHandType.SINGLE_HAND && this.FirstHand.HasTwoPairs;
            }
        }


        /// <summary>
        /// Gets if the player can double their hand
        /// </summary>
        public bool CanDouble
        {
            get
            {
                return this.FirstHand.CardCount == 2 && this.FirstHandValue >= 9 && this.FirstHandValue <= 11;
            }
        }

        /// <summary>
        /// Gets if the player can add more cards to their hand
        /// </summary>
        public bool CanHit
        {
            get
            {
                return this.FirstHandValue < PlayerHand.MAX_HAND_VALUE;
            }
        }

        /// <summary>
        /// Gets if the player can stand
        /// </summary>
        public bool CanStand
        {
            get
            {
                return this.FirstHandValue < PlayerHand.MAX_HAND_VALUE;
            }
        }

        /// <summary>
        /// Gets if the player can fold their hand
        /// </summary>
        public bool CanFold
        {
            get
            {
                return this.CurrentHandType == PlayerHandType.SINGLE_HAND && this.FirstHand.CardCount == 2;
            }
        }


        /// <summary>
        /// Double the players first hand only
        /// </summary>
        /// <param name="card">Card to add to the doubled hand</param>
        public void DoubleHand(Card card)
        {
            if (!CanDouble)
            {
                throw new InvalidCardActionException("Cannot double the player's hand, conditions not met");
            }
            FirstHand.DoubleHandCard(card);
        }

        /// <summary>
        /// Fold the player's hand
        /// </summary>
        public void Fold()
        {
            if (!CanFold)
            {
                throw new InvalidCardActionException("Cannot fold the player's hand, conditions not met");
            }
            this.FirstHand.Fold();
        }

        /// <summary>
        /// Stand on the player's current hand
        /// </summary>
        public void Stand()
        {
            if (!CanStand)
            {
                throw new InvalidCardActionException("Cannot stand the player's hand, conditions not met");
            }
            this.FirstHand.Stand();
        }



        /// <summary>
        /// Gets if the player can double their hand
        /// </summary>
        public bool CanDoubleSplitHand
        {
            get
            {
                return this.SplitHand.CardCount == 2 && this.SplitHandValue >= 9 && this.SplitHandValue <= 11;
            }
        }

        /// <summary>
        /// Gets if the player can add more cards to their split hand
        /// </summary>
        public bool CanHitSplitHand
        {
            get
            {
                return this.SplitHandValue < PlayerHand.MAX_HAND_VALUE;
            }
        }

        /// <summary>
        /// Gets if the player can stand on their split hand
        /// </summary>
        public bool CanStandSplitHand
        {
            get
            {
                return this.SplitHandValue < PlayerHand.MAX_HAND_VALUE;
            }
        }


        /// <summary>
        /// Double the player's split hand only
        /// </summary>
        /// <param name="card">Card to add to the doubled hand</param>
        public void DoubleSplitHand(Card card)
        {
            if (!CanDoubleSplitHand)
            {
                throw new InvalidCardActionException("Cannot double the player's hand, conditions not met");
            }
            this.SplitHand.DoubleHandCard(card);
        }



        /// <summary>
        /// Stand on the player's current hand
        /// </summary>
        public void StandSplitHand()
        {
            if (!CanStandSplitHand)
            {
                throw new InvalidCardActionException("Cannot stand the player's split hand, conditions not met");
            }
            this.SplitHand.Stand();
        }



        /// <summary>
        /// Splits the players hand into two, throws an exception if the hand does not meet split conditions
        /// </summary>
        public void SplitHandCards()
        {
            if (this._playerHandType == PlayerHandType.SINGLE_HAND && this.FirstHand.HasTwoPairs)
            {
                this._playerHandType = PlayerHandType.SPLIT_HAND;
                AddSplitHandCard(this._playerHand[0].SplitHandCard());
                return;
            }
            throw new InvalidHandSplitException();
        }

        /// <summary>
        /// Adds a card to the player's first hand
        /// </summary>
        /// <param name="card">Card to add to the player's hand</param>
        public void AddFirstHandCard(Card card)
        {
            this._playerHand[0].AddCard(card);
        }

        /// <summary>
        ///  Adds cards to the player's first hand
        /// </summary>
        /// <param name="cards">Card to add to the player's hand</param>
        public void AddFirstHandCards(Card[] cards)
        {
            foreach (Card card in cards)
            {
                AddFirstHandCard(card);
            }
        }


        /// <summary>
        ///  Adds a card to the players split hand, provided the player has a split hand, throws an exception if the player's hand is not split
        /// </summary>
        /// <param name="card">Card to add to the player's split hand</param>
        public void AddSplitHandCard(Card card)
        {
            if (this._playerHandType != PlayerHandType.SPLIT_HAND)
            {
                throw new PlayerHandNotSplitException();
            }

            this._playerHand.Add(new PlayerHand());
            this._playerHand[1].AddCard(card);
        }

        /// <summary>
        /// Adds cards to the players split hand, provided the player has a split hand
        /// </summary>
        /// <param name="cards">Cards to add to the player's split hand</param>
        public void AddSplitHandCards(Card[] cards)
        {
            foreach (Card card in cards)
            {
                AddSplitHandCard(card);
            }
        }

        /// <summary>
        /// Get the value of the player's first hand
        /// </summary>
        public int FirstHandValue
        {
            get
            {
                return this._playerHand[0].HandValue;
            }
        }

        /// <summary>
        /// Gets the player's first hand result
        /// </summary>
        public PlayerHandResult FirstHandResult
        {
            get
            {
                return this._playerHand[0].HandResult;
            }
        }

        /// <summary>
        /// Get the value of the player's split hand, throws an exception if the player does not have a split hand 
        /// </summary>
        public int SplitHandValue
        {
            get
            {
                if (this._playerHandType != PlayerHandType.SPLIT_HAND)
                {
                    throw new PlayerHandNotSplitException();
                }
                return this._playerHand[1].HandValue;
            }
        }

        /// <summary>
        /// Gets the player's split hand result
        /// </summary>
        public PlayerHandResult SplitHandResult
        {
            get
            {
                if (this._playerHandType != PlayerHandType.SPLIT_HAND)
                {
                    throw new PlayerHandNotSplitException();
                }
                return this._playerHand[1].HandResult;
            }
        }

        /// <summary>
        /// Sets the players hand result based on the dealers hand
        /// </summary>
        /// <param name="dealer">The dealers object</param>
        public void SetHandResult(Player dealer)
        {

            if (this.CurrentHandType == PlayerHandType.SINGLE_HAND)
            {
                this.FirstHand.SetPlayerHandResult(dealer.FirstHand, true);
            }
            else
            {
                this.FirstHand.SetPlayerHandResult(dealer.FirstHand);
                this.SplitHand.SetPlayerHandResult(dealer.FirstHand);
            }

        }

        /// <summary>
        /// Clear the player's active hand cards
        /// </summary>
        public void ClearHands()
        {

            foreach (PlayerHand hand in this._playerHand)
            {
                hand.ClearCards();
            }

            _playerHand.Clear();

            // Add a new player hand
            _playerHand.Add(new PlayerHand());

            this._playerHandType = PlayerHandType.SINGLE_HAND;
            this._playerIsuranceCorrect = false;
            this._playerInsured = false;
        }

        /// <summary>
        /// Returns the player object as a string
        /// </summary>
        /// <returns>retuns the player as a string</returns>
        public override string ToString()
        {
            string result;

            result = "Player: " + PlayerName;

            if (this.PlayerInsured)
            {
                result += "\n\n-- Player placed an insurance bet and ".ToUpper() + (this.PlayerIsuranceCorrect ? "WON" : "LOST") + "";
            }

            result += "\n\n-- Hand Cards: \n";
            result += FirstHand.ToString();


            if (this._playerHandType == PlayerHandType.SPLIT_HAND)
            {
                result += "\n-- Split Hand(s) Cards: \n";

                for (int x = 1; x < this._playerHand.Count; x++)
                {
                    PlayerHand hand = this._playerHand[x];
                    result += "** \n";
                    result += hand.ToString();
                    result += "\n";
                }
              
            }
            result += "------------------------------\n";

            return result;
        }

    }
}
