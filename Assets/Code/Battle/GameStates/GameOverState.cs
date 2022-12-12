using System;
using Code.Common;
using Code.Common.Events;
using Code.Util;

namespace Code.Battle.GameStates
{
    public class GameOverState : IGameState
    {
        // private readonly GameFacade _gameFacade;

        public GameOverState()
        {
            
        }
        
        public GameOverState(GameFacade gameFacade)
        {
            // _gameFacade = gameFacade;
        }
        
        public void DoStart(Action<GameStateId> endedCallback)
        {
            // TODO: must be stop or endbattlecommand
            // _gameFacade.EndBattle();

            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Enqueue(new GameOverEvent());
        }

        public void DoStop()
        {
        }
    }
}