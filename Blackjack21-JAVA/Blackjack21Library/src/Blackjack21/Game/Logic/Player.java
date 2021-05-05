package Blackjack21.Game.Logic;

import Blackjack21.Game.Exceptions.InvalidCardActionException;
import Blackjack21.Game.Exceptions.InvalidHandSplitException;
import Blackjack21.Game.Exceptions.PlayerHandNotSplitException;
import Blackjack21.Game.Model.Card;
import Blackjack21.Game.Model.PlayerHandResult;
import Blackjack21.Game.Model.PlayerHandType;

/**
 * A blackjack player object
 */
public class Player
{

    // Store our hand in a list, a player can have an additional hand if they choose to split.
    private final PlayerHand[] _playerHand;  // a player can only split his hand once there for we use an array

    private PlayerHandType _playerHandType;
    private boolean _playerInsured;
    private boolean _playerIsuranceCorrect;
    private final String _playerName;

    /**
     * initializes the player object
     *
     * @param playerName The player's name
     */
    public Player(String playerName)
    {
        // set player default hands to 2
        this._playerHand = new PlayerHand[]
        {
            new PlayerHand(), new PlayerHand()
        };
        this._playerName = playerName;
        this._playerHandType = PlayerHandType.SINGLE_HAND;
        this._playerIsuranceCorrect = false;
        this._playerInsured = false;
    }

    /**
     * Returns a value to indicate if the player has a single or split hand
     *
     * @return
     */
    public PlayerHandType getCurrentHandType()
    {
        return this._playerHandType;
    }

    /**
     * Gets the players first hand
     *
     * @return
     */
    public PlayerHand getFirstHand()
    {
        return this._playerHand[0];
    }

    /**
     * Gets the player's split hand, throws an exception if the player does not
     * have a split hand
     *
     * @return
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public PlayerHand getSplitHand() throws PlayerHandNotSplitException
    {
        if (this._playerHandType != PlayerHandType.SPLIT_HAND)
        {
            throw new PlayerHandNotSplitException();
        }

        return this._playerHand[1];
    }

    public void setIsurance(int dealerHandValue)
    {
        this._playerInsured = true;
        this._playerIsuranceCorrect = dealerHandValue == PlayerHand.MAX_HAND_VALUE;
    }

    /**
     * Gets if the player is insured
     *
     * @return
     */
    public boolean getPlayerInsured()
    {
        return _playerInsured;
    }

    /**
     * Gets if the players insurance was correct
     *
     * @return
     */
    public boolean getPlayerIsuranceCorrect()
    {
        return this._playerIsuranceCorrect;
    }

    /**
     * Returns the player's name, throws an exception if the player name is
     * empty
     *
     * @return
     */
    public String getPlayerName()
    {
        return _playerName;
    }

    /**
     * Gets of the player's hand can be split
     *
     * @return
     */
    public boolean getCanSplitHand()
    {
        return this._playerHandType == PlayerHandType.SINGLE_HAND && this.getFirstHand().getHasTwoPairs();
    }

    /**
     * Gets if the player can double their hand
     *
     * @return
     */
    public boolean getCanDouble()
    {
        return this.getFirstHand().getCardCount() == 2 && this.getFirstHandValue() >= 9 && this.getFirstHandValue() <= 11;
    }

    /**
     * Gets if the player can add more cards to their hand
     *
     * @return
     */
    public boolean getCanHit()
    {
        return this.getFirstHandValue() < PlayerHand.MAX_HAND_VALUE;
    }

    /**
     * Gets if the player can stand
     *
     * @return
     */
    public boolean getCanStand()
    {
        return this.getFirstHandValue() < PlayerHand.MAX_HAND_VALUE;
    }

    /**
     * Gets if the player can fold their hand
     *
     * @return
     */
    public boolean getCanFold()
    {
        return this.getCurrentHandType() == PlayerHandType.SINGLE_HAND && this.getFirstHand().getCardCount() == 2;
    }

    /**
     * Double the players first hand only
     *
     * @param card Card to add to the doubled hand
     * @throws Blackjack21.Game.Exceptions.InvalidCardActionException
     */
    public void doubleHand(Card card) throws InvalidCardActionException
    {
        if (!getCanDouble())
        {
            throw new InvalidCardActionException("Cannot double the player's hand, conditions not met");
        }

        getFirstHand().doubleHandCard(card);
    }

    /**
     * Fold the player's hand
     *
     * @throws Blackjack21.Game.Exceptions.InvalidCardActionException
     */
    public void fold() throws InvalidCardActionException
    {
        if (!getCanFold())
        {
            throw new InvalidCardActionException("Cannot fold the player's hand, conditions not met");
        }

        this.getFirstHand().fold();
    }

    /**
     * Stand on the player's current hand
     *
     * @throws Blackjack21.Game.Exceptions.InvalidCardActionException
     */
    public void stand() throws InvalidCardActionException
    {
        if (!getCanStand())
        {
            throw new InvalidCardActionException("Cannot stand the player's hand, conditions not met");
        }

        this.getFirstHand().stand();
    }

    /**
     * Gets if the player can double their hand
     *
     * @return
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public boolean getCanDoubleSplitHand() throws PlayerHandNotSplitException
    {
        return this.getSplitHand().getCardCount() == 2 && this.getSplitHandValue() >= 9 && this.getSplitHandValue() <= 11;
    }

    /**
     * Gets if the player can add more cards to their split hand
     *
     * @return
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public boolean getCanHitSplitHand() throws PlayerHandNotSplitException
    {
        return this.getSplitHandValue() < PlayerHand.MAX_HAND_VALUE;
    }

    /**
     * Gets if the player can stand on their split hand
     *
     * @return
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public boolean getCanStandSplitHand() throws PlayerHandNotSplitException
    {
        return this.getSplitHandValue() < PlayerHand.MAX_HAND_VALUE;
    }

    /**
     * Double the player's split hand only
     *
     * @param card Card to add to the doubled hand
     * @throws Blackjack21.Game.Exceptions.InvalidCardActionException
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public void doubleSplitHand(Card card) throws InvalidCardActionException, PlayerHandNotSplitException
    {
        if (!getCanDoubleSplitHand())
        {
            throw new InvalidCardActionException("Cannot double the player's hand, conditions not met");
        }

        this.getSplitHand().doubleHandCard(card);
    }

    /**
     * Stand on the player's current hand
     *
     * @throws Blackjack21.Game.Exceptions.InvalidCardActionException
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public void standSplitHand() throws InvalidCardActionException, PlayerHandNotSplitException
    {
        if (!getCanStandSplitHand())
        {
            throw new InvalidCardActionException("Cannot stand the player's split hand, conditions not met");
        }

        this.getSplitHand().stand();
    }

    /**
     * Splits the players hand into two, throws an exception if the hand does
     * not meet split conditions
     *
     * @throws Blackjack21.Game.Exceptions.InvalidHandSplitException
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public void splitHandCards() throws InvalidHandSplitException, PlayerHandNotSplitException
    {
        if (this._playerHandType == PlayerHandType.SINGLE_HAND && this.getFirstHand().getHasTwoPairs() == true)
        {
            this._playerHandType = PlayerHandType.SPLIT_HAND;
            addSplitHandCard(this._playerHand[0].splitHandCard());
            return;
        }

        throw new InvalidHandSplitException();
    }

    /**
     * Adds a card to the player's first hand
     *
     * @param card Card to add to the player's hand
     */
    public void addFirstHandCard(Card card)
    {
        this._playerHand[0].addCard(card);
    }

    /**
     * Adds cards to the player's first hand
     *
     * @param cards Card to add to the player's hand
     */
    public void addFirstHandCards(Card[] cards)
    {
        for (Object obj : cards)
        {
            Card card = (Card) obj;
            addFirstHandCard(card);
        }
    }

    /**
     * Adds a card to the players split hand, provided the player has a split
     * hand, throws an exception if the player's hand is not split
     *
     * @param card Card to add to the player's split hand
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public void addSplitHandCard(Card card) throws PlayerHandNotSplitException
    {
        if (this._playerHandType != PlayerHandType.SPLIT_HAND)
        {
            throw new PlayerHandNotSplitException();
        }

        this._playerHand[1].addCard(card);
    }

    /**
     * Adds cards to the players split hand, provided the player has a split
     * hand
     *
     * @param cards Cards to add to the player's split hand
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public void addSplitHandCards(Card[] cards) throws PlayerHandNotSplitException
    {
        for (Object obj : cards)
        {
            Card card = (Card) obj;
            addSplitHandCard(card);
        }
    }

    /**
     * Get the value of the player's first hand
     *
     * @return
     */
    public int getFirstHandValue()
    {
        return this._playerHand[0].getHandValue();
    }

    /**
     * Gets the player's first hand result
     *
     * @return
     */
    public PlayerHandResult getFirstHandResult()
    {
        return this._playerHand[0].getHandResult();
    }

    /**
     * Get the value of the player's split hand, throws an exception if the
     * player does not have a split hand
     *
     * @return
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public int getSplitHandValue() throws PlayerHandNotSplitException
    {
        if (this._playerHandType != PlayerHandType.SPLIT_HAND)
        {
            throw new PlayerHandNotSplitException();
        }

        return this._playerHand[1].getHandValue();
    }

    /**
     * Gets the player's split hand result
     *
     * @return
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public PlayerHandResult getSplitHandResult() throws PlayerHandNotSplitException
    {
        if (this._playerHandType != PlayerHandType.SPLIT_HAND)
        {
            throw new PlayerHandNotSplitException();
        }

        return this._playerHand[1].getHandResult();
    }

    /**
     * Sets the players hand result based on the dealers hand
     *
     * @param dealer The dealers object
     * @throws Blackjack21.Game.Exceptions.PlayerHandNotSplitException
     */
    public void setHandResult(Player dealer) throws PlayerHandNotSplitException
    {
        if (this.getCurrentHandType() == PlayerHandType.SINGLE_HAND)
        {
            this.getFirstHand().setHandResult(dealer.getFirstHand(), true);
        }
        else
        {
            this.getFirstHand().setHandResult(dealer.getFirstHand(), false);
            this.getSplitHand().setHandResult(dealer.getFirstHand(), false);
        }
    }

    /**
     * Clear the player's active hand cards
     */
    public void clearHands()
    {
        this._playerHand[0].clearCards();
        this._playerHand[1].clearCards();
        // clear split hand cards
        this._playerHandType = PlayerHandType.SINGLE_HAND;
        this._playerIsuranceCorrect = false;
        this._playerInsured = false;
    }

    /**
     * Returns the player object as a string
     *
     * @return return the player as a string
     */
    @Override
    public String toString()
    {

        String result;
        result = "Player: " + getPlayerName();
        if (this.getPlayerInsured())
        {
            result += "\n\n-- Player placed an insurance bet and ".toUpperCase() + (this.getPlayerIsuranceCorrect() ? "WON" : "LOST") + "";
        }

        result += "\n\n-- Hand Cards: \n";
        result += getFirstHand().toString();
        if (this._playerHandType == PlayerHandType.SPLIT_HAND)
        {
            result += "\n-- Split Hand Cards: ";
            result += getFirstHand().toString();
        }

        result += "------------------------------\n";
        return result;

    }

}
