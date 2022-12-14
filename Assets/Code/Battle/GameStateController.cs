using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Battle.GameStates;
using UnityEngine;

namespace Code.Battle
{
    public enum GameStateId
    {
        Playing,
        GameOver,
        Victory
    }

    public class GameStateController : MonoBehaviour
    {
        private Dictionary<GameStateId, IGameState> _idToStates;
        private IGameState _currentGameState;

        private void Awake()
        {
            // configure reusable states
            _idToStates = new Dictionary<GameStateId, IGameState>
            {
                { GameStateId.Playing , new PlayingState() },
                { GameStateId.GameOver, new GameOverState() },
                { GameStateId.Victory, new VictoryState() },
            };
        }

        private void Start()
        {
            _currentGameState = GetState(GameStateId.Playing);
            _currentGameState.DoStart(SwitchState);
        }

        private async void SwitchState(GameStateId id)
        {
            await Task.Yield();
            _currentGameState.DoStop();
            _currentGameState = GetState(id);
            _currentGameState.DoStart(SwitchState);
        }

        public void Reset()
        {
            SwitchState(GameStateId.Playing);
        }

        private IGameState GetState(GameStateId id)
        {
            return _idToStates[id];
        }
    }
}
