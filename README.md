# 01-04 Blackjack

<img src='demo.gif' width="350">

# Assignment Overview

In this project, you are creating a playable game of
[Blackjack](https://en.wikipedia.org/wiki/Blackjack). If you haven't played this
game ever, or in a while, grab a deck of cards and play a few games.

## Objectives

- Practice the skills and ideas you have learned so far.
- Effectively use loops, conditionals, and other control structures to implement
  [control flows](https://en.wikipedia.org/wiki/Control_flow)
- Demonstrate usage of data structures to model resources.

## Requirements

Create a single-player blackjack game that plays against the house, i.e., a
human player and computer dealer. You are free to create the user interface
however you want, **but keep it simple for Explorer Mode**.

### Explorer Mode

General Rules:

- [x] The game should be played with a standard deck of playing cards (52).
- [x] The house should be dealt two cards, hidden from the player until the
      house reveals its hand.
- [x] The player should be dealt two cards, visible to the player.
- [x] The player should have a chance to hit (i.e. be dealt another card) until
      they decide to stop or they _bust_ (i.e. their total is over 21). At which
      point they lose regardless of the dealer's hand.
- [x] When the player stands, the house reveals its hand and hits (i.e. draw
      cards) until they have 17 or more.
- [x] If dealer goes over 21 the dealer loses.

- [x] The player should have two choices: "Hit" and "Stand."
- [x] Consider Aces to be worth 11, never 1.
- [x] The app should display the winner. For this mode, the winner is who is
      closer to a blackjack (21) without going over.
- [x] There should be an option to play again; this should reset the hands and
      reshuffle the deck.

### Adventure Mode

- [x] Reveal one of the house's cards to the player when cards dealt.
- [x] Consider aces be worth one _or_ eleven.
- [x] Allow the player to
      ["Split"](https://blog.betway.com/casino/blackjack-strategy-101-how-do-you-split-in-blackjack/).
- [x] Improve the win requirements. From Wikipedia:
  > Players win by not busting and having a total higher than the dealer, or not
  > busting and having the dealer bust, or getting a blackjack without the
  > dealer getting a blackjack. If the player and dealer have the same total
  > (not counting blackjacks), this is called a "push," and the player typically
  > does not win or lose money on that hand. Otherwise, the dealer wins.

### Epic Mode

- [x] Add a betting system that carries between rounds.
- [x] Allow the player to "Double down" or "Surrender."
- [x] Add more players.
