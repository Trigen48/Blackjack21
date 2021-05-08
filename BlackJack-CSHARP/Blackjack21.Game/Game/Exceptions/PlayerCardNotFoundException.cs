using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack21.Game.Exceptions
{
    /// <summary>
    /// Throw an exception if the player's card is not found
    /// </summary>
    public class PlayerCardNotFoundException : Exception
    {

        public PlayerCardNotFoundException(string message="Player card not found, invalid index")  : base (message)
        {

        }
    }
}
