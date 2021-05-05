package Blackjack21.Game.Exceptions;

/**
 * Throws an exception when the player's name is empty
 */
public class PlayerNameEmptyException extends Exception
{

    public PlayerNameEmptyException()
    {
        super("Player name cannot be empty");
    }

    public PlayerNameEmptyException(String message)
    {
        super(message);
    }

}
