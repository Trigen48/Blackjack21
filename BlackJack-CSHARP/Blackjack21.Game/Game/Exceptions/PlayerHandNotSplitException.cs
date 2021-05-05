using System;

namespace Blackjack21.Game.Exceptions
{
    /// <summary>
    /// Throws an exception if the player's hand is not split
    /// </summary>
    public class PlayerHandNotSplitException : Exception
    {

        public PlayerHandNotSplitException(string message = "The player's hand is not split") : base(message)
        {

        }
    }
}
