using System;

namespace Blackjack21.Game.Exceptions
{
    /// <summary>
    /// Throws an exception when the player's name is empty
    /// </summary>
    public class PlayerNameEmptyException : Exception
    {

        public PlayerNameEmptyException(string message="Player name cannot be empty"): base(message)
        {

        }

    }
}
