using RPSWebApp.Server.Core;
using RPSWebApp.Server.Models;

namespace RPSWebApp.Server.Core
{
    public class EnhancedGamePlay : ClassicGamePlay
    {
        public GamePlayResult EnhancedPlay(GamePlayRequest request)
        {
            // Generate computer choice randomly
            var random = new Random();
            var choices = new[] { GameChoices.Rock, GameChoices.Paper, GameChoices.Scissors, GameChoices.Spock, GameChoices.Lizard };
            var computerChoice = choices[random.Next(choices.Length)];

            // Determine result
            var result = GetEnhancedResult(request.UserChoice, computerChoice);

            return result;
        }

        public GamePlayResult GetEnhancedResult(GameChoices user, GameChoices computer)
        {
            GamePlayResult result = new GamePlayResult
            {
                UserChoice = user,
                ComputerChoice = computer
            };
            result.Result = DetermineEnhancedResult(user, computer);

            // Set outcome reasons for frontend compatibility
            if (result.Result == GameResult.Win)
            {
                if (OutcomeReasons.Reasons.TryGetValue((user, computer), out string? reason))
                {
                    result.OutcomeReason = reason;
                }
                else
                {
                    result.OutcomeReason = "Unknown reason for win.";
                }
            }
            else if (result.Result == GameResult.Lose)
            {
                if (OutcomeReasons.Reasons.TryGetValue((computer, user), out string? reason))
                {
                    result.OutcomeReason = reason;
                }
                else
                {
                    result.OutcomeReason = "Unknown reason for loss.";
                }
            }
            else
            {
                result.OutcomeReason = "It's a draw!";
            }
            return result;
        }


        private GameResult DetermineEnhancedResult(GameChoices user, GameChoices computer)
        {
            if (user == computer) return GameResult.Draw;
            switch (user)
            {
                case GameChoices.Spock:
                    return (computer == GameChoices.Scissors ||computer == GameChoices.Rock) ? GameResult.Win : GameResult.Lose;
                case GameChoices.Paper:
                    return (computer == GameChoices.Rock || computer == GameChoices.Spock) ? GameResult.Win : GameResult.Lose;
                case GameChoices.Scissors:
                    return (computer == GameChoices.Paper ||computer == GameChoices.Lizard) ? GameResult.Win : GameResult.Lose;
                case GameChoices.Rock:
                    return (computer == GameChoices.Scissors || computer == GameChoices.Lizard) ? GameResult.Win : GameResult.Lose;
                case GameChoices.Lizard:
                    return (computer == GameChoices.Spock || computer == GameChoices.Paper) ? GameResult.Win : GameResult.Lose;
                default:
                    return GameResult.Unknown; // Invalid choice
            }
        }


    }

}
