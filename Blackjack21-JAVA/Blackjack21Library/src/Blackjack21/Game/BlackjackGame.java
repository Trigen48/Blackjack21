package Blackjack21.Game;

import Blackjack21.Game.Exceptions.DeckEmptyException;
import Blackjack21.Game.Exceptions.PlayerExistsException;
import Blackjack21.Game.Exceptions.PlayerHandNotSplitException;
import Blackjack21.Game.Exceptions.PlayerNotFoundException;
import Blackjack21.Game.Logic.CardDeck;
import Blackjack21.Game.Logic.Player;
import Blackjack21.Game.Model.Card;
import java.util.ArrayList;

/**
 * A game class containing blackjack game logic
 */
public class BlackjackGame
{

    private final Player _dealer;
    private final ArrayList<Player> _players;
    private final CardDeck _deck;
    final int MIN_DECK_CARDS = 10;
    final int CARD_DEAL_MAX = 2;
    final int MINIMUM_DEALER_VALUE = 17;

    /**
     * initialize the blackjack game
     */
    public BlackjackGame()
    {
        this._dealer = new Player("Blackjack Dealer");
        this._players = new ArrayList<>();
        this._deck = new CardDeck();
    }

    /**
     * Initialize a new deck
     *
     * @param deckCount
     */
    public void initializeDeck(int deckCount)
    {
        this._deck.newDeck(deckCount);
    }

    /**
     * Start a new round of blackjack, throws an exception if a deck has not
     * been initialized
     */
    public void newGame()
    {
        this._dealer.clearHands();
        for (Player player : this._players)
        {
            player.clearHands();
        }
        if (this._deck.getCardCount() <= MIN_DECK_CARDS)
        {
            this._deck.shuffleDeck();
        }

    }

    /**
     * Find the player's index in the array and return -1 if the player was not
     * found
     *
     * @param playerName The player's name to search
     * @return Return the player's index
     */
    private int findPlayerIndex(String playerName)
    {
        int index = -1;
        for (int x = 0; x < this._players.size(); x++)
        {
            Player player = this._players.get(x);

            if (player.getPlayerName().equals(playerName))
            {
                index = x;
                break;
            }
        }
        return index;
    }

    /**
     * Add two cards to the dealer's hand and players hand in turns
     *
     * @throws Blackjack21.Game.Exceptions.DeckEmptyException
     */
    public void dealCards() throws DeckEmptyException
    {
        for (int cardsToDeal = 0; cardsToDeal < CARD_DEAL_MAX; cardsToDeal++)
        {
            for (Player player : this._players)
            {
                player.addFirstHandCard(this.hitCard());
            }
            this._dealer.addFirstHandCard(this.hitCard());
        }
        this._dealer.getFirstHand().setDealerResult();
    }

    /**
     * Returns the top most card from the deck
     *
     * @return Return the top removed card
     * @throws Blackjack21.Game.Exceptions.DeckEmptyException
     */
    public Card hitCard() throws DeckEmptyException
    {
        return this._deck.hitCard();
    }

    /**
     * Add cards to the dealer's hand, if the dealer's hand value is under 17
     */
    public void dealerHit() throws DeckEmptyException
    {

        while (this._dealer.getFirstHandValue() < MINIMUM_DEALER_VALUE)
        {
            this.addDealerCard(this.hitCard());
        }
    }

    /**
     * Add a new player to the game table, throws an exception if the player
     * name already exists
     *
     * @param playerName The new players name
     * @throws Blackjack21.Game.Exceptions.PlayerExistsException
     */
    public void addPlayer(String playerName) throws PlayerExistsException
    {
        // look for the player name in the list
        int index = this.findPlayerIndex(playerName);
        if (index != -1)
        {
            throw new PlayerExistsException(playerName);
        }

        this._players.add(new Player(playerName));
    }

    /**
     * Add a card to the dealer's hand
     *
     * @param card A Card to add to the dealer's hand
     */
    public void addDealerCard(Card card)
    {
        this._dealer.addFirstHandCard(card);
    }

    /**
     * Add cards to the dealer's hand
     *
     * @param cards Cards to add to the dealer's hand
     */
    public void addDealerCards(Card[] cards)
    {
        for (Card card : cards)
        {
            addDealerCard(card);
        }
    }

    /**
     * Add a card to the players hand, throws an exception if the player is not
     * found
     *
     * @param index The index order of the player
     * @param card A card to add to the player's hand
     * @throws Blackjack21.Game.Exceptions.PlayerNotFoundException
     */
    public void addPlayerCard(int index, Card card) throws PlayerNotFoundException
    {
        Player player = this.getGamePlayer(index);
        player.addFirstHandCard(card);
    }

    /**
     * Add a card to the player's hand, throws an exception if the player is not
     * found
     *
     * @param index The index order of the player
     * @param cards Cards to add to the player's hand
     * @throws Blackjack21.Game.Exceptions.PlayerNotFoundException
     */
    public void addPlayerCards(int index, Card[] cards) throws PlayerNotFoundException
    {
        for (Object obj : cards)
        {
            Card card = (Card) obj;
            addPlayerCard(index, card);
        }
    }

    /**
     * Add a card to a players hand using their name, throws an exception if the
     * player is not found
     *
     * @param playerName The player's name
     * @param card The card to add to the player's hand
     * @throws Blackjack21.Game.Exceptions.PlayerNotFoundException
     */
    public void addPlayerCard(String playerName, Card card) throws PlayerNotFoundException
    {
        int index = findPlayerIndex(playerName);
        addPlayerCard(index, card);
    }

    /**
     * Add cards to a players hand using their name, throws an exception if the
     * player is not found
     *
     * @param playerName The player's name
     * @param cards Cards to add to the player's hand
     * @throws Blackjack21.Game.Exceptions.PlayerNotFoundException
     */
    public void addPlayerCards(String playerName, Card[] cards) throws PlayerNotFoundException
    {
        int index = findPlayerIndex(playerName);
        addPlayerCards(index, cards);
    }

    public Player getDealer()
    {
        return this._dealer;
    }

    /**
     * Get the player using the index number, throws a player not found
     * exception if the player index is not correct
     *
     * @param index Player's++ index number
     * @return Return the player object
     * @throws Blackjack21.Game.Exceptions.PlayerNotFoundException
     */
    public Player getGamePlayer(int index) throws PlayerNotFoundException
    {
        if (this._players.size() > index && index >= 0)
        {
            return this._players.get(index);
        }

        throw new PlayerNotFoundException(index);
    }

    /**
     * Return the player object using the player's name, throws an exception if
     * the player is not found
     *
     * @param playerName The player name to search
     * @return Returns the player object
     * @throws Blackjack21.Game.Exceptions.PlayerNotFoundException
     */
    public Player getGamePlayer(String playerName) throws PlayerNotFoundException
    {
        int index = findPlayerIndex(playerName);
        if (index != -1)
        {
            return this.getGamePlayer(index);
        }

        throw new PlayerNotFoundException(playerName);
    }

    /**
     * Complete the current round of blackjack
     *
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public void concludeRound() throws PlayerHandNotSplitException
    {
        this._dealer.getFirstHand().setDealerResult();
        for (Player player : _players)
        {
            player.setHandResult(this._dealer);
        }
    }

    /**
     *
     * @return
     */
    @Override
    public String toString()
    {

        String result = "+ Dealer Details\n\n";
        result += this._dealer.toString();
        result += "+ Players\n\n";
        for (Player player : this._players)
        {
            result += player.toString();
        }
        return result;

    }

}
