using System;
using Code.Common;
using Code.Common.Commands;
using Code.Common.Events;
using Code.Util;

namespace Code.Battle.GameStates
{
    public class VictoryState : IGameState
    {
        public void DoStart(Action<GameStateId> endedCallback)
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            
            commandQueue.AddAndRunCommand(new StopBattleCommand());
            eventQueue.Enqueue(new VictoryEvent());
        }

        public void DoStop()
        {
        }
    }
}