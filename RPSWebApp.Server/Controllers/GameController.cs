using Microsoft.AspNetCore.Mvc;
using RPSWebApp.Server.Models;
using RPSWebApp.Server.Core;

namespace RPSWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ClassicGamePlay _classicGamePlay;

        public GameController(ClassicGamePlay classicGamePlay)
        {
            _classicGamePlay = classicGamePlay;
        }

        [HttpPost("play")]
        public IActionResult Play([FromBody] GamePlayRequest request)
        {
            if (request == null || !Enum.IsDefined(typeof(GameChoices), request.UserChoice))
                return BadRequest("Invalid request.");

            var result = _classicGamePlay.Play(request);

            return Ok(result);
        }

        // Only classic choices for now
        [HttpGet("choices")]
        public IActionResult GetChoices()
        {
            var choices = new[] { GameChoices.Rock, GameChoices.Paper, GameChoices.Scissors };
            return Ok(choices);
        }

        // Only classic choices for now
        [HttpGet("enchancedchoices")]
        public IActionResult GetEnhancedChoices()
        {
            var choices = new[] { GameChoices.Rock, GameChoices.Paper, GameChoices.Scissors, GameChoices.Lizard, GameChoices.Spock };
            return Ok(choices);
        }

    }
}
