package Blackjack21.Game.Exceptions;

/**
 * Throws a deck not found exception
 */
public class DeckEmptyException extends Exception
{

    public DeckEmptyException()
    {
        super("There are no cards in the deck");
    }

    public DeckEmptyException(String message)
    {
        super(message);
    }

}
