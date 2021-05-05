using System;

namespace Blackjack21.Game.Exceptions
{
    /// <summary>
    /// Throws a player does not exist exception
    /// </summary>
    public class PlayerExistsException : Exception
    {

        public PlayerExistsException(string playerName) : base(string.Format("Player {0} already exists", playerName))
        {

        }
    }
}
