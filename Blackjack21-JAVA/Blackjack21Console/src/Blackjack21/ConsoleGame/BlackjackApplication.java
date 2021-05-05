package Blackjack21.ConsoleGame;

import Blackjack21.Game.BlackjackGame;
import Blackjack21.Game.Exceptions.InvalidCardActionException;
import Blackjack21.Game.Exceptions.InvalidHandSplitException;
import Blackjack21.Game.Exceptions.PlayerExistsException;
import Blackjack21.Game.Exceptions.PlayerHandNotSplitException;
import Blackjack21.Game.Exceptions.PlayerNotFoundException;
import Blackjack21.Game.Logic.Player;
import Blackjack21.Game.Logic.PlayerHand;
import Blackjack21.Game.Model.CardType;
import Blackjack21.Game.Model.PlayerHandType;
import Blackjack21.Game.Model.Card;
import java.util.Scanner;

public class BlackjackApplication
{

    static BlackjackGame game;
    static int playerCount = 0;
    static Scanner Scan;

    static void initGame()
    {
        game = new BlackjackGame();
        game.initializeDeck(1);

    }

    static void writeHeadingLine(String header)
    {
        System.out.println("-- " + header.toUpperCase());
        System.out.println("--");
        System.out.println("");

    }

    static void writeError(String error)
    {
        System.out.println(error);
    }

    static void writeMainMenu()
    {
        System.out.println("Select a game action: ");
        System.out.println("  1 - New Round");
        System.out.println("  2 - View Players");
        System.out.println("  3 - Change Deck Count");
        System.out.println("  E - Exit Game");
        System.out.println();
        System.out.print("Press key for game action: ");
    }

    static void showFirstTableCards() throws PlayerHandNotSplitException, PlayerNotFoundException
    {
        System.out.println("Dealer Cards: ");
        System.out.println("-- [{" + game.getDealer().getFirstHand().getCard(0).toString() + "}] [? ? ?]");
        System.out.println();
        for (int x = 0; x < playerCount; x++)
        {
            Player player = game.getGamePlayer(x);
            showPlayerCards(player);
            if (player.getCurrentHandType() == PlayerHandType.SPLIT_HAND)
            {
                showPlayerSplitCards(player);
            }

        }
    }

    static void showPlayerCards(Player player)
    {
        System.out.println(player.getPlayerName() + " First Hand Cards: ");
        System.out.print("-- ");
        for (Card card : player.getFirstHand().getCards())
        {
            System.out.print("[{" + card.toString() + "}] ");
        }
        System.out.println();
        System.out.println("-- Hand Value: " + player.getFirstHandValue());
        System.out.println();
    }

    static void showPlayerSplitCards(Player player) throws PlayerHandNotSplitException
    {
        System.out.println("{" + player.getPlayerName() + "} Split Hand Cards: ");
        System.out.print("-- ");
        for (Card card : player.getSplitHand().getCards())
        {
            System.out.print("[{" + card.toString() + "}] ");
        }
        System.out.println();
        System.out.println("-- Hand Value: {0}" + player.getFirstHandValue());
        System.out.println();
    }

    static void showFinalTable() throws PlayerNotFoundException
    {

        System.out.println("++ Dealer");
        System.out.println("++");
        System.out.println(game.getDealer().toString());
        System.out.println();
        System.out.println("++ Players");
        System.out.println("++");
        for (int x = 0; x < playerCount; x++)
        {
            System.out.println(game.getGamePlayer(x).toString());
        }

    }

    static void showPlayers() throws Exception
    {
        clear();
        writeHeadingLine("Showing current table players");
        for (int x = 0; x < playerCount; x++)
        {
            System.out.println("-- Player " + (x + 1) + " -  " + game.getGamePlayer(x).getPlayerName());
        }
    }

    public static void main(String[] args)
    {
        BlackjackApplication.Main();
    }

    static void Main()
    {
        Scan = new Scanner(System.in);
        String playerCountString;

        System.out.println("Console Blackjack Game");
        System.out.println("");
        writeHeadingLine("Player setup");
        while (playerCount <= 0 || playerCount > 7)
        {
            System.out.print("Enter the number of players (1 - 7): ");
            playerCountString = Scan.next();
            try
            {
                playerCount = Integer.parseInt(playerCountString);
            }
            catch (NumberFormatException exc)
            {
                writeError("Invalid value entered, enter a value between 1 and 7.");
            }

            System.out.println("");
        }
        // initialize the game object
        initGame();
        writeHeadingLine("Setup player names");
        for (int x = 1; x <= playerCount; x++)
        {
            String playerName = "";
            while (playerName.equals("") == true)
            {
                System.out.print("Enter player " + x + "'s name: ");
                playerName = Scan.next().trim().toUpperCase();
                try
                {
                    game.addPlayer(playerName);
                }
                catch (PlayerExistsException ex)
                {
                    writeError(ex.getMessage());
                    playerName = "";
                }

                System.out.println("");
            }
        }
        boolean loopMenu = true;
        while (loopMenu)
        {
            clear();
            writeHeadingLine("Blackjack game");
            writeMainMenu();

            String key = Scan.next().toUpperCase().trim();

            switch (key)
            {
                case "1":
                    try
                    {
                        newGame();
                    }
                    catch (Exception ex)
                    {
                        writeError(ex.getMessage());
                    }
                    System.out.println();
                    System.out.println("Press the enter key to continue");
                    Scan.nextLine();
                    break;

                case "2":
                    try
                    {
                        showPlayers();
                    }
                    catch (Exception ex)
                    {
                        writeError(ex.getMessage());
                    }
                    System.out.println();
                    System.out.println("Press the enter key to continue");
                    Scan.nextLine();
                    break;

                case "3":
                    try
                    {
                        changeDeckCount();
                    }
                    catch (Exception ex)
                    {

                    }
                    System.out.println();
                    System.out.println("Press the enter key to continue");
                    Scan.nextLine();
                    break;

                case "E":
                    loopMenu = false;
                    break;
            }

        }
    }

    static void newGame() throws Exception
    {
        clear();
        writeHeadingLine("New blackjack round: ");
        game.newGame();
        // Deal 2 Cards to each player and the dealer
        game.dealCards();
        // Show each players cards
        showFirstTableCards();
        int dealerValue = game.getDealer().getFirstHandValue();
        if (game.getDealer().getFirstHand().getCard(0).getCardType() == CardType.ACE)
        {
            writeHeadingLine("Dealer has a face up ace card. Insure ?");
            for (int x = 0; x < playerCount; x++)
            {
                Player player = game.getGamePlayer(x);
                boolean exitInsure = false;
                while (!exitInsure)
                {
                    System.out.print(player.getPlayerName() + " wants to insure? (1 - yes, 2 - no)  ");

                    String key = Scan.next().toUpperCase().trim();

                    switch (key)
                    {
                        case "1":
                            player.setIsurance(dealerValue);
                            exitInsure = true;
                            break;

                        case "2":
                            exitInsure = true;
                            break;

                    }

                    System.out.println("");
                    System.out.println("");
                }
            }
        }

        if (dealerValue != 21)
        {
            for (int x = 0; x < playerCount; x++)
            {

                playerOptions(game.getGamePlayer(x));

            }
            game.dealerHit();
        }

        // Server the dealer cards until he reaches a soft 17 value
        finalizeGame();

    }

    static void playerOptions(Player player) throws Exception
    {
        if (player.getFirstHand().getIsBlackjack())
        {
            return;
        }

        writeHeadingLine("Player " + player.getPlayerName() + "'s Turn");
        if (player.getCanSplitHand())
        {
            boolean exitSplit = false;
            while (!exitSplit)
            {
                System.out.print(player.getPlayerName() + " wants to split the hand? (1 = yes, 2 = no):  ");

                String key = Scan.next().toUpperCase().trim();

                switch (key)
                {
                    case "1":
                        try
                        {
                            player.splitHandCards();
                            // Add two new cards to the split hand and first hand
                            player.addFirstHandCard(game.hitCard());
                            player.addSplitHandCard(game.hitCard());
                            System.out.println("");
                            System.out.println("Player has split their hand");
                            exitSplit = true;
                        }
                        catch (InvalidHandSplitException ex)
                        {
                            writeError(ex.getMessage());
                        }
                        break;

                    case "2":
                        exitSplit = true;
                        break;

                }

                System.out.println("");
                System.out.println("");
            }
        }

        singleHandMenu(player);

        if (player.getCurrentHandType() == PlayerHandType.SPLIT_HAND)
        {
            splitHandMenu(player);
        }

    }

    static void singleHandMenu(Player player) throws Exception
    {
        clear();
        writeHeadingLine("Player " + player.getPlayerName() + "'s turn Hand 1");
        boolean exitCode = false;
        while (!exitCode && player.getFirstHandValue() < PlayerHand.MAX_HAND_VALUE)
        {

            if (player.getCanFold())
            {
                System.out.println("  1 - Fold Hand");
            }

            if (player.getCanHit())
            {
                System.out.println("  2 - Hit Hand");
            }

            if (player.getCanStand())
            {
                System.out.println("  3 - Stand");
            }

            if (player.getCanDouble())
            {
                System.out.println("  4 - Double");
            }

            System.out.println("  5 - Show Current Table");

            System.out.println();
            showPlayerCards(player);
            System.out.println();
            System.out.print("Select a player option: ");
            String key = Scan.next().toUpperCase().trim();

            switch (key)
            {
                case "1":
                    System.out.println("");
                    System.out.println("");
                    try
                    {
                        if (!player.getCanFold())
                        {
                            throw new InvalidCardActionException("Player cannot fold this hand");
                        }

                        player.fold();
                        System.out.println("");
                        System.out.println("Player has folded their hand");
                        exitCode = true;
                    }
                    catch (InvalidCardActionException ex)
                    {
                        writeError(ex.getMessage());
                    }
                    break;

                case "2":
                    System.out.println("");
                    System.out.println("");
                    try
                    {
                        if (!player.getCanHit())
                        {
                            throw new InvalidCardActionException("Player cannot hit this hand");
                        }

                        player.addFirstHandCard(game.hitCard());
                        System.out.println("");
                        System.out.println("Player has added a card to their hand");
                        System.out.println();
                        showPlayerCards(player);
                        if (player.getFirstHandValue() > PlayerHand.MAX_HAND_VALUE)
                        {
                            System.out.println("Player is over " + PlayerHand.MAX_HAND_VALUE + ", Player Bust");
                            exitCode = true;
                        }

                    }
                    catch (InvalidCardActionException ex)
                    {
                        writeError(ex.getMessage());
                    }
                    break;

                case "3":
                    System.out.println("");
                    System.out.println("");
                    try
                    {
                        if (!player.getCanStand())
                        {
                            throw new InvalidCardActionException("Player cannot stand on this hand");
                        }

                        player.stand();
                        System.out.println("");
                        System.out.println("Player stands");
                        exitCode = true;
                    }
                    catch (InvalidCardActionException ex)
                    {
                        writeError(ex.getMessage());
                    }
                    break;

                case "4":
                    System.out.println("");
                    System.out.println("");
                    try
                    {
                        if (!player.getCanDouble())
                        {
                            throw new InvalidCardActionException("Player cannot double this hand");
                        }

                        player.doubleHand(game.hitCard());
                        System.out.println("");
                        System.out.println("Player has doubled their hand");
                        System.out.println();
                        showPlayerCards(player);
                        if (player.getFirstHandValue() > PlayerHand.MAX_HAND_VALUE)
                        {
                            System.out.println("Player is over " + PlayerHand.MAX_HAND_VALUE + ", Player Bust");
                        }

                        exitCode = true;
                    }
                    catch (InvalidCardActionException ex)
                    {
                        writeError(ex.getMessage());
                    }

                    exitCode = true;
                    break;

                case "5":
                    System.out.println("");
                    System.out.println("");
                    showFirstTableCards();
                    break;

            }

            System.out.println();
            System.out.println();
        }
    }

    static void splitHandMenu(Player player) throws Exception
    {
        clear();
        writeHeadingLine("Player " + player.getPlayerName() + "'s turn Hand 2");
        boolean exitCode = false;
        while (!exitCode && player.getSplitHandValue() < PlayerHand.MAX_HAND_VALUE)
        {

            if (player.getCanHitSplitHand())
            {
                System.out.println("  1 - Hit Hand");
            }

            if (player.getCanStandSplitHand())
            {
                System.out.println("  2 - Stand");
            }

            if (player.getCanDoubleSplitHand())
            {
                System.out.println("  3 - Double");
            }

            System.out.println("  4 - Show Current Table");

            System.out.println();
            showPlayerSplitCards(player);
            System.out.println();
            System.out.print("Select a player option: ");
            String key = Scan.next().toUpperCase().trim();

            switch (key)
            {
                case "1":
                    System.out.println("");
                    System.out.println("");
                    try
                    {
                        if (!player.getCanHitSplitHand())
                        {
                            throw new InvalidCardActionException("Player cannot hit split hand");
                        }

                        player.addSplitHandCard(game.hitCard());
                        System.out.println("");
                        System.out.println("Player has added a card to their hand");
                        System.out.println();
                        showPlayerSplitCards(player);
                        if (player.getFirstHandValue() > PlayerHand.MAX_HAND_VALUE)
                        {
                            System.out.println("Player is over " + PlayerHand.MAX_HAND_VALUE + ", Player Bust");
                            exitCode = true;
                        }

                    }
                    catch (InvalidCardActionException ex)
                    {
                        writeError(ex.getMessage());
                    }
                    break;

                case "2":
                    System.out.println("");
                    System.out.println("");
                    try
                    {
                        if (!player.getCanStandSplitHand())
                        {
                            throw new InvalidCardActionException("Player cannot stand on this hand");
                        }

                        player.standSplitHand();
                        System.out.println("");
                        System.out.println("Player stands");
                        exitCode = true;
                    }
                    catch (InvalidCardActionException ex)
                    {
                        writeError(ex.getMessage());
                    }
                    break;

                case "3":
                    System.out.println("");
                    System.out.println("");
                    try
                    {
                        if (!player.getCanDoubleSplitHand())
                        {
                            throw new InvalidCardActionException("Player cannot double this hand");
                        }

                        player.doubleSplitHand(game.hitCard());
                        System.out.println("");
                        System.out.println("Player has doubled their hand");
                        System.out.println();
                        showPlayerSplitCards(player);
                        if (player.getSplitHandValue() > PlayerHand.MAX_HAND_VALUE)
                        {
                            System.out.println("Player is over " + PlayerHand.MAX_HAND_VALUE + ", Player Bust");
                        }

                        exitCode = true;
                    }
                    catch (InvalidCardActionException ex)
                    {
                        writeError(ex.getMessage());
                    }

                    break;

                case "4":
                    System.out.println("");
                    System.out.println("");
                    showFirstTableCards();
                    break;

            }

            System.out.println();
            System.out.println();
        }
    }

    static void changeDeckCount()
    {
        clear();
        writeHeadingLine("Change deck count: ");
        int deckCount = 0;
        while (deckCount < 1 || deckCount > 6)
        {
            System.out.print("New Deck Value (1 - 6): ");
            try
            {
                deckCount = Integer.parseInt(Scan.next());
            }
            catch (NumberFormatException exc)
            {
                writeError("Invalid deck count entered, enter a value between 1 and 6.");
            }

            System.out.println("");
        }
        game.initializeDeck(deckCount);
        System.out.println("Deck count changed");
    }

    static void finalizeGame() throws Exception
    {
        game.concludeRound();
        showFinalTable();

    }

    static void clear()
    {
        System.out.print("\033[H\033[2J");
        System.out.flush();
    }

}
