using RPSWebApp.Server.Core;

namespace RPSWebApp.Server.Models
{
    public class GamePlayRequest
    {
        public GameChoices UserChoice { get; set; } = GameChoices.None; // Fixed initialization to a valid GameChoices enum value
        public GameChoices ComputerChoice { get; set; } = GameChoices.None; // Fixed initialization to a valid GameChoices enum value
        public GameMode Mode { get; set; } = GameMode.None;
    }
}
