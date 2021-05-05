using System;
using Blackjack21.Game;
using Blackjack21.Game.Logic;
using Blackjack21.Game.Exceptions;
namespace Blackjack21.ConsoleGame
{
    class Program
    {

        static BlackjackGame game;
        static int playerCount = 0;

        static void InitGame()
        {
            game = new BlackjackGame();
            game.InitializeDeck(1);
        }

        #region Text Output

        static void WriteHeadingLine(string header)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-- {0}", header.ToUpper());
            Console.WriteLine("--");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void WriteMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Select a game action: ");
            Console.WriteLine("  1 - New Round");
            Console.WriteLine("  2 - View Players");
            Console.WriteLine("  3 - Change Deck Count");
            Console.WriteLine("  E - Exit Game");
            Console.WriteLine();
            Console.Write("Press key for game action: ");
            Console.ForegroundColor = ConsoleColor.White;

        }

        static void ShowFirstTableCards()
        {

            Console.WriteLine("Dealer Cards: ");
            Console.WriteLine("-- [{0}] [? ? ?]", game.Dealer.FirstHand.Cards[0].ToString());

            Console.WriteLine();
            for (int x = 0; x < playerCount; x++)
            {
                Player player = game[x];


                ShowPlayerCards(player);

                if (player.CurrentHandType == Game.Model.PlayerHandType.SPLIT_HAND)
                {
                    ShowPlayerSplitCards(player);
                }

            }

        }

        static void ShowPlayerCards(Player player)
        {
            Console.WriteLine("{0} First Hand Cards: ", player.PlayerName);


            Console.Write("-- ");
            foreach (var card in player.FirstHand.Cards)
            {
                Console.Write("[{0}] ", card.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("-- Hand Value: {0}", player.FirstHandValue);
            Console.WriteLine();

        }

        static void ShowPlayerSplitCards(Player player)
        {
            Console.WriteLine("{0} Split Hand Cards: ", player.PlayerName);


            Console.Write("-- ");
            foreach (var card in player.SplitHand.Cards)
            {
                Console.Write("[{0}] ", card.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("-- Hand Value: {0}", player.FirstHandValue);
            Console.WriteLine();

        }

        static void ShowFinalTable()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("++ Dealer");
            Console.WriteLine("++");
            Console.WriteLine(game.Dealer.ToString());


            Console.WriteLine();
            Console.WriteLine("++ Players");
            Console.WriteLine("++");
            for (int x = 0; x < playerCount; x++)
            {
                Console.WriteLine(game[x].ToString());
            }

            Console.ForegroundColor = ConsoleColor.White;

        }

        static void ShowPlayers()
        {
            Console.Clear();

            WriteHeadingLine("Showing current table players");
            for (int x = 0; x < playerCount; x++)
            {
                Console.WriteLine("-- Player {0} - {1} ", x + 1, game[x].PlayerName);
            }
        }

        #endregion

        static void Main()
        {
            string playerCountString;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Console Blackjack Game");
            Console.WriteLine("");


            #region Setup Player details and initialize game
            WriteHeadingLine("Player setup");

            while (playerCount <= 0 || playerCount > 7)
            {

                Console.Write("Enter the number of players (1 - 7): ");
                playerCountString = Console.ReadLine();


                try
                {
                    playerCount = int.Parse(playerCountString);
                }
                catch
                {
                    WriteError("Invalid value entered, enter a value between 1 and 7.");

                }
                Console.WriteLine("");
            }

            // initialize the game object
            InitGame();

            WriteHeadingLine("Setup player names");

            for (int x = 1; x <= playerCount; x++)
            {

                string playerName = "";

                while (playerName == "")
                {

                    Console.Write("Enter player {0}'s name: ", x);

                    playerName = Console.ReadLine().Trim().ToUpper();

                    try
                    {
                        game.AddPlayer(playerName);
                    }
                    catch (PlayerExistsException ex)
                    {
                        WriteError(ex.Message);
                        playerName = "";
                    }
                    Console.WriteLine("");
                }

            }

            #endregion

            #region Game Loop
            bool loopMenu = true;
            while (loopMenu)
            {
                Console.Clear();

                WriteHeadingLine("Blackjack game");
                WriteMainMenu();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:

                        NewGame();
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        ShowPlayers();
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        ChangeDeckCount();
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;




                    case ConsoleKey.E:
                        loopMenu = false;
                        break;
                }


            }

            #endregion


        }

        static void NewGame()
        {
            Console.Clear();
            WriteHeadingLine("New blackjack round: ");
            game.NewGame();

            // Deal 2 Cards to each player and the dealer
            game.DealCards();

            // Show each players cards
            ShowFirstTableCards();

            int dealerValue = game.Dealer.FirstHandValue;

            if (game.Dealer.FirstHand.Cards[0].CardType == Game.Model.CardType.ACE)
            {

                WriteHeadingLine("Dealer has a face up ace card. Insure ?");

                for (int x = 0; x < playerCount; x++)
                {
                    Player player = game[x];

                    bool exitInsure = false;

                    while (!exitInsure)
                    {
                        Console.Write("{0} wants to insure? (1 - yes, 2 - no)  ", player.PlayerName);

                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.D1:
                            case ConsoleKey.NumPad1:
                                player.SetIsurance(dealerValue);
                                exitInsure = true;
                                break;

                            case ConsoleKey.D2:
                            case ConsoleKey.NumPad2:
                                exitInsure = true;
                                break;

                        }
                        Console.WriteLine("");
                        Console.WriteLine("");
                    }

                }

            }


            if (dealerValue != 21)
            {
                for (int x = 0; x < playerCount; x++)
                {
                    Player player = game[x];
                    PlayerOptions(ref player);
                }

                game.DealerHit();// Server the dealer cards until he reaches a soft 17 value
            }

            FinalizeGame();
            return;

        }

        static void PlayerOptions(ref Player player)
        {

            if (player.FirstHand.IsBlackjack)
            {
                return;
            }


            WriteHeadingLine("Player " + player.PlayerName + "'s Turn");


            if (player.CanSplitHand)
            {
                bool exitSplit = false;

                while (!exitSplit)
                {
                    Console.Write("{0} wants to split the hand? (1 = yes, 2 = no):  ", player.PlayerName);

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:

                            try
                            {
                                player.SplitHandCards();

                                // Add two new cards to the split hand and first hand
                                player.AddFirstHandCard(game.HitCard());
                                player.AddSplitHandCard(game.HitCard());

                                Console.WriteLine("");
                                Console.WriteLine("Player has split their hand");
                                exitSplit = true;
                            }
                            catch (InvalidHandSplitException ex)
                            {
                                WriteError(ex.Message);
                            }

                            break;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            exitSplit = true;
                            break;

                    }
                    Console.WriteLine("");
                    Console.WriteLine("");
                }



            }

            SingleHandMenu(ref player);

            if (player.CurrentHandType == Game.Model.PlayerHandType.SPLIT_HAND)
            {
                SplitHandMenu(ref player);

            }

        }

        static void SingleHandMenu(ref Player player)
        {
            Console.Clear();


            WriteHeadingLine("Player " + player.PlayerName + "'s turn Hand 1");
            bool exitCode = false;

            while (!exitCode && player.FirstHandValue < PlayerHand.MAX_HAND_VALUE)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (player.CanFold) { Console.WriteLine("  1 - Fold Hand"); }
                if (player.CanHit) { Console.WriteLine("  2 - Hit Hand"); }
                if (player.CanStand) { Console.WriteLine("  3 - Stand"); }
                if (player.CanDouble) { Console.WriteLine("  4 - Double"); }
                Console.WriteLine("  5 - Show Current Table");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                ShowPlayerCards(player);
                Console.WriteLine();
                Console.Write("Select a player option: ");


                switch (Console.ReadKey().Key)
                {

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        try
                        {
                            if (!player.CanFold)
                            {
                                throw new InvalidCardAction("Player cannot fold this hand");

                            }

                            player.Fold();
                            Console.WriteLine("");
                            Console.WriteLine("Player has folded their hand");
                            exitCode = true;
                        }
                        catch (InvalidCardAction ex)
                        {
                            WriteError(ex.Message);
                        }

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        try
                        {
                            if (!player.CanHit)
                            {
                                throw new InvalidCardAction("Player cannot hit this hand");

                            }

                            player.AddFirstHandCard(game.HitCard());
                            Console.WriteLine("");
                            Console.WriteLine("Player has added a card to their hand");

                            Console.WriteLine();
                            ShowPlayerCards(player);

                            if (player.FirstHandValue > PlayerHand.MAX_HAND_VALUE)
                            {
                                Console.WriteLine("Player is over {0}, Player Bust", PlayerHand.MAX_HAND_VALUE);
                                exitCode = true;
                            }

                        }
                        catch (InvalidCardAction ex)
                        {
                            WriteError(ex.Message);
                        }

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        try
                        {
                            if (!player.CanStand)
                            {
                                throw new InvalidCardAction("Player cannot stand on this hand");

                            }

                            player.Stand();
                            Console.WriteLine("");
                            Console.WriteLine("Player stands");
                            exitCode = true;
                        }
                        catch (InvalidCardAction ex)
                        {
                            WriteError(ex.Message);
                        }
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        try
                        {
                            if (!player.CanDouble)
                            {
                                throw new InvalidCardAction("Player cannot double this hand");
                            }

                            player.DoubleHand(game.HitCard());
                            Console.WriteLine("");
                            Console.WriteLine("Player has doubled their hand");
                            Console.WriteLine();
                            ShowPlayerCards(player);
                            if (player.FirstHandValue > PlayerHand.MAX_HAND_VALUE)
                            {
                                Console.WriteLine("Player is over {0}, Player Bust", PlayerHand.MAX_HAND_VALUE);
                            }

                            exitCode = true;
                        }
                        catch (InvalidCardAction ex)
                        {
                            WriteError(ex.Message);
                        }
                        exitCode = true;
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        ShowFirstTableCards();
                        break;

                }


                Console.WriteLine();
                Console.WriteLine();
            }

        }

        static void SplitHandMenu(ref Player player)
        {
            Console.Clear();


            WriteHeadingLine("Player " + player.PlayerName + "'s turn Hand 2");
            bool exitCode = false;

            while (!exitCode && player.SplitHandValue < PlayerHand.MAX_HAND_VALUE)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                if (player.CanHitSplitHand) { Console.WriteLine("  1 - Hit Hand"); }
                if (player.CanStandSplitHand) { Console.WriteLine("  2 - Stand"); }
                if (player.CanDoubleSplitHand) { Console.WriteLine("  3 - Double"); }
                Console.WriteLine("  4 - Show Current Table");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                ShowPlayerSplitCards(player);
                Console.WriteLine();
                Console.Write("Select a player option: ");


                switch (Console.ReadKey().Key)
                {


                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        try
                        {
                            if (!player.CanHitSplitHand)
                            {
                                throw new InvalidCardAction("Player cannot hit split hand");

                            }

                            player.AddSplitHandCard(game.HitCard());
                            Console.WriteLine("");
                            Console.WriteLine("Player has added a card to their hand");
                            Console.WriteLine();
                            ShowPlayerSplitCards(player);
                            if (player.FirstHandValue > PlayerHand.MAX_HAND_VALUE)
                            {
                                Console.WriteLine("Player is over {0}, Player split hand Bust", PlayerHand.MAX_HAND_VALUE);
                                exitCode = true;
                            }

                        }
                        catch (InvalidCardAction ex)
                        {
                            WriteError(ex.Message);
                        }

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        try
                        {
                            if (!player.CanStandSplitHand)
                            {
                                throw new InvalidCardAction("Player cannot stand on this hand");

                            }

                            player.StandSplitHand();
                            Console.WriteLine("");
                            Console.WriteLine("Player stands");
                            exitCode = true;
                        }
                        catch (InvalidCardAction ex)
                        {
                            WriteError(ex.Message);
                        }
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        try
                        {
                            if (!player.CanDoubleSplitHand)
                            {
                                throw new InvalidCardAction("Player cannot double this hand");
                            }

                            player.DoubleSplitHand(game.HitCard());
                            Console.WriteLine("");
                            Console.WriteLine("Player has doubled their hand");
                            Console.WriteLine();
                            ShowPlayerSplitCards(player);
                            if (player.SplitHandValue > PlayerHand.MAX_HAND_VALUE)
                            {
                                Console.WriteLine("Player is over {0}, Player Bust", PlayerHand.MAX_HAND_VALUE);
                            }

                            exitCode = true;
                        }
                        catch (InvalidCardAction ex)
                        {
                            WriteError(ex.Message);
                        }
                        exitCode = true;
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        ShowFirstTableCards();
                        break;

                }


                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static void ChangeDeckCount()
        {
            Console.Clear();
            WriteHeadingLine("Change deck count: ");


            int deckCount = 0;

            while (deckCount < 1 || deckCount > 6)
            {
                Console.Write("New Deck Value (1 - 6): ");
                try
                {
                    deckCount = int.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteError("Invalid deck count entered, enter a value between 1 and 6.");

                }
                Console.WriteLine("");
            }

            game.InitializeDeck(deckCount);
            Console.WriteLine("Deck count changed");
        }

        static void FinalizeGame()
        {
            game.ConcludeRound();
            ShowFinalTable();
        }


    }
}
