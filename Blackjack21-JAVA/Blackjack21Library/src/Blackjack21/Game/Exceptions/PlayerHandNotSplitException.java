package Blackjack21.Game.Exceptions;

/**
 * Throws an exception if the player's hand is not split
 */
public class PlayerHandNotSplitException extends Exception
{

    public PlayerHandNotSplitException()
    {
        super("The player's hand is not split");
    }

    public PlayerHandNotSplitException(String message)
    {
        super(message);
    }

}
