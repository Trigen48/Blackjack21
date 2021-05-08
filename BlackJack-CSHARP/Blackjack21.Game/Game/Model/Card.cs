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
        /// Initializes the card object
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
        /// Gets card's type
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
        /// Compare the card type against the another card
        /// </summary>
        /// <param name="card">Card to compare with</param>
        /// <returns>Returns if the card type is the same as the other card type</returns>
        public bool CompareCardType(Card card)
        {
            return card != null && card.CardType == this.CardType;
        }

        /// <summary>
        /// Compare the card value against the another card
        /// </summary>
        /// <param name="card">Card to compare with</param>
        /// <returns>Returns if the card value is the same as the other card value</returns>
        public bool CompareCardValue(Card card)
        {
            return card != null && card.CardValue == this.CardValue;
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
        /// Returns the card name as a string value
        /// </summary>
        /// <returns>returns the string name of the card</returns>
        public override string ToString()
        {
            return this.CardType + " OF " + this._cardSymbol.ToString(); ;
        }

    }
}
