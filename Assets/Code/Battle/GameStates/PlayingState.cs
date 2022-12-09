using System;
using Code.Common;
using Code.Common.Events;
using Code.Ships.Common;

namespace Code.Battle.GameStates
{
    public class PlayingState : IGameState, IEventObserver
    {
        private int _shipsAliveCount;
        private bool _haveAllShipsSpawned;
        private Action<GameStateId> _endedCallback;

        public void DoStart(Action<GameStateId> endedCallback)
        {
            _endedCallback = endedCallback;
            EventQueue.Instance.Subscribe(EventId.ShipSpawned, this);
            EventQueue.Instance.Subscribe(EventId.ShipDestroyed, this);
            EventQueue.Instance.Subscribe(EventId.AllShipsSpawned, this);
        }

        public void DoStop()
        {
            EventQueue.Instance.Unsubscribe(EventId.ShipSpawned, this);
            EventQueue.Instance.Unsubscribe(EventId.ShipDestroyed, this);
            EventQueue.Instance.Unsubscribe(EventId.AllShipsSpawned, this);
        }
        
        public void Process(EventArgsBase args)
        {
            var eventType = args.EventId;
                
            if(eventType == EventId.ShipSpawned)
            {
                _shipsAliveCount++;
            }
            else if(eventType == EventId.ShipDestroyed)
            {
                _shipsAliveCount--;

                var shipDestroyedArgs = (ShipDestroyedEvent)args;
                    
                if (shipDestroyedArgs.team == Teams.Ally)
                {
                    _endedCallback?.Invoke(GameStateId.GameOver);
                }
            }
            else if(eventType == EventId.AllShipsSpawned)
            {
                _haveAllShipsSpawned = true;
            }
                
            CheckGameState();
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
    }
}