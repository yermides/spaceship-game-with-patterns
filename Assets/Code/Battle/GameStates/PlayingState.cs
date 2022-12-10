using System;
using Code.Common.Events;
using Code.Ships.Common;
using Code.Util;

namespace Code.Battle.GameStates
{
    public class PlayingState : IGameState
    {
        private int _shipsAliveCount;
        private bool _haveAllShipsSpawned;
        private Action<GameStateId> _endedCallback;

        public void DoStart(Action<GameStateId> endedCallback)
        {
            _endedCallback = endedCallback;
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<ShipDestroyedEvent>(OnShipDestroyed);
            eventQueue.Subscribe<ShipSpawnedEvent>(OnShipSpawned);
            eventQueue.Subscribe<AllShipsSpawnedEvent>(OnAllShipsSpawned);
        }

        public void DoStop()
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<ShipDestroyedEvent>(OnShipDestroyed);
            eventQueue.Unsubscribe<ShipSpawnedEvent>(OnShipSpawned);
            eventQueue.Unsubscribe<AllShipsSpawnedEvent>(OnAllShipsSpawned);
        }

        private void CheckGameState()
        {
            if (_haveAllShipsSpawned && (_shipsAliveCount == 0))
            {
                _endedCallback?.Invoke(GameStateId.Victory);

                // _hasVictoryBeenClaimed = true;
                // EventQueue.Instance.EnqueueEvent(new VictoryEvent());
                // Debug.LogWarning("Victory!");
            }
        }
        
        private void OnShipSpawned(ShipSpawnedEvent evt)
        {
            _shipsAliveCount++;
        }

        private void OnShipDestroyed(ShipDestroyedEvent evt)
        {
            _shipsAliveCount--;

            if (evt.team == Teams.Ally)
            {
                _endedCallback?.Invoke(GameStateId.GameOver);
            }
            
            CheckGameState();
        }
        
        private void OnAllShipsSpawned(AllShipsSpawnedEvent evt)
        {
            _haveAllShipsSpawned = true;
        }
    }
}