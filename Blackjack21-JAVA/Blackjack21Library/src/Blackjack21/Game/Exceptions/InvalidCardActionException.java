package Blackjack21.Game.Exceptions;

/**
 * Throws an exception if the player performs an invalid hand action
 */
public class InvalidCardActionException extends Exception
{

    public InvalidCardActionException(String message)
    {
        super(message);
    }

}
