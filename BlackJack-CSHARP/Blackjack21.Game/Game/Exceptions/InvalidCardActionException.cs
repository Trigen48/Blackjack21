using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack21.Game.Exceptions
{
    /// <summary>
    /// Throws an exception if the player performs an invalid hand action
    /// </summary>
    public class InvalidCardActionException : Exception
    {
        public InvalidCardActionException(string message): base(message)
        {

        }
    }
}
