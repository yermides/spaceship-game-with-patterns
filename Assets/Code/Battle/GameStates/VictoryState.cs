using System;
using Code.Common;
using Code.Common.Events;

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
            EventQueue.Instance.EnqueueEvent(new VictoryEvent());
        }

        public void DoStop()
        {
        }
    }
}