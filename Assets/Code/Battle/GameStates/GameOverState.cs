using System;
using Code.Common;
using Code.Common.Events;
using Code.Util;

namespace Code.Battle.GameStates
{
    public class GameOverState : IGameState
    {
        private readonly GameFacade _gameFacade;
        
        public GameOverState(GameFacade gameFacade)
        {
            _gameFacade = gameFacade;
        }
        
        public void DoStart(Action<GameStateId> endedCallback)
        {
            _gameFacade.EndBattle();

            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Enqueue(new GameOverEvent());
        }

        public void DoStop()
        {
        }
    }
}