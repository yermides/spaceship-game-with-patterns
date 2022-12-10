using System;
using Code.Common;
using Code.Common.Events;
using Code.Util;

namespace Code.Battle.GameStates
{
    public class VictoryState : IGameState
    {
        private GameFacade _gameFacade;

        public VictoryState(GameFacade gameFacade)
        {
            _gameFacade = gameFacade;
        }

        public void DoStart(Action<GameStateId> endedCallback)
        {
            _gameFacade.StopBattle();
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Enqueue(new VictoryEvent());
        }

        public void DoStop()
        {
        }
    }
}