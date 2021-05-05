package Blackjack21.Game.Model;

/**
 * The players hand result
 */
public enum PlayerHandResult
{
    NONE,// Not set yet  
    PLAYER_WON_HIGHER, // Play won with a higher hand than the dealer  
    PLAYER_WON_BLACKJACK,// Player hit black jack on their two delt cards, Ace + 10 card
    PLAYER_WON_5_CARD_CHARLIE, // Player has 5 cards which counts as a win
    PLAYER_BUST_OVER_21,// Player lost value over 21   
    PLAYER_HAND_LOWER,// Players hand is lower then the dealer
    PLAYER_HAND_LOWER_BLACKJACK, // Players hand is lower then the dealer's hand when the dealer has a blackjack result
    PLAYER_HAND_PUSH, // Player tied with dealer
    PLAYER_HAND_PUSH_BLACKJACK   //Player tied with the dealers black jack
}
