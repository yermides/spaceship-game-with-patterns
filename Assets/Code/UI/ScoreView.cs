using System;
using Code.Common;
using Code.Ships.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.UI
{
    [DefaultExecutionOrder(-1)]
    public class ScoreView : MonoBehaviour, IEventObserver
    {
        public static ScoreView Instance => _instance;
        private static ScoreView _instance;
        
        [SerializeField] private TextMeshProUGUI scoreText;
        private int _currentScore;

        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                _currentScore = value;

                if (!scoreText) return;
                scoreText.text = $"Score: {value}";
            }
        }

        private void Awake()
        {
            _instance = this;
            EventQueue.Instance.Subscribe(EventId.ShipDestroyed, this);
        }

        private void OnDestroy()
        {
            EventQueue.Instance.Unsubscribe(EventId.ShipDestroyed, this);
        }

        public void Reset()
        {
            CurrentScore = 0;
        }

        public void AddScore(Teams killedTeam, int scoreToAdd)
        {
            if (killedTeam != Teams.Enemy) return;

            CurrentScore += scoreToAdd;
        }

        public void Process(EventArgsBase args)
        {
            if (args.EventId != EventId.ShipDestroyed) return;

            var shipDestroyedArgs = (ShipDestroyedEvent)args;
            AddScore(shipDestroyedArgs.team, shipDestroyedArgs.scoreToAdd);
        }
    }
}