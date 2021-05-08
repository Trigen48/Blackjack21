using System;
using System.Collections.Generic;
using Blackjack21.Game.Model;
using Blackjack21.Game.Exceptions;

namespace Blackjack21.Game.Logic
{
    /// <summary>
    /// An object that stores the players hand cards
    /// </summary>
    public class PlayerHand
    {
        private readonly List<Card> _cards;
        private int _handValue;
        private PlayerHandResult _playerHandResult;
        private PlayerHandAction _playerHandAction;
        public const int MAX_HAND_VALUE = 21;

        // Keep track of the number of ace cards in the players hand
        private int aceCount = 0;

        /// <summary>
        /// Initialize the player's hand
        /// </summary>
        public PlayerHand()
        {
            this._cards = new List<Card>();
            this._handValue = 0;
            this.aceCount = 0;
            this._playerHandResult = PlayerHandResult.NONE;
            this._playerHandAction = PlayerHandAction.NONE;
        }

        /// <summary>
        /// Add new card to the player's hand
        /// </summary>
        /// <param name="cardType">Value of the card type</param>
        /// <param name="cardSymbol">Value of the card symbol</param>
        public void AddCard(CardType cardType, CardSymbol cardSymbol)
        {
            AddCard(new Card(cardType, cardSymbol));
        }

        /// <summary>
        /// Add new card to the player's hand
        /// </summary>
        /// <param name="card">Card Object</param>
        public void AddCard(Card card)
        {
            this._cards.Add(card);

            if (card.CardType == CardType.ACE)
            {
                this.aceCount++;
            }

            this._handValue += card.CardValue;
        }


        /// <summary>
        /// Returns the player's card based on the provided card index, throws an exception if the player's card is not found
        /// </summary>
        /// <param name="cardIndex">The player's card index</param>
        /// <returns>Returns the players card</returns>
        public Card this[int cardIndex]
        {
            get
            {
                if (cardIndex >= 0 && this._cards.Count > cardIndex)
                {
                    return this._cards[cardIndex];
                }

                throw new PlayerCardNotFoundException();
            }
        }

       

        /// <summary>
        /// Returns the players hand result
        /// </summary>
        public PlayerHandResult HandResult
        {
            get
            {
                return this._playerHandResult;
            }

        }

        /// <summary>
        /// Gets if the hand has two pairs of the same card
        /// </summary>
        public bool HasTwoPairs
        {
            get
            {
                return this.CardCount == 2 && this.Cards[0].CompareCardType(this.Cards[1]);
            }
        }

        /// <summary>
        /// Return the player's current hand cards
        /// </summary>
        public List<Card> Cards
        {
            get
            {
                return this._cards;
            }
        }

        /// <summary>
        /// Clear the player's hand cards
        /// </summary>
        public void ClearCards()
        {
            this._cards.Clear();
            this.aceCount = 0;
            this._handValue = 0;
            this._playerHandResult = PlayerHandResult.NONE;
            this._playerHandAction = PlayerHandAction.NONE;
        }


        /// <summary>
        /// Doubles the players hand
        /// </summary>
        /// <param name="card">Card to add to the doubled hand</param>
        public void DoubleHandCard(Card card)
        {
            this._playerHandAction = PlayerHandAction.DOUBLE;
            AddCard(card);
        }

        /// <summary>
        /// Fold the player's hand
        /// </summary>
        public void Fold()
        {
            this._playerHandAction = PlayerHandAction.FOLD;
        }

        /// <summary>
        /// Stand on the player's current hand
        /// </summary>
        public void Stand()
        {
            this._playerHandAction = PlayerHandAction.STAND;
        }

        /// <summary>
        /// Remove the second card from the hand and return it
        /// </summary>
        /// <returns>Returns the second card from the hand</returns>
        public Card SplitHandCard()
        {
            Card card = _cards[1];
            this._cards.RemoveAt(1);

            // reduce the ace card count if the split card is an ace
            if (card.CardType == CardType.ACE)
            {
                this.aceCount--;
            }

            this._handValue -= card.CardValue;

            return card;
        }

        /// <summary>
        /// Get the player's total amount of cards
        /// </summary>
        public int CardCount
        {
            get
            {
                return this._cards.Count;
            }
        }

        /// <summary>
        /// Checks if the hand is an automatic blackjack
        /// </summary>
        public bool IsNaturalBlackjack
        {
            get
            {
                return this.HandValue == MAX_HAND_VALUE && this.CardCount == 2;
            }
        }

        /// <summary>
        /// Returns the players hand action
        /// </summary>
        public PlayerHandAction HandAction
        {
            get
            {
                return this._playerHandAction;
            }
        }

        /// <summary>
        /// Sets the dealers hand card result
        /// </summary>
        public void SetDealerResult()
        {
            if (this.IsNaturalBlackjack)
            {
                this._playerHandResult = PlayerHandResult.PLAYER_WON_BLACKJACK;
            }
            else if (this.HandValue > MAX_HAND_VALUE)
            {
                this._playerHandResult = PlayerHandResult.PLAYER_BUST_OVER_21;
            }

        }

        /// <summary>
        /// Sets the hands result based on the dealers hand
        /// </summary>
        /// <param name="dealerHand">The dealer's hand to comapre to the current hand</param>
        /// <param name="IsSingleHand">Set if the the hand is from a split</param>
        public void SetPlayerHandResult(PlayerHand dealerHand, bool IsSingleHand = false)
        {
            
            if (IsSingleHand && this.IsNaturalBlackjack)
            {
                if (this.IsNaturalBlackjack == dealerHand.IsNaturalBlackjack)
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_HAND_PUSH_BLACKJACK;
                }
                else
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_WON_BLACKJACK;
                }

            }
            else
            {
                if (dealerHand.IsNaturalBlackjack)
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_HAND_LOWER_BLACKJACK;
                }
                else if(this._playerHandAction == PlayerHandAction.FOLD)
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_HAND_FOLD;
                }
                else if (this.HandValue > MAX_HAND_VALUE)
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_BUST_OVER_21;
                }
                else if (dealerHand.HandValue > this.HandValue && dealerHand.HandValue <= MAX_HAND_VALUE)
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_HAND_LOWER;
                }
                else if (dealerHand.HandValue == this.HandValue)
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_HAND_PUSH;
                }
                else if (this.CardCount == 5) // this rule is not commonly used, mostly used in home settings
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_WON_5_CARD_CHARLIE;
                }

                else
                {
                    this._playerHandResult = PlayerHandResult.PLAYER_WON_HIGHER;
                }
            }

        }

        /// <summary>
        /// Return sum of the players's hand cards
        /// </summary>
        public int HandValue
        {
            get
            {

                int handValue = this._handValue;
                int softHandValue = (handValue + 10);

                // check if there are any aces in the players hand, return the hand value if 
                // there are no ace cards 
                // or if the hand value >= 21 with the aces counting as 1 each
                // or if the soft hand value would exceed 21 if we added 1 ace value of 10
                // else add 10 to our hand value since only once ace is need, otherwise 2 aces = 22
                if (aceCount == 0 || handValue >= MAX_HAND_VALUE || softHandValue > MAX_HAND_VALUE)
                {
                    return handValue;
                }

                return softHandValue;
            }

        }

        /// <summary>
        /// Returns the player'ss hand as a string
        /// </summary>
        /// <returns>Returns a string value</returns>
        public override string ToString()
        {
            String result = "";

            foreach (Card card in _cards)
            {
                result += "-- [ " + card.ToString() + " ]\n";
            }

            result += "\n-- Hand Action: " + this.HandAction.ToString() + "\n";
            result += "\n-- Hand Value: " + this.HandValue.ToString() + "\n";
            result += "-- Hand Result: " + this._playerHandResult.ToString() + "\n";


            return result;
        }

        
    }
}
