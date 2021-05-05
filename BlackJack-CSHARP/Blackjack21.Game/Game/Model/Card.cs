namespace Blackjack21.Game.Model
{
    /// <summary>
    /// Blackjack Card Object
    /// </summary>
    public class Card
    {
        private CardType _cardType;
        private CardSymbol _cardSymbol;
        private int _cardValue = 0;

        /// <summary>
        /// Initialize the card based on input parameters
        /// </summary>
        /// <param name="cardType">Set Card Type</param>
        /// <param name="cardSymbol">Set Card Symbol</param>
        public Card(CardType cardType = CardType.ACE, CardSymbol cardSymbol = CardSymbol.CLUBS)
        {
            SetCard(cardType, cardSymbol);
        }

        /// <summary>
        /// initialize the card based on number parameters inputs
        /// </summary>
        /// <param name="cardType">Set Card Type</param>
        /// <param name="cardSymbol">Set Card Color</param>
        public Card(int cardType, int cardSymbol)
        {
            SetCard((CardType)cardType, (CardSymbol)cardSymbol);
        }

        /// <summary>
        /// Set cards identity and calulate the value of the card
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardSymbol"></param>
        private void SetCard(CardType cardType, CardSymbol cardSymbol)
        {
            this._cardSymbol = cardSymbol;
            this._cardType = cardType;
            SetCardValue();
        }

        /// <summary>
        /// Sets or gets card's type
        /// </summary>
        public CardType CardType
        {
            get
            {
                return this._cardType;
            }
        }

        /// <summary>
        /// Gets card's symbol
        /// </summary>
        public CardSymbol CardSymbol
        {
            get
            {
                return this._cardSymbol;
            }
        }

        /// <summary>
        /// Return card's number value
        /// </summary>
        public int CardValue
        {
            get
            {
                return this._cardValue;
            }
        }

        /// <summary>
        /// Compare if the card is the same type as this card
        /// </summary>
        /// <param name="card">Card to compare with</param>
        /// <returns></returns>
        public bool CompareCardType(Card card)
        {
            return card != null && card.CardType == this.CardType;
        }

        /// <summary>
        /// Set card's value
        /// </summary>
        private void SetCardValue()
        {
            int cardTypeInt = (int)this._cardType;

            // if the card type value is over 10 count it as 10
            this._cardValue = cardTypeInt > 9 ? 10 : cardTypeInt;
        }

        /// <summary>
        /// Returns the card as a string value
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            string cardName = "";
            switch (this.CardType)
            {
                case CardType.ACE:
                    cardName = "ACE OF ";
                    break;

                case CardType.TWO:
                    cardName = "TWO OF ";
                    break;

                case CardType.THREE:
                    cardName = "THREE OF ";
                    break;

                case CardType.FOUR:
                    cardName = "FOUR OF ";
                    break;

                case CardType.FIVE:
                    cardName = "FIVE OF ";
                    break;

                case CardType.SIX:
                    cardName = "SIX OF ";
                    break;

                case CardType.SEVEN:
                    cardName = "SEVEN OF ";
                    break;

                case CardType.EIGHT:
                    cardName = "EIGHT OF ";
                    break;

                case CardType.NINE:
                    cardName = "NINE OF ";
                    break;

                case CardType.TEN:
                    cardName = "TEN OF ";
                    break;

                case CardType.JACK:
                    cardName = "JACK OF ";
                    break;

                case CardType.QUEEN:
                    cardName = "QUEEN OF ";
                    break;

                case CardType.KING:
                    cardName = "KING OF ";
                    break;
            }

            switch (this._cardSymbol)
            {
                case CardSymbol.CLUBS:
                    cardName += "CLUBS";
                    break;

                case CardSymbol.SPADES:
                    cardName += "SPADES";
                    break;

                case CardSymbol.HEARTS:
                    cardName += "HEARTS";
                    break;

                case CardSymbol.DIAMONDS:
                    cardName += "DIAMONDS";
                    break;

            }

            return cardName;
        }

    }
}
