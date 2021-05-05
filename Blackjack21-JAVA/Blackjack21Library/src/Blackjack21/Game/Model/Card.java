package Blackjack21.Game.Model;

/**
 * Blackjack Card Object
 */
public class Card
{

    private CardType _cardType = CardType.ACE;
    private CardSymbol _cardSymbol = CardSymbol.CLUBS;
    private int _cardValue = 0;

    /**
     * Initialize the card based on input parameters
     *
     * @param cardType Set Card Type
     * @param cardSymbol Set Card Symbol
     */
    public Card(CardType cardType, CardSymbol cardSymbol)
    {
        setCard(cardType, cardSymbol);
    }

    /**
     * initialize the card based on number parameters inputs
     *
     * @param cardType Set Card Type
     * @param cardSymbol Set Card Color
     */
    public Card(int cardType, int cardSymbol)
    {
        setCard(CardType.values()[cardType], CardSymbol.values()[cardSymbol]);
    }

    /**
     * Set cards identity and calculate the value of the card
     *
     * @param cardType
     * @param cardSymbol
     */
    private void setCard(CardType cardType, CardSymbol cardSymbol)
    {
        this._cardSymbol = cardSymbol;
        this._cardType = cardType;
        setCardValue();
    }

    /**
     * Sets or gets card's type
     *
     * @return return the card's type
     */
    public CardType getCardType()
    {
        return this._cardType;
    }

    /**
     * Gets card's symbol
     *
     * @return return the card's symbol
     */
    public CardSymbol getCardSymbol()
    {
        return this._cardSymbol;
    }

    /**
     * Return card's number value
     *
     * @return return the cards value
     */
    public int getCardValue()
    {
        return this._cardValue;
    }

    /**
     * Compare if the card is the same type as this card
     *
     * @param card Card to compare with
     * @return return the result of the compare
     */
    public boolean compareCardType(Card card)
    {
        return card != null && card.getCardType() == this.getCardType();
    }

    /**
     * Set card's value
     */
    private void setCardValue()
    {
        int cardTypeInt = ((Enum) this._cardType).ordinal();
        // if the card type value is over 10 count it as 10
        this._cardValue = cardTypeInt > 9 ? 10 : cardTypeInt;
    }

    /**
     * Returns the card as a string value
     *
     * @return return the string value of the card
     */
    @Override
    public String toString()
    {

        String cardName = "";
        switch (this.getCardType())
        {
            case ACE:
                cardName = "ACE OF ";
                break;
            case TWO:
                cardName = "TWO OF ";
                break;
            case THREE:
                cardName = "THREE OF ";
                break;
            case FOUR:
                cardName = "FOUR OF ";
                break;
            case FIVE:
                cardName = "FIVE OF ";
                break;
            case SIX:
                cardName = "SIX OF ";
                break;
            case SEVEN:
                cardName = "SEVEN OF ";
                break;
            case EIGHT:
                cardName = "EIGHT OF ";
                break;
            case NINE:
                cardName = "NINE OF ";
                break;
            case TEN:
                cardName = "TEN OF ";
                break;
            case JACK:
                cardName = "JACK OF ";
                break;
            case QUEEN:
                cardName = "QUEEN OF ";
                break;
            case KING:
                cardName = "KING OF ";
                break;

        }
        switch (this._cardSymbol)
        {
            case CLUBS:
                cardName += "CLUBS";
                break;
            case SPADES:
                cardName += "SPADES";
                break;
            case HEARTS:
                cardName += "HEARTS";
                break;
            case DIAMONDS:
                cardName += "DIAMONDS";
                break;

        }
        return cardName;

    }

}
