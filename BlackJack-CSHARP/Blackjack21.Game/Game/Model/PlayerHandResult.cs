namespace Blackjack21.Game.Model
{
    /// <summary>
    /// The players hand result
    /// </summary>
    public enum PlayerHandResult
    {
        NONE = 0, // Not set yet
        PLAYER_WON_HIGHER = 1, // Play won with a higher hand than the dealer
        PLAYER_WON_BLACKJACK = 2, // Player hit black jack on their two delt cards, Ace + 10 card
        PLAYER_WON_5_CARD_CHARLIE = 3, // Player has 5 cards which counts as a win
        PLAYER_BUST_OVER_21 = 4, // Player lost value over 21
        PLAYER_HAND_LOWER = 5, // Players hand is lower then the dealer
        PLAYER_HAND_LOWER_BLACKJACK = 6, // Players hand is lower then the dealer's hand when the dealer has a blackjack result
        PLAYER_HAND_PUSH = 7, // Player tied with dealer
        PLAYER_HAND_PUSH_BLACKJACK = 8 // Player tied with the dealers black jack
    }
}
