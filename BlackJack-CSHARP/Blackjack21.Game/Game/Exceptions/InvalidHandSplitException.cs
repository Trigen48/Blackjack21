using System;

namespace Blackjack21.Game.Exceptions
{

    /// <summary>
    /// Throws an exception if the hand cannot be split
    /// </summary>
    public class InvalidHandSplitException : Exception
    {

        public InvalidHandSplitException(string message = "Invalid hand split, conditions to split the hand have not been met") : base(message)
        {

        }
    }
}
