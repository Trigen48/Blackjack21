using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack21.Game;
using Blackjack21.Game.Model;

namespace Blackjack21.ConsoleTestFirstDraw
{
    class Program
    {
        private static Game.BlackjackGame game;
        public static void InitTest()
        {
            // Define Test Players
            const string PLAYER1 = "Lemmy";
            const string PLAYER2 = "Andrew";
            const string PLAYER3 = "Billy";
            const string PLAYER4 = "Carla";

            game = new BlackjackGame();


            // Add new players
            game.AddPlayer(PLAYER1);
            game.AddPlayer(PLAYER2);
            game.AddPlayer(PLAYER3);
            game.AddPlayer(PLAYER4);

            
            // initialize a new deck of cards
            game.InitializeDeck();


            // Start a new Round
            game.NewGame();
            

            game.DealCards();

            // Ends the current round of the game
            game.ConcludeRound();


        }


        static void Main()
        {

            Console.WriteLine("Blackjack 21 Test First Round of random cards");
            Console.WriteLine("-----------------------------");

            try
            {
                InitTest();s
                Console.WriteLine("");
                Console.WriteLine(game.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
