package Blackjack21.Game.Logic;

import Blackjack21.Game.Model.Card;
import Blackjack21.Game.Model.CardSymbol;
import Blackjack21.Game.Model.CardType;
import Blackjack21.Game.Model.PlayerHandAction;
import Blackjack21.Game.Model.PlayerHandResult;
import java.util.ArrayList;
import java.util.List;

/**
 * An object that stores the players hand cards
 */
public class PlayerHand
{
    
    private final ArrayList<Card> _cards;
    private int _handValue = 0;
    private PlayerHandResult _playerHandResult;
    private PlayerHandAction _playerHandAction;
    public static final int MAX_HAND_VALUE = 21;

    // Keep track of the number of ace cards in the players hand
    private int aceCount = 0;

    /**
     * Initialize the player's hand
     */
    public PlayerHand()
    {
        this._cards = new ArrayList<>();
        this._handValue = 0;
        this.aceCount = 0;
        this._playerHandResult = PlayerHandResult.NONE;
        this._playerHandAction = PlayerHandAction.NONE;
    }

    /**
     * Add new card to the player's hand
     *
     * @param cardType Value of the card type
     * @param cardSymbol Value of the card symbol
     */
    public void addCard(CardType cardType, CardSymbol cardSymbol)
    {
        addCard(new Card(cardType, cardSymbol));
    }

    /**
     * Add new card to the player's hand
     *
     * @param card Card Object
     */
    public void addCard(Card card)
    {
        this._cards.add(card);
        if (card.getCardType() == CardType.ACE)
        {
            this.aceCount++;
        }
        
        this._handValue += card.getCardValue();
    }

    /**
     * Returns the players hand result
     *
     * @return
     */
    public PlayerHandResult getHandResult()
    {
        return this._playerHandResult;
    }

    /**
     * Gets if the hand has two pairs of the same card
     *
     * @return
     */
    public boolean getHasTwoPairs()
    {
        return this.getCardCount() == 2 && this.getCards().get(0).compareCardType(this.getCards().get(1));
    }

    /**
     * Gets a card from a given index
     *
     * @param index
     * @return
     */
    public Card getCard(int index)
    {
        return this._cards.get(index);
    }

    /**
     * Return the player's current hand cards
     *
     * @return
     */
    public List<Card> getCards()
    {
        return this._cards;
    }

    /**
     * Clear the player's hand cards
     */
    public void clearCards()
    {
        this._cards.clear();
        this.aceCount = 0;
        this._handValue = 0;
        this._playerHandResult = PlayerHandResult.NONE;
        this._playerHandAction = PlayerHandAction.NONE;
    }

    /**
     * Doubles the players hand
     *
     * @param card Card to add to the doubled hand
     */
    public void doubleHandCard(Card card)
    {
        this._playerHandAction = PlayerHandAction.DOUBLE;
        addCard(card);
    }

    /**
     * Fold the player's hand
     */
    public void fold()
    {
        this._playerHandAction = PlayerHandAction.FOLD;
    }

    /**
     * Stand on the player's current hand
     */
    public void stand()
    {
        this._playerHandAction = PlayerHandAction.STAND;
    }

    /**
     * Remove the second card from the hand and return it
     *
     * @return Returns the second card from the hand
     */
    public Card splitHandCard()
    {
        Card card = _cards.get(1);
        this._cards.remove(1);
        // reduce the ace card count if the split card is an ace
        if (card.getCardType() == CardType.ACE)
        {
            this.aceCount--;
        }
        
        this._handValue -= card.getCardValue();
        return card;
    }

    /**
     * Get the player's total amount of cards
     *
     * @return
     */
    public int getCardCount()
    {
        return this._cards.size();
    }

    /**
     * Checks if the hand is an automatic blackjack
     *
     * @return
     */
    public boolean getIsBlackjack()
    {
        return this.getHandValue() == MAX_HAND_VALUE && this.getCardCount() == 2;
    }

    /**
     * Returns the players hand action
     *
     * @return
     */
    public PlayerHandAction getHandAction()
    {
        return this._playerHandAction;
    }

    /**
     * Sets the dealers hand card result
     */
    public void setDealerResult()
    {
        if (this.getIsBlackjack())
        {
            this._playerHandResult = PlayerHandResult.PLAYER_WON_BLACKJACK;
        }
        else if (this.getHandValue() > MAX_HAND_VALUE)
        {
            this._playerHandResult = PlayerHandResult.PLAYER_BUST_OVER_21;
        }
        
    }

    /**
     * Sets the hands result based on the dealers hand
     *
     * @param dealerHand The dealer's hand to comapre to the current hand
     * @param IsSingleHand Set if the the hand is from a split
     */
    public void setHandResult(PlayerHand dealerHand, boolean IsSingleHand)
    {
        if (IsSingleHand && this.getIsBlackjack())
        {
            if (this.getIsBlackjack() == dealerHand.getIsBlackjack())
            {
                this._playerHandResult = PlayerHandResult.PLAYER_HAND_PUSH_BLACKJACK;
            }
            else
            {
                this._playerHandResult = PlayerHandResult.PLAYER_WON_BLACKJACK;
            }
        }
        else
        {
            if (dealerHand.getIsBlackjack())
            {
                this._playerHandResult = PlayerHandResult.PLAYER_HAND_LOWER_BLACKJACK;
            }
            else if (this.getHandValue() > MAX_HAND_VALUE)
            {
                this._playerHandResult = PlayerHandResult.PLAYER_BUST_OVER_21;
            }
            else if (dealerHand.getHandValue() > this.getHandValue() && dealerHand.getHandValue() <= MAX_HAND_VALUE)
            {
                this._playerHandResult = PlayerHandResult.PLAYER_HAND_LOWER;
            }
            else if (dealerHand.getHandValue() == this.getHandValue())
            {
                this._playerHandResult = PlayerHandResult.PLAYER_HAND_PUSH;
            }
            else if (this.getCardCount() == 5)
            {
                // this rule is not commonly used, mostly used in home settings
                this._playerHandResult = PlayerHandResult.PLAYER_WON_5_CARD_CHARLIE;
            }
            else
            {
                this._playerHandResult = PlayerHandResult.PLAYER_WON_HIGHER;
            }
        }
    }

    /**
     * Return sum of the player's hand cards
     *
     * @return
     */
    public int getHandValue()
    {
        int handValue = this._handValue;
        int softHandValue = (handValue + 10);
        // check if there are any aces in the players hand, return the hand value if
        // there are no ace cards
        // or if the hand value >= 21 with the aces counting as 1 each
        // or if the soft hand value would exceed 21 if we added 1 ace value of 10
        // else add 10 to our hand value since only once ace is need, otherwise 2 aces = 22
        if (aceCount == 0 || handValue >= MAX_HAND_VALUE || softHandValue > MAX_HAND_VALUE)
        {
            return handValue;
        }
        
        return softHandValue;
    }

    /**
     * Returns the player's hand as a string
     *
     * @return Returns a string value
     */
    @Override
    public String toString()
    {
        
        String result = "";
        for (Object obj : _cards)
        {
            Card card = (Card) obj;
            result += "-- [ " + card.toString() + " ]\n";
        }
        result += "\n-- Hand Action: " + this.getHandAction().toString() + "\n";
        result += "\n-- Hand Value: " + this.getHandValue() + "\n";
        result += "-- Hand Result: " + this._playerHandResult.toString() + "\n";
        return result;
        
    }
    
}
