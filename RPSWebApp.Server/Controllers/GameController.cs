using Microsoft.AspNetCore.Mvc;
using RPSWebApp.Server.Models;
using RPSWebApp.Server.Core;

namespace RPSWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        [HttpPost("play")]
        public IActionResult Play([FromBody] GamePlayRequest request)
        {
            if (request == null || !Enum.IsDefined(typeof(GameChoices), request.UserChoice))
                return BadRequest("Invalid request.");

            var computerChoice = logic.GetRandomChoice();
            var result = logic.GetResult(request.UserChoice, computerChoice);

            return Ok(new GamePlayResult
            {
                UserChoice = request.UserChoice,
                ComputerChoice = computerChoice,
                Result = result
            });
        }

        [HttpGet("choices")]
        public IActionResult GetChoices([FromQuery] GameMode mode)
        {
            var logic = mode == GameMode.Extended ? (IGameLogic)new ExtendedGameLogic() : new ClassicGameLogic();
            return Ok(logic.GetChoices());
        }
    }
}
