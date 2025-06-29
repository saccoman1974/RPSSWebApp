using RPSWebApp.Server.Models;

namespace RPSWebApp.Server.Core
{
    public interface IGamePlay
    {
        GamePlayResult Play(GamePlayRequest request);
    }
}
