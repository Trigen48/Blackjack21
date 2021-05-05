using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack21.Game;
using Blackjack21.Game.Model;

namespace Blackjack21.ConsoleTest
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

            // init new game
            //game.NewGame();

            // Dealer Cards
            game.AddDealerCards(new Card[] { new Card(CardType.JACK, CardSymbol.SPADES), new Card(CardType.NINE, CardSymbol.HEARTS) });


            // Add Player 1 Cards : Lemmy
            game.AddPlayerCards(PLAYER1, new Card[] { new Card(CardType.ACE, CardSymbol.SPADES),
                new Card(CardType.SEVEN, CardSymbol.HEARTS),
                new Card(CardType.ACE, CardSymbol.DIAMONDS) });

            // Add Player 2 Cards : Andrew
            game.AddPlayerCards(PLAYER2, new Card[] { new Card(CardType.KING, CardSymbol.DIAMONDS),
             new Card(CardType.FOUR, CardSymbol.SPADES),
             new Card(CardType.FOUR, CardSymbol.CLUBS)});

            // Add Player 3 Cards : Billy
            game.AddPlayerCards(PLAYER3, new Card[] { new Card(CardType.TWO, CardSymbol.SPADES),
             new Card(CardType.TWO, CardSymbol.DIAMONDS),
             new Card(CardType.TWO, CardSymbol.HEARTS),
             new Card(CardType.FOUR, CardSymbol.DIAMONDS),
             new Card(CardType.FIVE, CardSymbol.CLUBS)});

            // Add Player 4 Cards Carla
            game.AddPlayerCards(PLAYER4, new Card[] { new Card(CardType.QUEEN, CardSymbol.CLUBS),
             new Card(CardType.SIX, CardSymbol.SPADES),
             new Card(CardType.NINE, CardSymbol.DIAMONDS)});

            // Ends the current round of the game
            game.ConcludeRound();


        }


        public static void TestResults()
        {

        }
        static void Main()
        {

            Console.WriteLine("Blackjack 21 Test case");
            Console.WriteLine("-----------------------------");

            try
            {
                InitTest();
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
