package Blackjack21.Game.Exceptions;

/**
 * Throws an exception if the hand cannot be split
 */
public class InvalidHandSplitException extends Exception
{

    public InvalidHandSplitException()
    {
        super("Invalid hand split, conditions to split the hand have not been met");
    }

    public InvalidHandSplitException(String message)
    {
        super(message);
    }

}
