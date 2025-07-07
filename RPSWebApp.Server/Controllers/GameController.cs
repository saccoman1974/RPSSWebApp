using Microsoft.AspNetCore.Mvc;
using RPSWebApp.Server.Models;
using RPSWebApp.Server.Core;
using System.Linq;

namespace RPSWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ClassicGamePlay _classicGamePlay;
        private readonly EnhancedGamePlay _enhancedGamePlay;

        public GameController(ClassicGamePlay classicGamePlay, EnhancedGamePlay enhancedGamePlay)
        {
            _classicGamePlay = classicGamePlay;
            _enhancedGamePlay = enhancedGamePlay;
        }

        [HttpPost("play")]
        public IActionResult Play([FromBody] GamePlayRequest request)
        {
            if (request == null || !Enum.IsDefined(typeof(GameChoices), request.UserChoice))
                return BadRequest("Invalid request.");

            var result = new GamePlayResult
            {
                UserChoice = request.UserChoice,
                ComputerChoice = GameChoices.None,
                Result = GameResult.Unknown
            };

            // Check if Classic or Enhanced mode is selected
            if (request.Mode == GameMode.Enhanced)
            {
                result = _enhancedGamePlay.Play(request);
            }
            else if (request.Mode == GameMode.Classic)
            {
                result = _classicGamePlay.Play(request);
            }

            // Return string values for frontend compatibility
            return Ok(new
            {
                userChoice = result.UserChoice.ToString(),
                computerChoice = result.ComputerChoice.ToString(),
                result = result.Result.ToString(),
                outcomeReason = result.OutcomeReason.ToString()
            });
        }

        // Only classic choices for now
        [HttpGet("choices")]
        public IActionResult GetChoices()
        {
            var choices = new[] { GameChoices.Rock, GameChoices.Paper, GameChoices.Scissors };
            // Return string names for frontend
            return Ok(choices.Select(c => c.ToString()));
        }

        // Only enhanced choices for now
        [HttpGet("enchancedchoices")]
        public IActionResult GetEnhancedChoices()
        {
            var choices = new[] { GameChoices.Rock, GameChoices.Paper, GameChoices.Scissors, GameChoices.Lizard, GameChoices.Spock };
            return Ok(choices.Select(c => c.ToString()));
        }
    }
}
