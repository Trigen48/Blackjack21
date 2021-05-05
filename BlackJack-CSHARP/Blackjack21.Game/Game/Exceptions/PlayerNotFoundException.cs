using System;

namespace Blackjack21.Game.Exceptions
{
    /// <summary>
    /// Throws a player not found exception based on an invalid index
    /// </summary>
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException(int index) : base(string.Format("Index {0} is not a valid player entry", index))
        {

        }

        public PlayerNotFoundException(string playerName) : base(string.Format("Player {0} not found", playerName))
        {

        }


    }
}
