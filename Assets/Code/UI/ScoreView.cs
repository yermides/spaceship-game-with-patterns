using System;
using Code.Ships.Common;
using TMPro;
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

        private int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;

                if (!scoreText) return;
                scoreText.text = $"Score: {value}";
            }
        }

        private void Awake()
        {
            _instance = this;
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
    }
}