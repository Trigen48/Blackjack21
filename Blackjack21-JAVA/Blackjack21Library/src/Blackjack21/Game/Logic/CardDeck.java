package Blackjack21.Game.Logic;

import Blackjack21.Game.Exceptions.DeckEmptyException;
import Blackjack21.Game.Helper.ShuffleList;
import Blackjack21.Game.Model.Card;

/**
 * A card deck object, used to store and manage a deck which will be used in a
 * game
 */
public class CardDeck
{

    int CARDMAX = 13;
    int SYMBOLMAX = 4;

    // Store our cards in a queue
    private final ShuffleList<Card> _deckCards;
    // we will use this to reinitiilize the deck if it need to be refreshed
    private final ShuffleList<Card> _deckCopy;

    /**
     * Create a new instance of the deck object
     */
    public CardDeck()
    {
        this._deckCards = new ShuffleList<>();
        this._deckCopy = new ShuffleList<>();
    }

    /**
     * add cards to the copy deck
     *
     * @param card
     */
    private void addDeckCard(Card card)
    {
        this._deckCopy.add(card);
    }

    /**
     * Clear and initialize a new deck
     *
     * @param deckCount Number of card sets to use in this deck, 52 cards
     * multiply by the number of decks
     */
    public void newDeck(int deckCount)
    {

        clean();
        for (int deckIndex = 0; deckIndex < deckCount; deckIndex++)
        {
            for (int symbolCount = 1; symbolCount <= SYMBOLMAX; symbolCount++)
            {
                for (int typeCount = 1; typeCount <= CARDMAX; typeCount++)
                {
                    addDeckCard(new Card(typeCount, symbolCount));
                }
            }
        }
    }

    /**
     * Add a collection of cards to the deck
     *
     * @param cards Input cards that will be added to the deck
     * @throws Blackjack21.Game.Exceptions.DeckEmptyException
     */
    public void newDeckFromArray(Card[] cards) throws DeckEmptyException
    {
        if (cards == null || cards.length == 0)
        {
            throw new DeckEmptyException("Cards added to the deck cannot be null or empty");
        }

        clean();
        for (Object obj : cards)
        {
            Card card = (Card) obj;
            addDeckCard(card);
        }
    }

    /**
     * Returns the number of cards in the current deck
     *
     * @return
     */
    public int getCardCount()
    {
        return this._deckCards.size();
    }

    /**
     * Shuffle the deck
     */
    public void shuffleDeck()
    {
        clearDeck();
        this._deckCopy.shuffleShift();
        // Add shuffled cards to the play deck
        this._deckCards.addAll(this._deckCopy);
    }

    /**
     * Remove and return a card from the top of a deck, throws an exception if
     * the deck is empty.
     *
     * @return Return the top card from the deck
     * @throws Blackjack21.Game.Exceptions.DeckEmptyException
     */
    public Card hitCard() throws DeckEmptyException
    {
        if (_deckCards.isEmpty() == true)
        {
            throw new DeckEmptyException();
        }

        return this._deckCards.pop();
    }

    /**
     * Clear the playing deck cards
     */
    public void clearDeck()
    {
        this._deckCards.clear();
    }

    /**
     * Remove all cards from the deck
     */
    public void clean()
    {
        this._deckCards.clear();
        this._deckCopy.clear();
    }

}
