package Blackjack21.Game.Exceptions;

/**
 * Throws a player does not exist exception
 */
public class PlayerExistsException extends Exception
{

    public PlayerExistsException(String playerName)
    {
        super("Player " + playerName + " already exists");
    }

}
