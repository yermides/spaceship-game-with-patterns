using System;
using Code.Common.Commands;
using Code.Common.Events;
using Code.Util;

namespace Code.Battle.GameStates
{
    public class GameOverState : IGameState
    {
        public void DoStart(Action<GameStateId> endedCallback)
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            
            commandQueue.AddAndRunCommand(new StopBattleCommand());
            eventQueue.Enqueue(new GameOverEvent());
        }

        public void DoStop()
        {
        }
    }
}