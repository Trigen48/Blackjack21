using System;

namespace Blackjack21.Game.Exceptions
{
    /// <summary>
    /// Throws a deck not found exception
    /// </summary>
    public class DeckEmptyException : Exception
    {

        public DeckEmptyException(string message = "There are no cards in the deck") : base(message)
        {

        }
    }
}
