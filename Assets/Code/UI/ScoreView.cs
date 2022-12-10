using System;
using Code.Common;
using Code.Common.Events;
using Code.Ships.Common;
using Code.Util;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.UI
{
    [DefaultExecutionOrder(-1)]
    public class ScoreView : MonoBehaviour
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
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<ShipDestroyedEvent>(OnShipDestroyedEvent);
        }

        private void OnShipDestroyedEvent(ShipDestroyedEvent evt)
        {
            AddScore(evt.team, evt.scoreToAdd);
        }

        private void OnDestroy()
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<ShipDestroyedEvent>(OnShipDestroyedEvent);
        }

        public void Reset()
        {
            CurrentScore = 0;
        }

        private void AddScore(Teams killedTeam, int scoreToAdd)
        {
            if (killedTeam != Teams.Enemy) return;

            CurrentScore += scoreToAdd;
        }
    }
}