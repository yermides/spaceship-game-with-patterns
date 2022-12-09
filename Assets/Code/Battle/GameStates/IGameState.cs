using System;

namespace Code.Battle.GameStates
{
    public interface IGameState
    {
        void DoStart(Action<GameStateId> endedCallback);
        void DoStop();
    }
}