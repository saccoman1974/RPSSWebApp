using System.Collections.Generic;

namespace RPSWebApp.Server.Core
{
    public static class OutcomeReasons
    {
        public static readonly Dictionary<(GameChoices winner, GameChoices loser), string> Reasons =
            new()
            {
                {(GameChoices.Rock, GameChoices.Scissors), "Rock crushes Scissors"},
                {(GameChoices.Rock, GameChoices.Lizard), "Rock crushes Lizard"},
                {(GameChoices.Paper, GameChoices.Rock), "Paper covers Rock"},
                {(GameChoices.Paper, GameChoices.Spock), "Paper disproves Spock"},
                {(GameChoices.Scissors, GameChoices.Paper), "Scissors cuts Paper"},
                {(GameChoices.Scissors, GameChoices.Lizard), "Scissors decapitates Lizard"},
                {(GameChoices.Lizard, GameChoices.Spock), "Lizard poisons Spock"},
                {(GameChoices.Lizard, GameChoices.Paper), "Lizard eats Paper"},
                {(GameChoices.Spock, GameChoices.Scissors), "Spock smashes Scissors"},
                {(GameChoices.Spock, GameChoices.Rock), "Spock vaporizes Rock"}
            };
    }
}