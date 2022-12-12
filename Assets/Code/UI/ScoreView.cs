using Code.Common.Events;
using Code.Ships.Common;
using Code.Util;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private int _currentScore;

        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                _currentScore = value;

                if (!scoreText) return;
                scoreText.text = $"{value}";
            }
        }

        private void Start()
        {
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