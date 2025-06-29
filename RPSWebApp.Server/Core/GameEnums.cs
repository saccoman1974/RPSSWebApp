namespace RPSWebApp.Server.Core
{
    // This file defines the enums used in the game logic for Rock-Paper-Scissors and its variants.
    public enum GameChoices
    {
        None,
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    public enum GameMode
    {
        None,
        Classic,
        Enhanced
    }

    public enum GameResult
    {
        Unknown,
        Win,
        Lose,
        Draw
    }
}
