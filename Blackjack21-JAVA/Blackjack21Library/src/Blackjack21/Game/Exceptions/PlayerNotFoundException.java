package Blackjack21.Game.Exceptions;

/**
 * Throws a player not found exception based on an invalid index
 */
public class PlayerNotFoundException extends Exception
{

    public PlayerNotFoundException(int index)
    {
        super("Index " + index + " is not a valid player entry");
    }

    public PlayerNotFoundException(String playerName)
    {
        super("Player " + playerName + " not found");
    }

}
