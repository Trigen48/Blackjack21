package Blackjack21.ConsoleTest;

import Blackjack21.Game.BlackjackGame;
import Blackjack21.Game.Model.Card;
import Blackjack21.Game.Model.CardSymbol;
import Blackjack21.Game.Model.CardType;
import java.util.Scanner;

public class TestCaseApplication
{

    private static BlackjackGame game;
    static Scanner Scan;

    public static void initTest() throws Exception
    {
        // Define Test Players
        String PLAYER1 = "Lemmy";
        String PLAYER2 = "Andrew";
        String PLAYER3 = "Billy";
        String PLAYER4 = "Carla";
        game = new BlackjackGame();
        // Add new players
        game.addPlayer(PLAYER1);
        game.addPlayer(PLAYER2);
        game.addPlayer(PLAYER3);
        game.addPlayer(PLAYER4);
        // init new game
        //game.NewGame();
        // Dealer Cards
        game.addDealerCards(new Card[]
        {
            new Card(CardType.JACK, CardSymbol.SPADES), new Card(CardType.NINE, CardSymbol.HEARTS)
        });
        // Add Player 1 Cards : Lemmy
        game.addPlayerCards(PLAYER1, new Card[]
        {
            new Card(CardType.ACE, CardSymbol.SPADES), new Card(CardType.SEVEN, CardSymbol.HEARTS), new Card(CardType.ACE, CardSymbol.DIAMONDS)
        });
        // Add Player 2 Cards : Andrew
        game.addPlayerCards(PLAYER2, new Card[]
        {
            new Card(CardType.KING, CardSymbol.DIAMONDS), new Card(CardType.FOUR, CardSymbol.SPADES), new Card(CardType.FOUR, CardSymbol.CLUBS)
        });
        // Add Player 3 Cards : Billy
        game.addPlayerCards(PLAYER3, new Card[]
        {
            new Card(CardType.TWO, CardSymbol.SPADES), new Card(CardType.TWO, CardSymbol.DIAMONDS), new Card(CardType.TWO, CardSymbol.HEARTS), new Card(CardType.FOUR, CardSymbol.DIAMONDS), new Card(CardType.FIVE, CardSymbol.CLUBS)
        });
        // Add Player 4 Cards Carla
        game.addPlayerCards(PLAYER4, new Card[]
        {
            new Card(CardType.QUEEN, CardSymbol.CLUBS), new Card(CardType.SIX, CardSymbol.SPADES), new Card(CardType.NINE, CardSymbol.DIAMONDS)
        });
        // Ends the current round of the game
        game.concludeRound();
    }

    public static void testResults() throws Exception
    {
    }

    public static void main(String[] args)
    {
        Scan = new Scanner(System.in);
        System.out.println("Blackjack 21 Test case");
        System.out.println("-----------------------------");
        try
        {
            initTest();
            System.out.println("");
            System.out.println(game.toString());
        }
        catch (Exception ex)
        {
            System.out.println(ex.getMessage());
        }

        System.out.println();
        System.out.println("Press any key to continue");
        Scan.nextLine();
    }

}
