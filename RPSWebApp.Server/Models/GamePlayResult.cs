using RPSWebApp.Server.Core;

namespace RPSWebApp.Server.Models
{
    public class GamePlayResult
    {
        public GameResult Result { get; set; } = GameResult.Unknown;
        public GameChoices UserChoice { get; set; } = GameChoices.None;
        public GameChoices ComputerChoice { get; set; } = GameChoices.None;
    }
}
