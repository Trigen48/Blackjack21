using Blackjack21.Game.Exceptions;
using Blackjack21.Game.Model;
using Blackjack21.Game.Helper;
namespace Blackjack21.Game.Logic
{
    /// <summary>
    /// A card deck object, used to store and manage a deck which will be used in a game
    /// </summary>
    public class CardDeck
    {
        // Store our cards in a queue
        private readonly ShuffleList<Card> _deckCards;

        // we will use this to reinitiilize the deck if it need to be refreshed
        private readonly ShuffleList<Card> _deckCopy;


        /// <summary>
        /// Create a new instance of the deck object
        /// </summary>
        public CardDeck()
        {
            this._deckCards = new ShuffleList<Card>();
            this._deckCopy = new ShuffleList<Card>();
        }

        /// <summary>
        /// add cards to the copy deck
        /// </summary>
        /// <param name="card"></param>
        private void AddDeckCard(Card card)
        {
            this._deckCopy.Add(card);
        }

        /// <summary>
        /// Clear and initialize a new deck
        /// </summary>
        /// <param name="deckCount">Number of card sets to use in this deck, 52 cards multiply by the number of decks</param>
        public void NewDeck(int deckCount = 1)
        {
            const int CARDMAX = 13;
            const int SYMBOLMAX = 4;

            Clean();
            for (int deckIndex = 0; deckIndex < deckCount; deckIndex++)
            {
                for (int symbolCount = 1; symbolCount <= SYMBOLMAX; symbolCount++)
                {
                    for (int typeCount = 1; typeCount <= CARDMAX; typeCount++)
                    {
                        AddDeckCard(new Card(typeCount, symbolCount));
                    }
                }

            }

        }

        /// <summary>
        /// Add a collection of cards to the deck
        /// </summary>
        /// <param name="cards">Input cards that will be added to the deck</param>
        public void NewDeckFromArray(Card[] cards)
        {

            if (cards == null || cards.Length == 0)
            {
                throw new DeckEmptyException("Cards added to the deck cannot be null or empty");
            }


            Clean();
            foreach (Card card in cards)
            {
                AddDeckCard(card);
            }
        }

        /// <summary>
        /// Returns the number of cards in the current deck
        /// </summary>
        public int CardCount
        {
            get
            {
                return this._deckCards.Count;
            }
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        public void ShuffleDeck()
        {
            ClearDeck();
            this._deckCopy.ShuffleShift();

            // Add shuffled cards to the play deck
            this._deckCards.AddRange(this._deckCopy);
        }

        /// <summary>
        /// Get the playing card deck as an array
        /// </summary>
        /// <returns>Returns the card deck as an array</returns>
        public Card[] DeckToArray()
        {
            return this._deckCards.ToArray();
        }

        /// <summary>
        /// Remove and return a card from the top of a deck, throws an exception if the deck is empty.
        /// </summary>
        /// <returns>Return the top card from the deck</returns>
        public Card HitCard()
        {
            if (_deckCards.Count == 0)
            {
                throw new DeckEmptyException();
            }

            return this._deckCards.Pop();
        }

        /// <summary>
        /// Clear the playing deck cards
        /// </summary>
        public void ClearDeck()
        {
            this._deckCards.Clear();
        }

        /// <summary>
        /// Remove all cards from the deck
        /// </summary>
        public void Clean()
        {
            this._deckCards.Clear();
            this._deckCopy.Clear();
        }


    }
}
