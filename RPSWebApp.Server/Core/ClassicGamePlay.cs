using RPSWebApp.Server.Models;

namespace RPSWebApp.Server.Core
{
    public abstract class ClassicGamePlay : IGamePlay
    {
        public GamePlayResult Play(GamePlayRequest request)
        {
            // Generate computer choice randomly
            var random = new Random();
            var choices = new[] { GameChoices.Rock, GameChoices.Paper, GameChoices.Scissors };
            var computerChoice = choices[random.Next(choices.Length)];

            // Determine result
            var result = GetResult(request.UserChoice, computerChoice);

            return result;
        }

        public GamePlayResult GetResult(GameChoices user, GameChoices computer)
        {
            GamePlayResult result = new GamePlayResult
            {
                UserChoice = user,
                ComputerChoice = computer
            };
            result.Result = DetermineResult(user, computer);

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

        private GameResult DetermineResult(GameChoices user, GameChoices computer)
        {
            if (user == computer) return GameResult.Draw;
            switch (user)
            {
                case GameChoices.Rock:
                    return (computer == GameChoices.Scissors) ? GameResult.Win : GameResult.Lose;
                case GameChoices.Paper:
                    return (computer == GameChoices.Rock) ? GameResult.Win : GameResult.Lose;
                case GameChoices.Scissors:
                    return (computer == GameChoices.Paper) ? GameResult.Win : GameResult.Lose;
                default:
                    return GameResult.Unknown; // Invalid choice
            }
        }
    }
}
